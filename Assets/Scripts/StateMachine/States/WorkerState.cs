using Assets.Scripts.StateMachine;

public abstract class WorkerState : IState
{
    protected readonly IStateSwitcher StateSwitcher;
    protected readonly WorkerStateMachineData Data;
    protected readonly Worker Worker;

    public WorkerState(IStateSwitcher stateSwitcher, WorkerStateMachineData data, Worker worker)
    {
        StateSwitcher = stateSwitcher;
        Data = data;
        Worker = worker;
    }

    public abstract void Enter();
    public abstract void Update();
    public abstract void Exit();


}
