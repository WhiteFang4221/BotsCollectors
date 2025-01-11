using UnityEngine;

public class MotherbaseBuilder : MonoBehaviour
{
    [SerializeField] Vector3 _spawnOffsetPosition = new Vector3(0, 3, 3);
    private MotherbaseSpawner _motherbaseSpawner;


    public void Initialize(MotherbaseSpawner spawner)
    {
        _motherbaseSpawner = spawner;
    }

    public void ListenMotherbase(IWorkerEmployer workerEmployer)
    {
        workerEmployer.WorkerSent += ListenBaseBuilder;
    }

    private void ListenBaseBuilder(IBaseBuilder baseBuilder)
    {
        baseBuilder.ReachedFlag += BuildNewMotherbase;
    }

    private void BuildNewMotherbase(Transform flagTransform, IBaseBuilder baseBuilder)
    {
        Destroy(flagTransform.gameObject);
        Vector3 spawnPoint = flagTransform.position + _spawnOffsetPosition;
        IWorkerEmployer workerEmployer = _motherbaseSpawner.SpawnMotherbase(spawnPoint);
        workerEmployer.HireWorker((Worker)baseBuilder);
    }
}
