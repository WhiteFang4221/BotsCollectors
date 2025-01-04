using System;
using UnityEngine;

public class WorkerSpawner : Spawner<Worker>
{
    private float _checkRadius = 5f;

    public event Action<Worker> WorkerSpawned;

    public void SpawnWorker(Collider spawnCollider)
    {
        Vector3 newPosition;

        do
        {
            newPosition = GetRandomPosition(spawnCollider);
        }
        while (IsPositionOccupied(newPosition));

        Worker worker = SpawnObject(newPosition);
        WorkerSpawned?.Invoke(worker);
    }

    private bool IsPositionOccupied(Vector3 position)
    {
        Collider[] colliders = Physics.OverlapSphere(position, _checkRadius);

        foreach (var collider in colliders)
        {
            if (collider.TryGetComponent(out Worker worker) == true)
            {
                return true;
            }
        }

        return false;
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
