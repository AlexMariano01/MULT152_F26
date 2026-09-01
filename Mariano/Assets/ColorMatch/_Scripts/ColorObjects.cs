using UnityEngine;

public class ColorObject : MonoBehaviour
{
    public enum ColorState
    {
        Red,
        Green,
        Blue
    }

    public Material redMaterial;
    public Material greenMaterial;
    public Material blueMaterial;

    public ColorState CurrentColor { get; private set; }

    private Renderer rend;

    private void Awake()
    {
        rend = GetComponent<Renderer>();
    }

    private void Start()
    {
        SetColor(ColorState.Red);
    }

    public void CycleColor()
    {
        switch(CurrentColor)
        {
            case ColorState.Red:
                SetColor(ColorState.Green);
                break;

            case ColorState.Green:
                SetColor(ColorState.Blue);
                break;

            case ColorState.Blue:
                SetColor(ColorState.Red);
                break;
        }

        ColorMatchManager.Instance.CheckMatch();
    }

    private void SetColor(ColorState color)
    {
        CurrentColor = color;

        switch(color)
        {
            case ColorState.Red:
                rend.material = redMaterial;
                break;

            case ColorState.Green:
                rend.material = greenMaterial;
                break;

            case ColorState.Blue:
                rend.material = blueMaterial;
                break;
        }
    }
}