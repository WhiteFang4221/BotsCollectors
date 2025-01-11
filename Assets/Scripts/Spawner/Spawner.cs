using UnityEngine;

public abstract class Spawner<T> : MonoBehaviour where T : PoolableObject<T>
{
    [SerializeField] private Pool<T> _objectPool;

    protected Transform ParentTransform => _objectPool.ParentTransform;

    protected void InitializePool(Pool<T> pool)
    {
        _objectPool = pool;
    }

    protected virtual T SpawnObject(Vector3 vector)
    {
        T createdObject = _objectPool.Get(vector);
        createdObject.Disabled += DestroyObject;
        return createdObject;
    }

    protected virtual void DestroyObject(T spawnableObject)
    {
        _objectPool.Release(spawnableObject);
        spawnableObject.Disabled -= DestroyObject;
    }
}
