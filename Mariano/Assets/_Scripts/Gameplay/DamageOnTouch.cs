using UnityEngine;

public class DamageOnTouch : MonoBehaviour
{
    public int damageAmount = 10;

    void OnCollisionEnter(Collision collision)
    {
        Health health = collision.gameObject.GetComponent<Health>();

        if (health != null)
        {
            health.currentHealth -= damageAmount;
            Debug.Log(collision.gameObject.name + " took damage, Health: " + health.currentHealth);
        }
    }
}
