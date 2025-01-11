using System;
using UnityEngine;

public class WorkerSpawner : Spawner<Worker>
{
    public event Action<Worker> WorkerSpawned;

    public void InitializePool(WorkerPool pool)
    {
        base.InitializePool(pool);
    }

    public void SpawnWorker(Collider spawnCollider)
    {
        Vector3 newPosition;
        newPosition = GetRandomPosition(spawnCollider);
        Worker worker = SpawnObject(newPosition);
        WorkerSpawned?.Invoke(worker);
    }

    private Vector3 GetRandomPosition(Collider spawnCollider)
    {
        Vector3 spawnAreaSize = spawnCollider.bounds.size;
        Vector3 spawnAreaCenter = spawnCollider.bounds.center;

        float randomX = UnityEngine.Random.Range(spawnAreaCenter.x - spawnAreaSize.x / 2f, spawnAreaCenter.x + spawnAreaSize.x / 2f);
        float randomZ = UnityEngine.Random.Range(spawnAreaCenter.z - spawnAreaSize.z / 2f, spawnAreaCenter.z + spawnAreaSize.z / 2f);

        return new Vector3(randomX, 0, randomZ);
    }
}
