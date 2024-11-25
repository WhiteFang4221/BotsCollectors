using System;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class Worker : PoolableObject<Worker>
{
    [SerializeField] private WorkersTrunk _trunk;
    private Transform _targetPosition;
    private WorkerStateMachine _stateMachine;
    private NavMeshAgent _agent;
    
    public event Action<Worker, Transform> ResourceHasGiven;

    public bool IsBusy { get; private set; } = false;
    public Collider MotherbaseCollider { get; private set; }

    public NavMeshAgent Agent => _agent;
    public Transform TargetTransform => _targetPosition;
    public WorkersTrunk Trunk => _trunk;

    private void Awake()
    {
        _stateMachine = new WorkerStateMachine(this);
        _agent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        _stateMachine.Update();
    }

    public void InitializeMotherbase(Collider motherbaseCollider)
    {
        MotherbaseCollider = motherbaseCollider;
    }

    public void SetTargetResource(Transform resourcePosition)
    {
        _targetPosition = resourcePosition;
        IsBusy = true;
    }

    public void GiveResourceToBase(Transform resource)
    {
        IsBusy = false;
        _targetPosition = null;
        ResourceHasGiven?.Invoke(this, resource);
    }
}
