using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ResourceScanner : MonoBehaviour
{
    [SerializeField] private SphereCollider _scanArea;
    [SerializeField] private LayerMask _resourceMask;

    public List<Transform> GetResourceInRange()
    {
        List<Transform> foundResources = new List<Transform>();

        Vector3 sphereCenter = _scanArea.transform.position + _scanArea.center * _scanArea.transform.lossyScale.x;
        float radius = _scanArea.radius * _scanArea.transform.lossyScale.x;
        Collider[] hitColliders = Physics.OverlapSphere(sphereCenter, radius, _resourceMask);

        foreach (Collider collider in hitColliders)
        {
            if(collider.TryGetComponent(out Resource resource))
            {
                foundResources.Add(collider.transform);
            }
        }

        return foundResources.OrderBy(resource => resource.transform.position.SqrDistance(resource.position)).ToList();
    }
}


