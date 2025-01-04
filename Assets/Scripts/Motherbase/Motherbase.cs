using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class Motherbase : PoolableObject<Motherbase>, IShowPanel, IMotherbasePanelEvents, IFlagKeeper
{
    [SerializeField] private ResourceScanner _resourceScanner;
    [SerializeField] private WorkerSpawner _workerSpawner;
    [SerializeField] private Collider _workerSpawnArea;
    [SerializeField] private int _availableResources = 0;

    private int _minResourceToCreateWorker = 3;
    private int _minResourceToCreateBase = 5;

    private Collider _motherbaseCollider;
    private List<Worker> _workers = new List<Worker>();
    private int _minWorkersCount = 3;

    private List<Transform> _foundResources = new List<Transform>();
    private HashSet<Transform> _resourcesInProgress = new HashSet<Transform>();

    private Coroutine _scannerCoroutine;
    private WaitForSeconds _scannerCooldown = new WaitForSeconds(0.5f);

    private Coroutine _buildingMotherbaseCoroutine;

    private MotherbaseMediator _motherbaseMediator;
    private ResourceRegistry _resourceRegistry;
    private Flag _currentFlag;

    private bool _isPanelOpen = false;
    private bool _isMotherbaseBuilding = false;

    public event Action PanelOpened;
    public event Action PanelClosed;
    public event Action<int> WorkersCountUpdated;
    public event Action<int, int> ResourceCountUpdated;
    public event Action<IFlagKeeper> FlagGot;
    public event Action<IFlagKeeper> FlagKeeperDisabled;

    private void OnEnable()
    {
        _workerSpawner.WorkerSpawned += AddWorker;
    }

    private void OnDisable()
    {
        _workerSpawner.WorkerSpawned -= AddWorker;
        StopScannerRoutine();
        FlagKeeperDisabled?.Invoke(this);
    }

    private void Start()
    {
        _motherbaseCollider = GetComponent<Collider>();

        for (int i = 0; i < _minWorkersCount; i++)
        {
            _workerSpawner.SpawnWorker(_workerSpawnArea);
        }

        WorkersCountUpdated?.Invoke(_workers.Count);
        ResourceCountUpdated?.Invoke(_availableResources, _minResourceToCreateWorker);
    }

    private void Update()
    {
        if (_scannerCoroutine == null && _foundResources.Count == 0)
        {
            StartScannerRoutine();
        }

        if (_currentFlag != null && _isMotherbaseBuilding == false && _availableResources >= _minResourceToCreateBase)
        {
            SendWorkerBuildNewMotherbase();
        }

        if (_foundResources.Count > 0 && _resourcesInProgress.Count < _workers.Count)
        {
            SendWorkerAtResource();
        }
    }

    public void Initialize(MotherbaseMediator mediator, ResourceRegistry resourceRegistry)
    {
        _motherbaseMediator = mediator;
        _resourceRegistry = resourceRegistry;
    }

    public void ShowPanel()
    {
        _isPanelOpen = true;
        _motherbaseMediator.SubscribeToEvents(this);
        PanelOpened?.Invoke();
        ResourceCountUpdated?.Invoke(_availableResources, _minResourceToCreateWorker);
        WorkersCountUpdated?.Invoke(_workers.Count);
        _motherbaseMediator.WorkerCreateStarted += CreateWorker;
        _motherbaseMediator.BaseCreateStarted += SetTheFlag;
    }

    public void HidePanel()
    {
        _isPanelOpen = false;
        _motherbaseMediator.WorkerCreateStarted -= CreateWorker;
        _motherbaseMediator.BaseCreateStarted -= SetTheFlag;
        PanelClosed?.Invoke();
        _motherbaseMediator.UnscribeToEvents(this);
    }

    public void SetBuildMotherbasePriority(Flag flag)
    {
        _currentFlag = flag;
        SendWorkerBuildNewMotherbase();
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
        Worker availableWorker = _data.Workers.FirstOrDefault(worker => !worker.IsBusy);
        Transform availableResource = _data.FoundResources.FirstOrDefault(resource => !_resourceRegistry.ResourceInProgress.Contains(resource));

        if (availableWorker != null && availableResource != null)
        {
            _foundResources.Remove(availableResource);

            if (_resourceRegistry.TryReserveResource(availableResource))
            {
                _resourcesInProgress.Add(availableResource);
                _resourceRegistry.RegisterResourceInProgress(availableResource);
                availableWorker.ResourceHasGiven += OnResourceCollected;
                availableWorker.SetTarget(availableResource);
            }
        }
    }

    private void SendWorkerBuildNewMotherbase()
    {
        if (_workers.FirstOrDefault(worker => !worker.IsBusy))
        {
            Worker availableWorker = _workers.FirstOrDefault(worker => !worker.IsBusy);
            availableWorker.SetTarget(_currentFlag.transform);
            _isMotherbaseBuilding = true;
        }
    }

    private void SetTheFlag()
    {
        FlagGot?.Invoke(this);
    }

    private void CreateWorker()
    {
        _workerSpawner.SpawnWorker(_workerSpawnArea);
        _availableResources -= _minResourceToCreateWorker;
        ResourceCountUpdated?.Invoke(_availableResources, _minWorkersCount);
        WorkersCountUpdated?.Invoke(_workers.Count);
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
        ReceiveResource();
    }

    private void ReceiveResource()
    {
        _availableResources++;

        if (_isPanelOpen)
        {
            ResourceCountUpdated?.Invoke(_availableResources, _minResourceToCreateWorker);
        }
    }

    private void AddWorker(Worker worker)
    {
        _workers.Add(worker);

        if (_isPanelOpen)
        {
            WorkersCountUpdated?.Invoke(_workers.Count);
        }

        worker.InitializeMotherbase(_motherbaseCollider);
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
}
