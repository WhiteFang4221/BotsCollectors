using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ResourceScanner : MonoBehaviour
{
    [SerializeField] private LayerMask _resourceLayer;
    [SerializeField] private SphereCollider _scanArea;

    private List<Transform> _foundResources = new List<Transform>(); 

    public List<Transform> GetResourceInRange()
    {
        _foundResources.Clear();

        Vector3 sphereCenter = _scanArea.transform.position + _scanArea.center * _scanArea.transform.lossyScale.x;
        float radius = _scanArea.radius * _scanArea.transform.lossyScale.x;
        Collider[] hitColliders = Physics.OverlapSphere(sphereCenter, radius);

        foreach (Collider collider in hitColliders)
        {
            if(collider.TryGetComponent(out Resource resource))
            {
                _foundResources.Add(collider.transform);
            }
        }

        return _foundResources.OrderBy(resource => Vector3.Distance(transform.position, resource.position)).ToList();
    }
}


