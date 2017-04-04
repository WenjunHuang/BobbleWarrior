using UnityEngine;
using UnityEngine.AI;

public class Alien : MonoBehaviour
{
    public Transform Target;
    private NavMeshAgent _agent;

    public void Start()
    {
        _agent = GetComponent<NavMeshAgent>();
    }

    public void Update()
    {
        if (Target != null)
            _agent.destination = Target.position;
    }
}