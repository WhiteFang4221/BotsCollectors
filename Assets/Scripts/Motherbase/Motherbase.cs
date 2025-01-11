using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class Motherbase : PoolableObject<Motherbase>, IShowPanel, IMotherbasePanelEvents, IFlagSetter, IWorkerEmployer
{
    [SerializeField] private ResourceScanner _resourceScanner;
    [SerializeField] private Collider _workerSpawnArea;
    [SerializeField] public WorkerSpawner _workerSpawner;
    [SerializeField] private List<Worker> _workers = new List<Worker>();

    private List<Transform> _foundResources = new List<Transform>();
    private HashSet<Transform> _resourcesInProgress = new HashSet<Transform>();

    private MotherbaseMediator _motherbaseMediator;
    private ResourceRegistry _resourceRegistry;

    private int _availableResources = 0;
    private int _minResourceToCreateWorker = 3;
    private int _minResourceToCreateBase = 5;
    private int _minWorkersOnBase = 1;

    private Collider _motherbaseCollider;
    private Coroutine _sendingWorkerToFlagCoroutine;
    private Coroutine _scannerCoroutine;
    private WaitForSeconds _scannerCooldown = new WaitForSeconds(0.5f);
    private bool _isPanelOpen = false;

    public Flag CurrentFlag { get; private set; }

    public event Action PanelOpened;
    public event Action PanelClosed;
    public event Action<int> WorkersCountUpdated;
    public event Action<int, int> ResourceCountUpdated;

    public event Action<IFlagSetter, Flag> FlagGot;
    public event Action<IFlagSetter> FlagSetterDisabled;

    public event Action<IBaseBuilder> WorkerSent;

    private void OnDisable()
    {
        _workerSpawner.WorkerSpawned -= AddWorker;
        StopScannerRoutine();
        FlagSetterDisabled?.Invoke(this);
    }

    private void Update()
    {
        if (_scannerCoroutine == null && _foundResources.Count == 0)
        {
            StartScannerRoutine();
        }

        if (_foundResources.Count > 0 && _resourcesInProgress.Count < _workers.Count)
        {
            SendWorkerAtResource();
        }
    }

    public void Initialize(MotherbaseMediator mediator, ResourceRegistry resourceRegistry, WorkerPool pool)
    {
        _motherbaseCollider = GetComponent<Collider>();
        _motherbaseMediator = mediator;
        _resourceRegistry = resourceRegistry;
        _workerSpawner.WorkerSpawned += AddWorker;
        _workerSpawner.InitializePool(pool);
        
        foreach (var worker in _workers)
        {
            InitializeWorker(worker);
        }

        WorkersCountUpdated?.Invoke(_workers.Count);
        ResourceCountUpdated?.Invoke(_availableResources, _minResourceToCreateWorker);
    }

    public void ShowPanel()
    {
        _isPanelOpen = true;
        _motherbaseMediator.SubscribeToEvents(this);
        PanelOpened?.Invoke();
        ResourceCountUpdated?.Invoke(_availableResources, _minResourceToCreateWorker);
        WorkersCountUpdated?.Invoke(_workers.Count);
        _motherbaseMediator.WorkerCreateStarted += SpawnWorker;
        _motherbaseMediator.BaseCreateStarted += SetTheFlag;
    }

    public void HidePanel()
    {
        _isPanelOpen = false;
        _motherbaseMediator.WorkerCreateStarted -= SpawnWorker;
        _motherbaseMediator.BaseCreateStarted -= SetTheFlag;
        PanelClosed?.Invoke();
        _motherbaseMediator.UnscribeToEvents(this);
    }

    private IEnumerator ScanTheArea()
    {
        while (_foundResources.Count == 0)
        {
            _foundResources = _resourceScanner.GetResourceInRange().Where(resource => !_resourceRegistry.ResourceInProgress.Contains(resource)).ToList();
            _resourceRegistry.RegisterResources(_foundResources);
            yield return _scannerCooldown;
        }

        StopScannerRoutine();
    }

    private void SendWorkerAtResource()
    {
        Worker availableWorker = _workers.FirstOrDefault(worker => !worker.IsBusy);
        Transform availableResource = _foundResources.FirstOrDefault(resource => !_resourceRegistry.ResourceInProgress.Contains(resource));

        if (availableWorker != null && availableResource != null)
        {
            _foundResources.Remove(availableResource);

            if (_resourceRegistry.TryReserveResource(availableResource))
            {
                _resourcesInProgress.Add(availableResource);
                _resourceRegistry.RegisterResourceInProgress(availableResource);
                availableWorker.SetTarget(availableResource);
                availableWorker.ResourceHasGiven += OnResourceCollected;
            }
        }
    }

    public void SetBuildMotherbasePriority(Flag flag)
    {
        CurrentFlag = flag;
        StartSendWorkerToFlagRoutine();
    }

    public void HireWorker(Worker worker)
    {
        AddWorker(worker);
    }

    private void SetTheFlag()
    {
        if (_workers.Count > _minWorkersOnBase)
        {
            FlagGot?.Invoke(this, CurrentFlag);
        }
        else
        {
            Debug.Log("Слишком мало работников на базе");
        }
    }

    private IEnumerator SendWorkerToFlag()
    {
        while (_availableResources < _minResourceToCreateBase)
        {
            yield return null;
        }

        RemoveResource(_minResourceToCreateBase);

        while (IsWorkerSentToFlag() == false)
        {
            yield return null;
        }
    }

    private bool IsWorkerSentToFlag()
    {
        if (_workers.FirstOrDefault(worker => !worker.IsBusy))
        {
            Worker availableWorker = _workers.FirstOrDefault(worker => !worker.IsBusy);
            availableWorker.SetTarget(CurrentFlag.transform);
            WorkerSent.Invoke(availableWorker);
            return true;
        }
        else
        {
            return false;
        }
    }

    private void SpawnWorker()
    {
        _workerSpawner.SpawnWorker(_workerSpawnArea);
        _availableResources -= _minResourceToCreateWorker;
        ResourceCountUpdated?.Invoke(_availableResources, _minResourceToCreateWorker);
        WorkersCountUpdated?.Invoke(_workers.Count);
    }

    private void AddWorker(Worker worker)
    {
        _workers.Add(worker);
        InitializeWorker(worker);
    }

    private void DismissWorker(Worker worker)
    {
        _workers.Remove(worker);

        worker.QuitMotherbase -= OnWorkerQuit;
        UpdateWorkersCount();
    }

    private void InitializeWorker(Worker worker)
    {
        worker.QuitMotherbase += OnWorkerQuit;
        UpdateWorkersCount();
        worker.InitializeMotherbaseCollider(_motherbaseCollider);
    }

    private void OnResourceCollected(Worker worker, Transform resource)
    {

        if (resource.TryGetComponent(out Resource component))
        {
            component.Disable();
        }

        _resourcesInProgress.Remove(resource);
        _resourceRegistry.UnregisterResourceInProgress(resource);
        worker.ResourceHasGiven -= OnResourceCollected;
        AddResource();
    }

    private void OnWorkerQuit(Worker worker)
    {
        CurrentFlag = null;
        DismissWorker(worker);
    }

    private void AddResource()
    {
        _availableResources++;
        UpdateResourceCount();
    }

    private void RemoveResource(int count)
    {
        _availableResources -= count;

        if (_availableResources < 0)
        {
            _availableResources = 0;
        }

        UpdateResourceCount();
    }

    private void UpdateWorkersCount()
    {
        if (_isPanelOpen)
        {
            WorkersCountUpdated?.Invoke(_workers.Count);
        }
    }

    private void UpdateResourceCount()
    {
        if (_isPanelOpen)
        {
            ResourceCountUpdated?.Invoke(_availableResources, _minResourceToCreateWorker);
        }
    }

    private void StartScannerRoutine()
    {
        StopScannerRoutine();
        _scannerCoroutine = StartCoroutine(ScanTheArea());
    }

    private void StopScannerRoutine()
    {
        if (_scannerCoroutine != null)
        {
            StopCoroutine(_scannerCoroutine);
            _scannerCoroutine = null;
        }
    }

    private void StartSendWorkerToFlagRoutine()
    {
        StopSendWorkerToFlagRoutine();
        _sendingWorkerToFlagCoroutine = StartCoroutine(SendWorkerToFlag());
    }

    private void StopSendWorkerToFlagRoutine()
    {
        if (_sendingWorkerToFlagCoroutine != null)
        {
            StopCoroutine(_sendingWorkerToFlagCoroutine);
            _sendingWorkerToFlagCoroutine = null;
        }
    }
}
