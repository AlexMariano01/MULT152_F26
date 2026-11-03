using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    [Header("References")]
    public Transform player;
    public NavMeshAgent agent;
    public EnemyThrow enemyThrow;
    public EnemyHealth enemyHealth;

    [Header("Movement")]
    public bool useNavMesh = true;
    public float moveSpeed = 3.5f;
    public float rotationSpeed = 10f;

    [Header("Ranges")]
    public float detectionRange = 15f;
    public float attackRange = 8f;

    public EnemyState currentState;

    private void Start()
    {
        currentState = EnemyState.Idle;

        if (player == null)
        {
            GameObject playerObj = GameObject.FindGameObjectWithTag("Player");

            if (playerObj != null)
            {
                player = playerObj.transform;
            }
        }

        if (useNavMesh)
        {
            if (agent == null)
            {
                agent = GetComponent<NavMeshAgent>();
            }
        }

        if (enemyThrow == null)
        {
            enemyThrow = GetComponent<EnemyThrow>();
        }

        if (enemyHealth == null)
        {
            enemyHealth = GetComponent<EnemyHealth>();
        }
    }

    private void Update()
    {
        if (player == null)
        {
            return;
        }

        if (enemyHealth != null && enemyHealth.health <= 0)
        {
            currentState = EnemyState.Dead;
        }

        HandleState();
    }

    void HandleState()
    {
        switch (currentState)
        {
            case EnemyState.Idle:
                IdleState();
                break;

            case EnemyState.Chase:
                ChaseState();
                break;

            case EnemyState.Attack:
                AttackState();
                break;

            case EnemyState.Dead:
                DeadState();
                break;
        }
    }

    void IdleState()
    {
        float distance = Vector3.Distance(transform.position, player.position);

        if (distance <= detectionRange)
        {
            currentState = EnemyState.Chase;
        } 
    }

    void ChaseState()
    {
        float distance = Vector3.Distance(transform.position, player.position);

        if (useNavMesh)
        {
            if (agent != null)
            {
                agent.isStopped = false;
                agent.SetDestination(player.position);
            }
        }

        else
        {
            Vector3 direction = (player.position - transform.position).normalized;
            transform.position += direction * moveSpeed * Time.deltaTime;
            direction.y = 0;

            if (direction != Vector3.zero)
            {
                Quaternion targetRotation = Quaternion.LookRotation(direction);

                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
            }
        }

        if (distance <= attackRange)
        {
            currentState = EnemyState.Attack;
        }

        if (distance > detectionRange)
        {
            currentState = EnemyState.Idle;
        }
    }

    void AttackState()
    {
        float distance = Vector3.Distance(transform.position, player.position);

        if (useNavMesh && agent != null)
        {
            agent.isStopped = true;
        }

        Vector3 direction = player.position - transform.position;
        direction.y = 0;

        if (direction != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(direction);

            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }

        if (enemyThrow != null)
        {
            enemyThrow.Attack();
        }

        if (distance > attackRange)
        {
            currentState = EnemyState.Chase;
        }
    }

    void DeadState()
    {
        if (useNavMesh && agent != null)
        {
            agent.isStopped = true;

            Destroy(GetComponent<Collider>());
        }

        enabled = false;
    }
}