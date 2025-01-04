using System;

public interface IFlagKeeper
{
    public event Action<IFlagKeeper> FlagGot;
    public event Action<IFlagKeeper> FlagKeeperDisabled;

    public void SetBuildMotherbasePriority(Flag flag);
}
