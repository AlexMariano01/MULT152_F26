using UnityEngine;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour
{
    [Header("Settings")]
    public Slider healthSlider;
    public int health = 50;
    public int maxHealth = 50;

    [Header("Text Damage")]
    public GameObject damageTextPrefab;
    public Transform damageTextSpawnPoint;

    void Start()
    {
        health = maxHealth;
        healthSlider.maxValue = maxHealth;
        healthSlider.value = health;
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        healthSlider.value = health;

        ShowDamageText(damage);

        Debug.Log(gameObject.name + "took " + damage + "damage.");

        if (health <= 0)
        {
            Debug.Log(gameObject.name + "died");
            Destroy(gameObject);
        }
    }

    void ShowDamageText(int damage)
    {
        if (damageTextPrefab == null)
        {
            return;
        }

        Vector3 randomOffset = new Vector3( Random.Range(-0.25f, 0.25f), Random.Range(0f, 1f), 0f);

        GameObject text = Instantiate(damageTextPrefab, damageTextSpawnPoint.position + randomOffset, Quaternion.identity);

        FloatingDamageText floatingText = text.GetComponent<FloatingDamageText>();

        if (floatingText != null)
        {
            floatingText.SetDamage(damage);
        }
    }
}
