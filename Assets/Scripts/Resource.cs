using UnityEngine;

public class Resource : PoolableObject<Resource>
{
    private Transform _parentTransform;

    private void Start()
    {
        _parentTransform = transform.parent;
    }

    public void BackToPool()
    {
        transform.SetParent(_parentTransform);
    }
}
