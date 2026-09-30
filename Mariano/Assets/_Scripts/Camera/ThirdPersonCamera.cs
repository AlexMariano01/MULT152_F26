using UnityEngine;
using UnityEngine.InputSystem;

public class ThirdPersonCamera : MonoBehaviour
{
    public Transform target;
    public Vector3 offset = new Vector3(0f, 3f, -6f);
    public float rotationSpeed = 120f;
    Vector2 lookInput;
    float currentYaw;

    public void OnLook(InputAction.CallbackContext context)
    {
        lookInput = context.ReadValue<Vector2>();
    }

    void LateUpdate()
    {
        currentYaw += lookInput.x * rotationSpeed * Time.deltaTime;
        Quaternion rotation = Quaternion.Euler(0f, currentYaw, 0f);
        transform.position = target.position + rotation * offset;
        transform.LookAt(target);
    }
}
