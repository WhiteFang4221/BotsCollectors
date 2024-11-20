using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class Motherbase : MonoBehaviour
{
    [SerializeField] private List<Worker> _workers;
    [SerializeField] private ResourceScanner _resourceScanner;

    private List<Transform> _foundResources = new List<Transform>();
    private HashSet<Transform> _resourcesInProgress = new HashSet<Transform>();

    private Collider _collider;

    private float _availableResources = 0;

    private bool _isWork = true;

    private WaitForSeconds _scanDelay = new WaitForSeconds(10f); 

    private void Awake()
    {
        StartCoroutine(ScaningAreaRoutine());
    }

    private void Start()
    {
        _collider = GetComponent<Collider>();
    }

    private void Update()
    {
        if (_foundResources.Count > 0)
        {
            SendWorkerAtResource();
        }  
    }

    private void SendWorkerAtResource()
    {
        foreach (var worker in _workers)
        {
            if (worker.IsBusy == false)
            {
                worker.SetMotherbaseData(_collider, transform);
                foreach (Transform resource in _foundResources)
                {
                    if (_resourcesInProgress.Contains(resource) == false)
                    {
                        _resourcesInProgress.Add(resource);
                        worker.ResourceCollected += OnResourceCollected;
                        worker.SetTargetResource(resource);
                        break;
                    }
                }
            }
        }
    }

    private void OnResourceCollected(Worker worker, Transform resource)
    {
        _resourcesInProgress.Remove(resource);
        _foundResources.Remove(resource);
        worker.ResourceCollected -= OnResourceCollected;
        ReceiveResource();
        Destroy(resource.gameObject);
    }

    private void ScanTheArea()
    {
        _foundResources = _resourceScanner.GetResourceInRange();
        Debug.Log($"{_foundResources.Count} В области");
    }

    private IEnumerator ScaningAreaRoutine()
    {
        while (_isWork)
        {
            ScanTheArea();  
            yield return _scanDelay;
        }
    }

    private void ReceiveResource()
    {
        _availableResources++;
    }
}
