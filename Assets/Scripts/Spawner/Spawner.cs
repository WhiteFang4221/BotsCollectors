using UnityEngine;

public class Spawner<T> : MonoBehaviour where T : PoolableObject<T>
{
    [SerializeField] private Pool<T> _objectPool;

    public virtual void SpawnObject(Vector3 vector)
    {
        T createdObject = _objectPool.Get(vector);
        createdObject.Disabled += DestroyObject;
    }

    private void DestroyObject(T spawnableObject)
    {
        _objectPool.Return((T)spawnableObject);
        spawnableObject.Disabled -= DestroyObject;
    }
}
