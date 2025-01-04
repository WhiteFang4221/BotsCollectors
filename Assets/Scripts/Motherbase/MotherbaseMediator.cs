using System;
using UnityEngine;

public class MotherbaseMediator : MonoBehaviour
{
    [SerializeField] private MotherbasePanel _panel;

    public event Action WorkerCreateStarted;
    public event Action BaseCreateStarted;

    private void OnEnable()
    {
        _panel.CreateWorkerButtonClicked += OnCreateWorker;
        _panel.CreateBaseButtonClicked += OnCreateBase;
    }

    private void OnDisable()
    {
        _panel.CreateWorkerButtonClicked -= OnCreateWorker;
        _panel.CreateBaseButtonClicked -= OnCreateBase;
    }

    public void SubscribeToEvents(IMotherbasePanelEvents motherbase)
    {
        motherbase.PanelOpened += OpenPanel;
        motherbase.PanelClosed += ClosePanel;
        motherbase.ResourceCountUpdated += UpdateResourcesCount;
        motherbase.WorkersCountUpdated += UpdateWorkersCount;
    }

    public void UnscribeToEvents(IMotherbasePanelEvents motherbase)
    {
        motherbase.PanelOpened -= OpenPanel;
        motherbase.PanelClosed -= ClosePanel;
        motherbase.ResourceCountUpdated -= UpdateResourcesCount;
        motherbase.WorkersCountUpdated -= UpdateWorkersCount;
    }

    private void OpenPanel()
    {
        _panel.Show();
    }

    private void ClosePanel()
    {
        _panel.Hide();
    }

    private void UpdateWorkersCount(int workersCount)
    {
        _panel.UpdateWorkers(workersCount);
    }

    private void UpdateResourcesCount(int resourceCount, int minResourceCount)
    {
        _panel.UpdateResources(resourceCount, minResourceCount);
    }

    private void OnCreateWorker()
    {
        WorkerCreateStarted?.Invoke();
    }

    private void OnCreateBase()
    {
        BaseCreateStarted?.Invoke();
    }
}
