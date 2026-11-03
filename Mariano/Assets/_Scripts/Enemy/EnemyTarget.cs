using UnityEngine;

public class EnemyTarget : MonoBehaviour
{
    [Header("Target Colors")]
    public Color normalColor = new Color(0.7294f, 0.7294f, 0.7294f, 1f);
    public Color targetedColor = Color.red;

    private Renderer[] renderers;

    public bool IsTargeted { get; private set; }

    private void Awake()
    {
        renderers = GetComponentsInChildren<Renderer>();

        ApplyColor(normalColor);
    }

    public void SetTargeted(bool targeted)
    {
        IsTargeted = targeted;

        ApplyColor(
            targeted ? targetedColor : normalColor
        );
    }

    public void RefreshColor()
    {
        ApplyColor(
            IsTargeted ? targetedColor : normalColor
        );
    }

    private void ApplyColor(Color color)
    {
        if (renderers == null)
            return;

        foreach (Renderer r in renderers)
        {
            if (r == null)
                continue;

            // URP Lit
            if (r.material.HasProperty("_BaseColor"))
            {
                r.material.SetColor("_BaseColor", color);
            }

            // Standard Shader fallback
            if (r.material.HasProperty("_Color"))
            {
                r.material.color = color;
            }
        }
    }
}