using System;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class Worker : MonoBehaviour
{
    [SerializeField] private WorkersTrunk _trunk;

    private Transform _targetPosition;

    private WorkerStateMachine _stateMachine;

    private NavMeshAgent _agent;
    
    public event Action<Worker, Transform> ResourceCollected;


    public bool IsBusy { get; private set; } = false;
    public WorkersTrunk Trunk => _trunk;
    public Transform TargetTransform => _targetPosition;
    public Transform MotherbasePosition { get; private set; }
    public Collider MotherbaseCollider { get; private set; }
    public NavMeshAgent Agent => _agent;

    private void Awake()
    {
        _stateMachine = new WorkerStateMachine(this);
        _agent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        _stateMachine.Update();
    }

    public void SetTargetResource(Transform resourcePosition)
    {
        Debug.Log($"{GetInstanceID()}   - Еду за ресурсоом");
        _targetPosition = resourcePosition;
        IsBusy = true;
    }

    public void SetMotherbaseData(Collider collider, Transform transform)
    {
        MotherbaseCollider = collider;
        MotherbasePosition = transform;
    }

    public void GiveResourceToBase(Transform resource)
    {
        IsBusy = false;
        _targetPosition = null;
        ResourceCollected?.Invoke(this, resource);
    }
}
