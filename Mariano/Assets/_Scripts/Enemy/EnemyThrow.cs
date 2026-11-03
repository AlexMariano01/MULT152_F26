using System.Threading;
using UnityEngine;
using UnityEngine.Rendering;

public class EnemyThrow : MonoBehaviour
{
    [Header("Connection")]
    public Transform player;

    public GameObject projectilePrefab;
    public Transform throwPoint;

    [Header("Settings")]
    public float throwForce = 15f;
    public float attackCooldown = 2f;

    private float timer;

   private void Start()
    {
        timer = attackCooldown;
    }

   private void Update()
    {
        timer += Time.deltaTime;
    }

    public void Attack()
    {
        if (timer < attackCooldown)
        {
            return;
        }

        timer = 0f;

        ThrowProjectile();
    }

    void ThrowProjectile()
    {        
        if (throwPoint == null)
        {
            Debug.LogWarning("Throw Point not assigned!");
            return;
        }

        if (projectilePrefab == null)
        {
            Debug.LogWarning("Projectile Prefab not assigned!");
            return;
        }

        if (player == null)
        {
            GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
            if (playerObj != null)
            {
                player = playerObj.transform;
            }
            else
            {
                Debug.LogWarning("Player not found in the scene!!");
                return;
            }
        }

        GameObject projectile = Instantiate(projectilePrefab, throwPoint.position, Quaternion.identity);

        Rigidbody rb = projectile.GetComponent<Rigidbody>();

        Vector3 direction = (player.position - throwPoint.position).normalized;

        rb.AddForce(direction*throwForce, ForceMode.Impulse);
    }
}