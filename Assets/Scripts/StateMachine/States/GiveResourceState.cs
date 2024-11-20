using Assets.Scripts.StateMachine;
using UnityEngine;

public class GiveResourceState : WorkerState
{
    private Transform _resourceTransform;
    private Transform _motherbaseTransform;
    private float _speedGiving;
    private float _tolerance = 0.01f;

    public GiveResourceState(IStateSwitcher stateSwitcher, WorkerStateMachineData data, Worker worker) : base(stateSwitcher, data, worker)
    {
    }

    public override void Enter()
    {
        _resourceTransform = Worker.TargetTransform;
        _motherbaseTransform = Worker.MotherbasePosition;
        float distance = Vector3.Distance(_resourceTransform.position, _motherbaseTransform.position);
        _speedGiving = distance / Data.DurationLoadingResource;
    }

    public override void Exit()
    {

    }

    public override void Update()
    {
        if (Vector3.Distance(_resourceTransform.position, _motherbaseTransform.position) < _tolerance)
        {
            Worker.GiveResourceToBase(_resourceTransform);
            StateSwitcher.SwitchState<IdlingState>();
        }
        else
        {
            _resourceTransform.position = Vector3.MoveTowards(_resourceTransform.position, _motherbaseTransform.position, _speedGiving * Time.deltaTime);
        }
    }
}
