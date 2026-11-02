using UnityEngine;
using UnityEngine.UI;

public class CrosshairController : MonoBehaviour
{
    [Header("Crosshair Settings")]
    public Image crosshair;
    public Color normalColor = Color.white;
    public Color targetColor = Color.red;

    public PlayerTargeting playerTargeting;
    public PlayerCameraManager cameraManager;

    void Update()
    {
        UpdateVisibility();
        UpdateColor();
    }

    void UpdateVisibility()
    {
        bool crosshairShow = cameraManager.currentMode == CameraMode.FirstPerson || cameraManager.currentMode == CameraMode.ThirdPerson;

        crosshair.gameObject.SetActive(crosshairShow);
    }

    void UpdateColor()
    {
        if (!crosshair.enabled)
        {
            return;
        }
        
        if (playerTargeting.CurrentTarget != null)
        {
            crosshair.color = targetColor;
        }

        else
        {
            crosshair.color = normalColor;
        }
    }
}