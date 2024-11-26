using System;
using UnityEngine;

public class PoolableObject<T> : MonoBehaviour where T : PoolableObject<T>
{
    private Transform _parentTransform;
    public event Action<T> Disabled;

    private void Start()
    {
        _parentTransform = transform.parent;
    }

    public void Disable()
    {
        gameObject.SetActive(false);
        Disabled.Invoke((T)this);
    }

    public void BackToPool()
    {
        transform.SetParent(_parentTransform);
    }
}
