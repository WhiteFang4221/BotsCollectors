using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class Motherbase : MonoBehaviour
{
    [SerializeField] private ResourceScanner _resourceScanner;
    [SerializeField] private WorkerSpawner _workerSpawner;
    [SerializeField] private Collider _workerSpawnArea;
    [SerializeField] private float _availableResources = 0;

    private Collider _baseCollider;
    private List<Worker> _workers = new List<Worker>();
    private int _minWorkersCount = 3;

    private List<Transform> _foundResources = new List<Transform>();
    private HashSet<Transform> _resourcesInProgress = new HashSet<Transform>();

    private void OnEnable()
    {
        _workerSpawner.WorkerSpawned += AddWorker;
    }

    private void Start()
    {
        _baseCollider = GetComponent<Collider>();

        for (int i = 0; i < _minWorkersCount; i++)
        {
            _workerSpawner.SpawnWorker(_workerSpawnArea);
        }
    }

    private void Update()
    {
        if (_workers.Count == 0)
        {
            return;
        }

        if (_foundResources.Count > 0 && _resourcesInProgress.Count < _workers.Count)
        {
            SendWorkerAtResource();
        }
        else
        {
            ScanTheArea();
        }
    }

    private void SendWorkerAtResource()
    {
        foreach (var worker in _workers)
        {
            if (worker.IsBusy == false)
            {
                foreach (Transform resource in _foundResources)
                {
                    if (_resourcesInProgress.Contains(resource) == false)
                    {
                        _resourcesInProgress.Add(resource);
                        worker.ResourceHasGiven += OnResourceCollected;
                        worker.SetTargetResource(resource);
                        break;
                    }
                }
            }
        }
    }

    private void ScanTheArea()
    {
        _foundResources = _resourceScanner.GetResourceInRange();
    }

    private void ReceiveResource()
    {
        _availableResources++;
    }

    private void AddWorker(Worker worker)
    {
        _workers.Add(worker);
        worker.InitializeMotherbase(_baseCollider);
    }

    private void OnResourceCollected(Worker worker, Transform resource)
    {
        _foundResources.Remove(resource);
        _resourcesInProgress.Remove(resource);
        worker.ResourceHasGiven -= OnResourceCollected;
        ReceiveResource();

        if (resource.TryGetComponent(out Resource component))
        {
            component.BackToPool();
            component.Disable();
        }
    }
}
