using UnityEngine;
using TMPro;
using System;

public class FloatingDamageText : MonoBehaviour
{
    [Header("Settings")]
    public float moveSpeed = 2f;
    public float lifeTime = 1.5f;

    private TextMeshProUGUI damageText;

    private void Awake()
    {
        damageText = GetComponentInChildren<TextMeshProUGUI>();
    }

    public void SetDamage(int damage)
    {
        damageText.text = damage.ToString();
    }

    void Update()
    {
        transform.position += Vector3.up * moveSpeed * Time.deltaTime;

        lifeTime -= Time.deltaTime;

        if (lifeTime <= 0)
        {
            Destroy(gameObject);
        }
    }

    void LateUpdate()
    {
        if (Camera.main != null)
        {
            transform.forward = Camera.main.transform.forward;
        }
    }
}