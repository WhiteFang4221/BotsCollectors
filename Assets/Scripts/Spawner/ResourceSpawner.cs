using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceSpawner : Spawner<Resource>
{
    [SerializeField] int _spawnCount;
    [SerializeField] private float _minDistance = 1f;
    private BoxCollider _spawnArea;

    private List<Vector3> _spawnedPositions = new List<Vector3>();
    private WaitForSeconds _generateDelay = new WaitForSeconds(10f);

    private float _dividerSpawnArea = 2f;

    private void Awake()
    {
        _spawnArea = GetComponent<BoxCollider>();
    }

    public void StartGenerateResources()
    {
        StartCoroutine(GenerateResources());
    }

    private IEnumerator GenerateResources()
    {
        while (true)
        {

            if (IsSpawnAreaEmpty() == true)
            {
                _spawnedPositions.Clear();
                GenerateValidPositions();
                SpawnResources();
            }

            yield return _generateDelay;
        }
    }

    private void GenerateValidPositions()
    {
        for (int i = 0; i < _spawnCount; i++)
        {
            Vector3 newPosition;

            do
            {
                newPosition = GetRandomPosition();
            }
            while (IsPositionValid(newPosition) == false);

            _spawnedPositions.Add(newPosition);
        }
    }

    private bool IsSpawnAreaEmpty()
    {
        Vector3 spawnAreaCenter = transform.position + _spawnArea.center;
        Vector3 spawnAreaSize = _spawnArea.size;
        Collider[] colliders = Physics.OverlapBox(spawnAreaCenter, spawnAreaSize / _dividerSpawnArea, Quaternion.identity);

        foreach (Collider collider in colliders)
        {
            if (collider.TryGetComponent(out Resource resource))
            {
                return false;
            }
        }

        return true;
    }

    private bool IsPositionValid(Vector3 position)
    {
        foreach (var existingPosition in _spawnedPositions)
        {
            if (position.IsEnoughClose(existingPosition, _minDistance))
            {
                return false; 
            }
        }

        return true;
    }

    private void SpawnResources()
    {
        foreach (var position in _spawnedPositions)
        {
            Resource resource = SpawnObject(position);
            resource.Disabled += OnResourceDisabled;
        }
    }

    private Vector3 GetRandomPosition()
    {
        Vector3 spawnAreaSize = _spawnArea.size;
        Vector3 spawnAreaCenter = _spawnArea.center + transform.position;

        float randomX = Random.Range(-spawnAreaSize.x / _dividerSpawnArea, spawnAreaSize.x / _dividerSpawnArea);
        float randomZ = Random.Range(-spawnAreaSize.z / _dividerSpawnArea, spawnAreaSize.z / _dividerSpawnArea);

        return spawnAreaCenter + new Vector3(randomX, 0, randomZ);
    }

    private void OnResourceDisabled(Resource resource)
    {
        resource.transform.SetParent(ParentTransform);
        resource.Disabled -= OnResourceDisabled;
    }
}
