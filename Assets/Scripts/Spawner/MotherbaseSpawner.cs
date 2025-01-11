using UnityEngine;

public class MotherbaseSpawner : Spawner<Motherbase>
{
    private MotherbaseMediator _mediator;
    private MotherbaseBuilder _motherbaseBuilder;
    private ResourceRegistry _resourceRegistry;
    private FlagPlacer _flagPlacer;
    private WorkerPool _workerPool;

    public void Initialize(MotherbaseMediator mediator, ResourceRegistry resourceRegistry, FlagPlacer flagPlacer, WorkerPool pool, MotherbaseBuilder motherbaseBuilder)
    {
        _mediator = mediator;
        _resourceRegistry = resourceRegistry;
        _flagPlacer = flagPlacer;
        _workerPool = pool;
        _motherbaseBuilder = motherbaseBuilder;
    }

    public Motherbase SpawnMotherbase(Vector3 position)
    {
        Motherbase motherbase = SpawnObject(position);
        motherbase.Initialize(_mediator, _resourceRegistry, _workerPool);
        _motherbaseBuilder.ListenMotherbase(motherbase);
        _flagPlacer.ListenMotherbase(motherbase);

        return motherbase;
    }

    protected override void DestroyObject(Motherbase motherbase)
    {
        base.DestroyObject(motherbase);
        _flagPlacer.UnlistenMotherbase(motherbase);
    }
}
