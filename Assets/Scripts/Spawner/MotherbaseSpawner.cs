using UnityEngine;

public class MotherbaseSpawner : Spawner<Motherbase>
{
    private MotherbaseMediator _mediator;
    private ResourceRegistry _resourceRegistry;
    private FlagPlacer _flagPlacer;


    public void Initialize(MotherbaseMediator mediator, ResourceRegistry resourceRegistry, FlagPlacer flagPlacer)
    {
        _mediator = mediator;
        _resourceRegistry = resourceRegistry;
        _flagPlacer = flagPlacer;
    }

    protected override Motherbase SpawnObject(Vector3 position)
    {
        Motherbase motherbase = base.SpawnObject(position);
        motherbase.Initialize(_mediator, _resourceRegistry);
        _flagPlacer.ListenMotherbase(motherbase);

        return motherbase;
    }
}
