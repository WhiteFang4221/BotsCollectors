using Assets.Scripts.StateMachine;
using UnityEngine;

public class MoveToMotherbaseState : WorkerState
{
    private Transform _transform;
    private Vector3 _target;

    private Collider _motherbaseCollider;

    private float _stopDistance = 1f;
    private float _offsetRadius = 2f;

    public MoveToMotherbaseState(IStateSwitcher stateSwitcher, WorkerStateMachineData data, Worker worker) : base(stateSwitcher, data, worker)
    {
    }

    public override void Enter()
    {
        _transform = Worker.transform;
        _motherbaseCollider = Worker.MotherbaseCollider;
        _target = SetTargetPosition();
        Worker.Agent.isStopped = false;
        Worker.Agent.stoppingDistance = _stopDistance;
    }

    public override void Exit()
    {
    }

    public override void Update()
    {

        if (Vector3.Distance(_transform.position, _target) <= Data.MinDistanceToMotherbase)
        {
            Worker.Agent.isStopped = true;
            Worker.Agent.velocity = Vector3.zero;
            StateSwitcher.SwitchState<GiveResourceState>();
        }

        Worker.Agent.SetDestination(_target);
    }

    private Vector3 SetTargetPosition()
    {
        {
            Vector3 closestPoint = _motherbaseCollider.ClosestPoint(_transform.position);

            Vector3 randomOffset = Random.insideUnitSphere * _offsetRadius;
            randomOffset.y = 0;

            return closestPoint + randomOffset;
        }
    }
}
