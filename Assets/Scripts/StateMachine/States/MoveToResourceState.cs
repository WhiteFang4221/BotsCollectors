using Assets.Scripts.StateMachine;
using UnityEngine;

public class MoveToResourceState : WorkerState
{
    private Transform _transform;
    private Transform _target;

    public MoveToResourceState(IStateSwitcher stateSwitcher, WorkerStateMachineData data, Worker worker) : base(stateSwitcher, data, worker)
    {
    }

    public override void Enter()
    {
        Debug.Log("MoveToResourceState");
        Worker.Agent.isStopped = false;
        _transform = Worker.transform;
        _target = Worker.TargetTransform;
    }


    public override void Update()
    {
        if (Vector3.Distance(_transform.position, _target.position) <= Data.MinDistanceToResource)
        {
            Worker.Agent.isStopped = true;
            Worker.Agent.velocity = Vector3.zero;
            StateSwitcher.SwitchState<LoadingResourceState>();
        }

        Worker.Agent.SetDestination(_target.position);
    }

    public override void Exit() { }
}
