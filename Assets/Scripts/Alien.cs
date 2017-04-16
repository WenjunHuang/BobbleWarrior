using UnityEngine;
using UnityEngine.AI;

public class Alien : MonoBehaviour
{
    public Transform Target;
    public float NavigationUpdate;

    private float _navagationTime;
    private NavMeshAgent _agent;

    public void Start()
    {
        _agent = GetComponent<NavMeshAgent>();
    }

    public void Update()
    {
        _navagationTime += Time.deltaTime;
        if (_navagationTime > NavigationUpdate)
        {
            UpdateDestinationToTarget();
            _navagationTime = 0.0f;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Destroy(gameObject);
        SoundManager.Instance.PlayOneShot(SoundManager.Instance.AlienDeath);
    }

    private void UpdateDestinationToTarget()
    {
        _agent.destination = Target.position;
    }
}