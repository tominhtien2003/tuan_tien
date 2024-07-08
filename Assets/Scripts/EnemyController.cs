using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    [SerializeField] PlayerController target;
    private NavMeshAgent agent;
    [SerializeField] private float updateInterval;
    [SerializeField] private float thresholdDistance;

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        InvokeRepeating(nameof(UpdateDestination), 0, updateInterval);
    }

    private void UpdateDestination()
    {
        if (target == null) return;
        
        float distance = Vector3.Distance(transform.position, target.transform.position);

        if (distance > thresholdDistance)
        {
            agent.SetDestination(target.transform.position);
        }
        else
        {
            agent.SetDestination(transform.position);
        }
    }

    private void Update()
    {
        RotateTowardsTarget();
    }

    private void RotateTowardsTarget()
    {
        if (target == null) return;

        Vector3 direction = (target.transform.position - transform.position).normalized;

        direction.y = 0;

        Quaternion lookRotation = Quaternion.LookRotation(direction);

        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * agent.angularSpeed);
    }
}
