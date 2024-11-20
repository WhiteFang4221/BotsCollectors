using System.Collections;
using UnityEngine;

public class ResourceSpawner : Spawner<Resource>
{
    [SerializeField] int _spawnCount;
    private BoxCollider _spawnArea;

    private float _spawnRadius = 0.5f;
    private float _dividerSpawnArea = 2f;

    private WaitForSeconds _spawnDelay = new WaitForSeconds(1f);

    private void Awake()
    {
        _spawnArea = GetComponent<BoxCollider>();
    }

    private void Start()
    {
        SpawnResource();
    }

    private void SpawnResource()
    {
        int spawnedCount = 0;

        while (spawnedCount < _spawnCount)
        {
            Vector3 randomPosition = GetSpawnArea();

            // Проверяем, есть ли объекты в месте спавна
            if (!Physics.CheckSphere(randomPosition, _spawnRadius))
            {
                SpawnObject(randomPosition);
                spawnedCount++;
            }
        }
    }

    private Vector3 GetSpawnArea()
    {
        Vector3 spawnAreaSize = _spawnArea.size;
        Vector3 spawnAreaCenter = _spawnArea.center + transform.position;
        float randomX = Random.Range(-spawnAreaSize.x / _dividerSpawnArea, spawnAreaSize.x / _dividerSpawnArea);
        float randomZ = Random.Range(-spawnAreaSize.z / _dividerSpawnArea, spawnAreaSize.z / _dividerSpawnArea);
        return spawnAreaCenter + new Vector3(randomX, 0, randomZ);
    }
}
