using UnityEngine;

public class Health : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth;

    void Start()
    {
        currentHealth = maxHealth;
    }

    void Update()
    {
        if (currentHealth <= 0)
        {
            Death();
        }
    }

    void Death()
    {
        Debug.Log(gameObject.name + " died");
        Destroy(gameObject);
    }
}
