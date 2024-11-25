using UnityEngine;

public abstract class Spawner<T> : MonoBehaviour where T : PoolableObject<T>
{
    [SerializeField] private Pool<T> _objectPool;

    protected virtual T SpawnObject(Vector3 vector)
    {
        T createdObject = _objectPool.Get(vector);
        createdObject.Disabled += DestroyObject;
        return createdObject;
    }

    private void DestroyObject(T spawnableObject)
    {
        _objectPool.Return((T)spawnableObject);
        spawnableObject.Disabled -= DestroyObject;
    }
}
