using UnityEngine;
public class EnemyAttack : MonoBehaviour
{
    public int damage = 10;
    public float attackRange = 2f;
    public float attackCooldown = 1f;
    private float lastAttackTime;
    private Transform player;
    private void Start()
    {
        GameObject playerObject =
            GameObject.FindGameObjectWithTag("Player");
        if (playerObject != null)
        {
            player = playerObject.transform;
        }
    }
    private void Update()
    {
        if (Time.time < lastAttackTime + attackCooldown)
            return;
        if (player == null)
            return;
        float distance =
            Vector3.Distance(
                transform.position,
                player.position);
        if (distance <= attackRange)
        {
            Health health =
                player.GetComponent<Health>();
            if (health == null)
                return;
            health.TakeDamage(damage);
            lastAttackTime = Time.time;
            Debug.Log("Player Hit");
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(
            transform.position,
            attackRange);
    }
}