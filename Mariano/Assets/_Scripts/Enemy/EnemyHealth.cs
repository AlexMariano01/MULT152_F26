using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour
{
    [Header("Health")]
    public int maxHealth = 100;
    public int health;

    [Header("UI")]
    public Slider healthSlider;

    [Header("Damage Text")]
    public GameObject damageTextPrefab;
    public Transform damageTextSpawnPoint;

    [Header("Death")]
    public GameObject deathEffect;
    public float destroyDelay = 2f;

    [Header("Flash")]
    public float flashDuration = 0.1f;

    private Renderer rend;
    private EnemyTarget enemyTarget;
    private bool isDead;

    void Start()
    {
        health = maxHealth;

        if (healthSlider != null)
        {
            healthSlider.maxValue = maxHealth;
            healthSlider.value = health;
        }

        rend = GetComponentInChildren<Renderer>();
        enemyTarget = GetComponent<EnemyTarget>();
    }

    public void TakeDamage(int damage)
    {
        if (isDead)
            return;
        
        health -= damage;

        if (health < 0)
            health = 0;

        if (healthSlider != null)
        {
            healthSlider.value = health;
        }

        ShowDamageText(damage);

        StartCoroutine(FlashHit());

        if (health <= 0 && !isDead)
        {
            Die();
        }
    }

    private void ShowDamageText(int damage)
    {
        if (damageTextPrefab == null || damageTextSpawnPoint == null)
            return;

        GameObject textObj = Instantiate(
            damageTextPrefab,
            damageTextSpawnPoint.position,
            Quaternion.identity);

        FloatingDamageText damageTextScript =
            textObj.GetComponent<FloatingDamageText>();

        if (damageTextScript != null)
        {
            damageTextScript.SetDamage(damage);
        }
    }

    private IEnumerator FlashHit()
    {
        if (rend == null)
            yield break;

        if (rend.material.HasProperty("_BaseColor"))
        {
            rend.material.SetColor("_BaseColor", Color.white);
        }
        else if (rend.material.HasProperty("_Color"))
        {
            rend.material.color = Color.white;
        }

        yield return new WaitForSeconds(flashDuration);

        if (enemyTarget != null)
        {
            enemyTarget.RefreshColor();
        }
    }

    private void Die()
    {
        isDead = true;

        EnemyAI ai = GetComponent<EnemyAI>();

        if (ai != null)
        {
            ai.currentState = EnemyState.Dead;
        }

        if (deathEffect != null)
        {
            Instantiate(
                deathEffect,
                transform.position,
                Quaternion.identity);
        }

        Destroy(gameObject, destroyDelay);
    }
}
