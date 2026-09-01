using TMPro;
using UnityEngine;

public class ColorMatchManager : MonoBehaviour
{
    public static ColorMatchManager Instance;

    public ColorObject[] colorObjects;

    public TMP_Text resultText;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        CheckMatch();
    }

    public void CheckMatch()
    {
        if (colorObjects.Length < 3)
            return;

        var firstColor = colorObjects[0].CurrentColor;

        foreach (var obj in colorObjects)
        {
            if (obj.CurrentColor != firstColor)
            {
                resultText.text = "";
                return;
            }
        }

        resultText.text =
            $"ALL OBJECTS ARE {firstColor.ToString().ToUpper()}!";
    }
}