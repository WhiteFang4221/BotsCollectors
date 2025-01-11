
public class BuildingMotherbaseState : WorkerState
{
    public BuildingMotherbaseState(IStateSwitcher stateSwitcher, WorkerStateMachineData data, Worker worker) : base(stateSwitcher, data, worker)
    {
    }

    public override void Enter()
    {
        Worker.ReportReachTarget();
        StateSwitcher.SwitchState<MoveToMotherbaseState>();
    }

    public override void Update()
    {

    }

    public override void Exit() { }
}
