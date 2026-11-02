using UnityEngine;
using UnityEngine.InputSystem;
public class PlayerAttack : MonoBehaviour
{
    public InputActionReference Attack;
    public int damage = 20;
    public float attackRange = 2f;
    public float attackCooldown = 0.5f;
    private float lastAttackTime;
    private bool attackPressed;

    public void OnAttack(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            attackPressed = true;
        }
    }
    private void Update()
    {
        if (!attackPressed)
            return;
        attackPressed = false;
        if (Time.time < lastAttackTime + attackCooldown)
            return;
        Collider[] hits =
            Physics.OverlapSphere(
                transform.position,
                attackRange);
        foreach (Collider hit in hits)
        {
            if (!hit.CompareTag("Enemy"))
                continue;
            Health health =
                hit.GetComponent<Health>();
            if (health == null)
                continue;
            health.TakeDamage(damage);
            lastAttackTime = Time.time;
            Debug.Log("Enemy Hit");
            break;
        }
    }
}