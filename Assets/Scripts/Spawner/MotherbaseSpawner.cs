using UnityEngine;

public class MotherbaseSpawner : Spawner<Motherbase>
{
    private MotherbaseMediator _mediator;
<<<<<<< HEAD
    private MotherbaseBuilder _motherbaseBuilder;
    private ResourceRegistry _resourceRegistry;
    private FlagPlacer _flagPlacer;
    private WorkerPool _workerPool;

    public void Initialize(MotherbaseMediator mediator, ResourceRegistry resourceRegistry, FlagPlacer flagPlacer, WorkerPool pool, MotherbaseBuilder motherbaseBuilder)
=======
    private ResourceRegistry _resourceRegistry;
    private FlagPlacer _flagPlacer;


    public void Initialize(MotherbaseMediator mediator, ResourceRegistry resourceRegistry, FlagPlacer flagPlacer)
>>>>>>> ec5cbbcbc3b4ad89f95722c6c941dafc1256bde8
    {
        _mediator = mediator;
        _resourceRegistry = resourceRegistry;
        _flagPlacer = flagPlacer;
<<<<<<< HEAD
        _workerPool = pool;
        _motherbaseBuilder = motherbaseBuilder;
    }

    public Motherbase SpawnMotherbase(Vector3 position)
    {
        Motherbase motherbase = SpawnObject(position);
        motherbase.Initialize(_mediator, _resourceRegistry, _workerPool);
        _motherbaseBuilder.ListenMotherbase(motherbase);
=======
    }

    protected override Motherbase SpawnObject(Vector3 position)
    {
        Motherbase motherbase = base.SpawnObject(position);
        motherbase.Initialize(_mediator, _resourceRegistry);
>>>>>>> ec5cbbcbc3b4ad89f95722c6c941dafc1256bde8
        _flagPlacer.ListenMotherbase(motherbase);

        return motherbase;
    }
<<<<<<< HEAD

    protected override void DestroyObject(Motherbase motherbase)
    {
        base.DestroyObject(motherbase);
        _flagPlacer.UnlistenMotherbase(motherbase);
    }
=======
>>>>>>> ec5cbbcbc3b4ad89f95722c6c941dafc1256bde8
}
