using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ResourceScanner : MonoBehaviour
{
    [SerializeField] private SphereCollider _scanArea;

    public List<Transform> GetResourceInRange()
    {
        List<Transform> foundResources = new List<Transform>();

        Vector3 sphereCenter = _scanArea.transform.position + _scanArea.center * _scanArea.transform.lossyScale.x;
        float radius = _scanArea.radius * _scanArea.transform.lossyScale.x;
        Collider[] hitColliders = Physics.OverlapSphere(sphereCenter, radius);

        foreach (Collider collider in hitColliders)
        {
            if(collider.TryGetComponent(out Resource resource))
            {
                foundResources.Add(collider.transform);
            }
        }

        return foundResources.OrderBy(resource => Vector3.Distance(transform.position, resource.position)).ToList();
    }
}


