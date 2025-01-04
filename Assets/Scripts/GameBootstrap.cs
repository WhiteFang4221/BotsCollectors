using System.Collections.Generic;
using UnityEngine;

public class GameBootstrap : MonoBehaviour
{
    [SerializeField] private List<ResourceSpawner> _resourceSpawners;

    private void Start()
    {
        GenerateResources();
    }

    private void GenerateResources()
    {
        foreach (var generator in _resourceSpawners)
        {
            generator.StartGenerateResources();
        }
    }
}
