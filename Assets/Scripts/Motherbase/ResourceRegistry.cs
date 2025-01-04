using System.Collections.Generic;
using UnityEngine;

public class ResourceRegistry : MonoBehaviour
{
    public HashSet<Transform> RegistredResources { get; private set; } = new HashSet<Transform>();
    public HashSet<Transform> ResourceInProgress { get; private set; } = new HashSet<Transform>();

    public void RegisterResources(List<Transform> resources)
    {
        RegistredResources.UnionWith(resources);
    }

    public void RegisterResourceInProgress(Transform resource)
    {
        ResourceInProgress.Add(resource);
    }

    public void UnregisterResourceInProgress(Transform resource)
    {
        ResourceInProgress.Remove(resource);
    }

    public bool TryReserveResource(Transform resource)
    {
      return RegistredResources.Remove(resource);
    }
}
