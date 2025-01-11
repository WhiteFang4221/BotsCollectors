using System;

public interface IWorkerEmployer 
{
    event Action<IBaseBuilder> WorkerSent;

    public void HireWorker(Worker worker);
}
