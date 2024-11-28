using UnityEngine;

public abstract class Spawner<T> : MonoBehaviour where T : PoolableObject<T>
{
    [SerializeField] private Pool<T> _objectPool;

    protected Transform ParentTransform => _objectPool.ParentTransform;

    protected virtual T SpawnObject(Vector3 vector)
    {
        T createdObject = _objectPool.Get(vector);
        createdObject.Disabled += DestroyObject;
        return createdObject;
    }

    private void DestroyObject(T spawnableObject)
    {
        _objectPool.Release(spawnableObject);
        spawnableObject.Disabled -= DestroyObject;
    }
}
