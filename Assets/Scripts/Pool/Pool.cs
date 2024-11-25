using System.Collections.Generic;
using UnityEngine;

public abstract class Pool<T> : MonoBehaviour where T : MonoBehaviour
{
    [SerializeField] private T _prefab;
    [SerializeField] private Transform _parentTransform;

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

    public void Return(T entity)
    {
        entity.gameObject.SetActive(false);
        _pool.Enqueue(entity);
    }

    private void ExpandPool()
    {
        T entity = Instantiate(_prefab, _parentTransform);
        _pool.Enqueue(entity);
    }
}

