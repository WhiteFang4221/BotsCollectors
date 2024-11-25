using System;

public class WorkerStateMachineData
{
    private float _speed = 15f;

    public float Speed
    {
        get => _speed;
        set
        {
            if (value < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(value));
            }

            _speed = value;
        }
    }
    public float DurationLoadingResource { get; private set; } = 1f;
    public float MinDistanceToResource { get; private set; } = 3f;
    public float MinDistanceToMotherbase { get; private set; } = 3f;
}
