﻿namespace Assets.Scripts.StateMachine
{
    public interface IStateSwitcher
    {
        void SwitchState<State>() where State : IState;
    }
}
