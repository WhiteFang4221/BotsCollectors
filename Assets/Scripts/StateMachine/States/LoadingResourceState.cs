using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class LoadingResourceState : WorkerState
{
    private Transform _resourceTransform;
    private Transform _trunkTransform;
    private float _speedLoading;
    private float _tolerance = 0.01f;

    public LoadingResourceState(IStateSwitcher stateSwitcher, WorkerStateMachineData data, Worker worker) : base(stateSwitcher, data, worker) { }

    public override void Enter()
    {
        _resourceTransform = Worker.TargetTransform;
        _trunkTransform = Worker.Trunk.transform;
        float distance = Vector3.Distance(_resourceTransform.position, _trunkTransform.position);
        _speedLoading = distance / Data.DurationLoadingResource;
    }

    public override void Update()
    {
        if (_resourceTransform.position.IsEnoughClose(_trunkTransform.position, _tolerance))
        {
            _resourceTransform.SetParent(_trunkTransform);
            Worker.Trunk.AddResource(_resourceTransform);
            StateSwitcher.SwitchState<MoveToMotherbaseState>();
        }
        else
        {
            _resourceTransform.position = Vector3.MoveTowards(_resourceTransform.position, _trunkTransform.position, _speedLoading * Time.deltaTime);
            _resourceTransform.rotation = Quaternion.Lerp(_resourceTransform.rotation, _trunkTransform.rotation, _speedLoading * Time.deltaTime);
        }
    }

    public override void Exit() { }
}
