using System.Collections.Generic;
using UnityEngine;

public class WorkerTrunk : MonoBehaviour
{
    public List<Transform> Resources { get; private set; } = new List<Transform>();

    public void AddResource(Transform transform)
    {
        Resources.Add(transform);
    }

    public void RemoveResource(Transform transform)
    {
        Resources.Remove(transform);
    }
}
