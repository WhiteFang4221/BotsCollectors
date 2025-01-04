using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MotherbasePanel : MonoBehaviour
{
    [SerializeField] private  TextMeshProUGUI _label;
    [SerializeField] private TextMeshProUGUI _resourceCountText;
    [SerializeField] private TextMeshProUGUI _workersCountText;
    [SerializeField] private Button _createWorkerButton;
    [SerializeField] private Button _createMotherbaseButton;

    private string _initialLabelText = "База";
    private string _initialResourceText = "Ресурсы: ";
    private string _initialWorkerText = "Работники: ";

    public event Action CreateWorkerButtonClicked;
    public event Action CreateBaseButtonClicked;

    private void Awake()
    {
        _label.text = _initialLabelText;
        _createWorkerButton.interactable = false;
    }

    private void OnEnable()
    {
        _createWorkerButton.onClick.AddListener(OnCreateWorkerButtonClick);
        _createMotherbaseButton.onClick.AddListener(OnCreateMotherbaseButtonClick);
    }

    private void OnDisable()
    {
        _createWorkerButton.onClick.RemoveListener(OnCreateWorkerButtonClick);
        _createMotherbaseButton.onClick.RemoveListener(OnCreateMotherbaseButtonClick);
    }

    public void UpdateResources(int resourceCount, int minResourceCount)
    {
        _resourceCountText.text = $"{_initialResourceText} {resourceCount}";

        if (resourceCount >= minResourceCount)
        {
            _createWorkerButton.interactable = true;
        }
        else
        {
            _createWorkerButton.interactable = false; 
        }
    }

    public void UpdateWorkers(int workersCount)
    {
        _workersCountText.text = $"{_initialWorkerText} {workersCount}";
    }

    public void Show() => gameObject.SetActive(true);
    public void Hide() => gameObject.SetActive(false);

    private void OnCreateWorkerButtonClick()
    {
        CreateWorkerButtonClicked?.Invoke();
    }

    private void OnCreateMotherbaseButtonClick()
    {
        CreateBaseButtonClicked?.Invoke();
    }
}
