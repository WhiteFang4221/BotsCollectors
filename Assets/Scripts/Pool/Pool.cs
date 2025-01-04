using System.Collections.Generic;
using UnityEngine;

public abstract class Pool<T> : MonoBehaviour where T : PoolableObject<T>
{
    [SerializeField] private T _prefab;

    [field: SerializeField]
    public Transform ParentTransform {  get; private set; }

    private Queue<T> _pool = new Queue<T>();

    public T Get(Vector3 vector)
    {
        if (_pool.Count == 0)
        {
            ExpandPool();
        }

        T entity = _pool.Dequeue();
        entity.gameObject.SetActive(true);
        entity.transform.position = vector;

        return entity;
    }

    public virtual void Release(T entity)
    {
        entity.gameObject.SetActive(false);
        _pool.Enqueue(entity);
    }

    private void BackToParentTransform(T entity)
    {
        entity.transform.SetParent(ParentTransform);
    }

    private void ExpandPool()
    {
        T entity = Instantiate(_prefab, ParentTransform);
        _pool.Enqueue(entity);
    }

}

