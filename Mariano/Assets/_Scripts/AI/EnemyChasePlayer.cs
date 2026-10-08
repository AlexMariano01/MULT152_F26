using UnityEngine;
using UnityEngine.AI;
 
[RequireComponent(typeof(NavMeshAgent))]
public class EnemyChasePlayer : MonoBehaviour
{
    public Transform target;
 
    private NavMeshAgent agent;
 
    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
    }
    
    private void Update()
    {
        if (target == null)
        return;
    
        agent.SetDestination(target.position);
    }
}