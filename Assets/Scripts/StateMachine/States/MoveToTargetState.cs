using UnityEngine;

public class MoveToTargetState : WorkerState
{
    private Transform _transform;
    private Transform _target;

    public MoveToTargetState(IStateSwitcher stateSwitcher, WorkerStateMachineData data, Worker worker) : base(stateSwitcher, data, worker) { }

    public override void Enter()
    {
        Worker.Agent.isStopped = false;
        Worker.Agent.speed = Data.Speed;
        _transform = Worker.transform;
        _target = Worker.TargetTransform;
    }

    public override void Update()
    {
        if (_transform.position.IsEnoughClose(_target.position, Data.MinDistanceToResource))
        {
            Worker.Agent.isStopped = true;
            Worker.Agent.velocity = Vector3.zero;

            if (_target.gameObject.TryGetComponent(out Resource resource))
            {
                StateSwitcher.SwitchState<LoadingResourceState>();
            }
            else if (_target.gameObject.TryGetComponent(out Flag flag))
            {
                StateSwitcher.SwitchState<BuildingNewMotherbaseState>();
            }
        }

        Worker.Agent.SetDestination(_target.position);
    }

    public override void Exit() { }
}
