using Assets.Scripts.StateMachine;
using UnityEngine;

public class IdlingState : WorkerState
{
    public IdlingState(IStateSwitcher stateSwitcher, WorkerStateMachineData data, Worker worker) : base(stateSwitcher, data, worker)
    {
    }

    public override void Enter()
    {
        Debug.Log("IdlingState");
    }

    public override void Exit()
    {
    }

    public override void Update()
    {
        if (Worker.TargetTransform != null)
        {
            StateSwitcher.SwitchState<MoveToResourceState>();
        }
    }
}
