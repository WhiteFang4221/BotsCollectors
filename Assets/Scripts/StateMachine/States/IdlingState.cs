public class IdlingState : WorkerState
{
    public IdlingState(IStateSwitcher stateSwitcher, WorkerStateMachineData data, Worker worker) : base(stateSwitcher, data, worker) { }

    public override void Enter() { }

    public override void Update()
    {
        if (Worker.TargetTransform != null)
        {
            StateSwitcher.SwitchState<MoveToTargetState>();
        }
    }

    public override void Exit() { }
}
