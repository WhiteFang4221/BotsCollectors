using System;
using UnityEngine;

public class PoolableObject<T> : MonoBehaviour where T : PoolableObject<T>
{
    public event Action<T> Disabled;

    public void Disable()
    {
        gameObject.SetActive(false);
        Disabled.Invoke((T)this);
    }
}
