using UnityEngine;
using UnityEngine.InputSystem;

public class ThirdPersonCamera : MonoBehaviour
{
    [Header("References")]
    public Transform target;
    public InputActionReference lookAction;
    public PlayerCameraManager cameraManager;

    [Header("Camera Settings")]
    public float rotationSpeed = 120f;

    [Header("Pitch Limits")]
    public float minPitch = -30f;
    public float maxPitch = 60f;

    [Header("Offsets")]
    public Vector3 normalOffset = new Vector3(0f, 3f, -6f);
    public Vector3 focusOffset = new Vector3(1.25f, 2.5f, -3f);

    private float currentYaw;
    private float currentPitch = 20f;
    private Vector3 currentOffset;

    public PlayerTargeting targeting;

    void Start()
    {
        currentOffset = normalOffset;
    }

    void LateUpdate()
    {
        Vector2 lookInput = lookAction.action.ReadValue<Vector2>();
        currentYaw += lookInput.x * rotationSpeed * Time.deltaTime;
        currentPitch -= lookInput.y * rotationSpeed * Time.deltaTime;
        currentPitch = Mathf.Clamp(currentPitch, minPitch, maxPitch);
        Vector3 targetOffset = cameraManager.IsAiming() ? focusOffset : normalOffset;
        currentOffset = Vector3.Lerp(currentOffset, targetOffset, 8f * Time.deltaTime);
        Quaternion rotation = Quaternion.Euler(currentPitch, currentYaw, 0f);

        Vector3 offset = rotation * currentOffset;

        if (targeting != null && targeting.IsLockedOn && targeting.LockedTarget != null)
        {
            transform.LookAt(targeting.LockedTarget.transform.position + Vector3.up * 1.5f);
            return;
        }

        transform.position = target.position + offset;
        transform.LookAt(target.position + Vector3.up * 1.5f);
    }
}