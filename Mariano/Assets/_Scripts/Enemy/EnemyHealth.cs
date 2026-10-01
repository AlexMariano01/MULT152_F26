using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public int health = 50;

    public void TakeDamage(int damage)
    {
        health -= damage;

        Debug.Log(gameObject.name + "took " + damage + "damage.");

        if (health <= 0)
        {
            Debug.Log(gameObject.name + "died");
            Destroy(gameObject);
        }
    }
}
