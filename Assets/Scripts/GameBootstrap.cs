using System.Collections.Generic;
using UnityEngine;

public class GameBootstrap : MonoBehaviour
{
    [SerializeField] private CameraMovement _cameraMovement;
    [SerializeField] private CursorIntaractionHandler _cursorIntaractionHandler;
    [SerializeField] private Camera _camera;
    [SerializeField] private LayerMask _cursorIgnoreRaycastLayers;
    [SerializeField] private List<Motherbase> _motherbases;
    [SerializeField] private List<ResourceSpawner> _resourceSpawners;
    [SerializeField] private WorkerPool _workerPool;
    [SerializeField] private MotherbaseSpawner _motherbaseSpawner;
    [SerializeField] private MotherbaseBuilder _motherbaseBuilder;
    [SerializeField] private MotherbaseMediator _motherbaseMediator;
    [SerializeField] private ResourceRegistry _resourceRegistry;
    [SerializeField] private FlagPlacer _flagPlacer;

    private GameInput _input;

    private InputHandler _inputHandler;

    private void Awake()
    {
        _input = new GameInput();
        _input.Enable();
        _inputHandler = new InputHandler(_camera, _input, _cursorIgnoreRaycastLayers);
        _flagPlacer.Initialize(_inputHandler);
        _cameraMovement.Initialize(_inputHandler);
        _cursorIntaractionHandler.Initialize(_inputHandler);
        InitializeMotherbases();
        _motherbaseBuilder.Initialize(_motherbaseSpawner);
        _motherbaseSpawner.Initialize(_motherbaseMediator, _resourceRegistry, _flagPlacer, _workerPool, _motherbaseBuilder);
        GenerateResources();
    }

    private void OnDisable()
    {
        _input.Disable();
    }

    private void GenerateResources()
    {
        foreach (var generator in _resourceSpawners)
        {
            generator.StartGenerateResources();
        }
    }

    private void InitializeMotherbases()
    {
        foreach (var motherbase in _motherbases)
        {
            motherbase.Initialize(_motherbaseMediator, _resourceRegistry, _workerPool);
            
            _flagPlacer.ListenMotherbase(motherbase);
            _motherbaseBuilder.ListenMotherbase(motherbase);
        }
    }
}
