<<<<<<< HEAD
=======
using System;
>>>>>>> parent of ce86695 (Все сломалось)
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

<<<<<<< HEAD
=======
        if (hitColliders.Length == 0)
        {
            return foundResources;
        }

>>>>>>> parent of ce86695 (Все сломалось)
        foreach (Collider collider in hitColliders)
        {
            if(collider.TryGetComponent(out Resource resource))
            {
                foundResources.Add(collider.transform);
            }
        }

<<<<<<< HEAD
        return foundResources.OrderBy(resource => resource.transform.position.SqrDistance(resource.position)).ToList();
=======
        foundResources = foundResources.Where(resource => resource.gameObject.activeInHierarchy).OrderBy(resource => resource.transform.position.SqrDistance(resource.position)).ToList();

        return foundResources;
>>>>>>> parent of ce86695 (Все сломалось)
    }
}


