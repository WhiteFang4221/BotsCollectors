using System;

public interface IFlagSetter
{
    public Flag CurrentFlag { get; }

    public event Action<IFlagSetter, Flag> FlagGot;
    public event Action<IFlagSetter> FlagSetterDisabled;

    public void SetBuildMotherbasePriority(Flag flag);
}
