using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[Serializable]
public class MotherbaseData : IShowPanel, IMotherbasePanelEvents
{
    private MotherbaseMediator _motherbaseMediator;
    private ResourceRegistry _resourceRegistry;
    private bool _isPanelOpen = false;
    public List<Worker> Workers { get; private set; } = new List<Worker>();
    public List<Transform> FoundResources { get; private set; } = new List<Transform>();
    public HashSet<Transform> ResourcesInProgress { get; private set; } = new HashSet<Transform>();
    public int AvailableResources { get; private set; } = 0;
    public int MinWorkersStartCount { get; private set; } = 3;
    public int MinResourceToCreateWorker { get; private set; } = 3;
    public int MinResourceToCreateBase { get; private set; } = 5;


    public event Action PanelOpened;
    public event Action PanelClosed;
    public event Action<int> WorkersCountUpdated;
    public event Action<int, int> ResourceCountUpdated;
    public void Initialize(MotherbaseMediator mediator, ResourceRegistry resourceRegistry)
    {
        _motherbaseMediator = mediator;
        _resourceRegistry = resourceRegistry;
    }

    public void AddFoundResources(List<Transform> foundResources)
    {
        FoundResources = foundResources.Where(resource => !_resourceRegistry.ResourceInProgress.Contains(resource)).ToList();
        _resourceRegistry.RegisterResources(FoundResources);
    }

    public void HidePanel()
    {
        _isPanelOpen = true;
        _motherbaseMediator.SubscribeToEvents(this);
        PanelOpened?.Invoke();
        ResourceCountUpdated?.Invoke(AvailableResources, MinResourceToCreateWorker);
        WorkersCountUpdated?.Invoke(Workers.Count);
        _motherbaseMediator.WorkerCreateStarted += CreateWorker;
        _motherbaseMediator.BaseCreateStarted += SetTheFlag;
    }


    public void ShowPanel()
    {
        _isPanelOpen = false;
        _motherbaseMediator.WorkerCreateStarted -= CreateWorker;
        _motherbaseMediator.BaseCreateStarted -= SetTheFlag;
        PanelClosed?.Invoke();
        _motherbaseMediator.UnscribeToEvents(this);
    }
}
