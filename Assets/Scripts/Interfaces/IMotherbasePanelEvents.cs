using System;

public interface IMotherbasePanelEvents
{
    public event Action PanelOpened;
    public event Action PanelClosed;
    public event Action<int> WorkersCountUpdated;
    public event Action<int, int> ResourceCountUpdated;
}
