using System.Collections.Generic;
using System.Linq;

public class WorkerStateMachine : IStateSwitcher
{
    private List<IState> _states;
    private IState _currentState;
    

    public WorkerStateMachine(Worker worker)
    {
        WorkerStateMachineData data = new WorkerStateMachineData();

        _states = new List<IState>()
        {
            new IdlingState(this, data, worker),
            new MoveToResourceState(this, data, worker),
            new LoadingResourceState(this, data, worker),
            new MoveToMotherbaseState(this, data, worker),
            new GiveResourceState(this, data, worker),
        };

        _currentState = _states[0];
        _currentState.Enter();
    }

    public void SwitchState<State>() where State : IState
    {
        IState state = _states.FirstOrDefault(state => state is State);

        _currentState.Exit();
        _currentState = state;
        _currentState.Enter();
    }

    public void Update() => _currentState.Update();
}
