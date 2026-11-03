using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerDamageEffects : MonoBehaviour
{
    public Image damageOverlay;
    public float fadeSpeed = 3f;

    public void ShowDamageFlash()
    {
        StartCoroutine(DamageFlash());
    }

    IEnumerator DamageFlash()
    {
        damageOverlay.color =
            new Color(1,0,0,0.5f);

        while(damageOverlay.color.a > 0.01f)
        {
            damageOverlay.color =
                Color.Lerp(
                    damageOverlay.color,
                    new Color(1,0,0,0),
                    fadeSpeed * Time.deltaTime);

            yield return null;
        }
    }
}