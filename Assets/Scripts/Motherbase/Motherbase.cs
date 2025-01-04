using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class Motherbase : PoolableObject<Motherbase>, IFlagKeeper
{
    [SerializeField] private ResourceScanner _resourceScanner;
    [SerializeField] private WorkerSpawner _workerSpawner;
    [SerializeField] private Collider _workerSpawnArea;
    private MotherbaseData _data = new MotherbaseData();

    private Collider _motherbaseCollider;

    private Coroutine _scannerCoroutine;
    private WaitForSeconds _scannerCooldown = new WaitForSeconds(0.5f);

    private Coroutine _buildingMotherbaseCoroutine;

    private Flag _currentFlag;
    private bool _isMotherbaseBuilding = false;

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

        for (int i = 0; i < _data.MinWorkersStartCount; i++)
        {
            _workerSpawner.SpawnWorker(_workerSpawnArea);
        }
    }

    private void Update()
    {
        if (_scannerCoroutine == null && _data.FoundResources.Count == 0)
        {
            StartScannerRoutine();
        }

        if (_currentFlag != null && _isMotherbaseBuilding == false && _data.AvailableResources >= _data.MinResourceToCreateBase)
        {
            SendWorkerBuildNewMotherbase();
        }

        if (_data.FoundResources.Count > 0 && _data.ResourcesInProgress.Count < _data.Workers.Count)
        {
            SendWorkerAtResource();
        }
    }

    public void Initialize(MotherbaseMediator mediator, ResourceRegistry resourceRegistry)
    {
        _data.Initialize(mediator, resourceRegistry);
    }

    public void SetBuildMotherbasePriority(Flag flag)
    {
        _currentFlag = flag;
        SendWorkerBuildNewMotherbase();
    }

    private IEnumerator ScanTheArea()
    {
        bool _isFound = false;

        while (_isFound == false)
        {
            List<Transform> foundResaources = _resourceScanner.GetResourceInRange();

            if (foundResaources.Count != 0)
            {
                _isFound = true;
            }

            _data.AddFoundResources(foundResaources);
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
        ResourceCountUpdated?.Invoke(_availableResources, _minWorkersStartCount);
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
