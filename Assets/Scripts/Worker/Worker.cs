using System;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class Worker : PoolableObject<Worker>, IBaseBuilder
{
    [SerializeField] private WorkerTrunk _trunk;

    private Transform _targetPosition;
    private WorkerStateMachine _stateMachine;
    private NavMeshAgent _agent;
    
    public event Action<Worker, Transform> ResourceHasGiven;
    public event Action<Transform, IBaseBuilder> ReachedFlag;
    public event Action<Worker> QuitMotherbase;

    public bool IsBusy { get; private set; } = false;
    public bool IsResourceFollowing { get; private set; } = false;
    public Collider MotherbaseCollider { get; private set; }

    public NavMeshAgent Agent => _agent;
    public Transform TargetTransform => _targetPosition;
    public WorkerTrunk Trunk => _trunk;

    private void Awake()
    {
        _stateMachine = new WorkerStateMachine(this);
        _agent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        _stateMachine.Update();
    }

    public void InitializeMotherbaseCollider(Collider motherbaseCollider)
    {
        MotherbaseCollider = motherbaseCollider;
    }

    public void SetTarget(Transform targetPosition)
    {
        _targetPosition = targetPosition;
        IsBusy = true;
    }

    public void BecomeFree()
    {
        IsBusy = false;
        _targetPosition = null;
    }

    public void GiveResourceToBase(Transform resource)
    {
        ResourceHasGiven?.Invoke(this, resource);
    }

    public void ReportReachTarget()
    {
        QuitMotherbase?.Invoke(this);
        ReachedFlag?.Invoke(TargetTransform, this);
    }
}
