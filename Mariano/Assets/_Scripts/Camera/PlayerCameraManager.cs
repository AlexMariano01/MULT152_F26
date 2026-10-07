using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerCameraManager : MonoBehaviour
{
    [Header("Camera Objects")]
    public GameObject firstPersonCamera;
    public GameObject thirdPersonCamera;

    [Header("Input")]
    public InputActionReference toggleCameraAction;
    public InputActionReference aimAction;
    public PlayerTargeting targeting;
    public InputActionReference lockOnAction;

    [Header("Current Mode")]
    public CameraMode currentMode = CameraMode.ThirdPerson;

    private CameraMode previousMode;

    private void OnEnable()
    {
        toggleCameraAction.action.Enable();
        aimAction.action.Enable();
        lockOnAction.action.Enable();
    }

    private void OnDisable()
    {
        toggleCameraAction.action.Disable();
        aimAction.action.Disable();
        lockOnAction.action.Disable();
    }

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        UpdateCameraState();
    }

    void Update()
    {
        HandleCameraToggle();
        HandleAimMode();

        if (lockOnAction.action.triggered)
        {
            targeting.ToggleLock();
        }
    }

    void HandleCameraToggle()
    {
        if (!toggleCameraAction.action.triggered)
        {
            return;
        }

        if (currentMode == CameraMode.FirstPerson)
        {
            currentMode = CameraMode.ThirdPerson;
        }
        else
        {
            currentMode = CameraMode.FirstPerson;
        }

        UpdateCameraState();
    }

    void HandleAimMode()
    {
        if (currentMode == CameraMode.FirstPerson)
        {
            return;
        }

        if (aimAction.action.IsPressed())
        {
            if (currentMode != CameraMode.FocusAim)
            {
                previousMode = currentMode;
                currentMode = CameraMode.FocusAim;

                UpdateCameraState();
            }
        }
        else
        {
            if (currentMode == CameraMode.FocusAim)
            {
                currentMode = CameraMode.ThirdPerson;
                
                UpdateCameraState();
            }
        }
    }

    void UpdateCameraState()
    {
        switch(currentMode)
        {
            case CameraMode.FirstPerson:
                
                firstPersonCamera.SetActive(true);
                thirdPersonCamera.SetActive(false);

                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;

                Debug.Log("Camera: First Person");

                break;

            case CameraMode.ThirdPerson:
                
                firstPersonCamera.SetActive(false);
                thirdPersonCamera.SetActive(true);

                Debug.Log("Camera: Third Person");

                break;

            case CameraMode.FocusAim:
                
                firstPersonCamera.SetActive(false);
                thirdPersonCamera.SetActive(true);

                Debug.Log("Camera: Shoulder Aim");

                break;
        }
    }

    public bool IsFirstPerson()
    {
        return currentMode == CameraMode.FirstPerson;
    }

    public bool IsThirdPerson()
    {
        return currentMode == CameraMode.ThirdPerson;
    }

    public bool IsAiming()
    {
        return currentMode == CameraMode.FocusAim;
    }
}