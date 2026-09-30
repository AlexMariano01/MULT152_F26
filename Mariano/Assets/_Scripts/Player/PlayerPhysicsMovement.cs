using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody))]
public class PlayerPhysicsMovement : MonoBehaviour
{
    public float moveForce = 10f;
    public float maxSpeed = 5f;
    public float jumpForce = 5f;
    public LayerMask groundLayer;
    public Transform groundCheck;
    public float groundCheckRadius = 0.2f;
    bool isGrounded;
    
    Rigidbody rb;
    Vector2 moveInput;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
    }

    private void FixedUpdate()
    {
        Vector3 force = new Vector3(moveInput.x, 0f, moveInput.y) * moveForce;
        rb.AddForce(force);
        Vector3 velocity = rb.linearVelocity;
        velocity.y = rb.linearVelocity.y;

        if (velocity.magnitude > maxSpeed)
        {
            velocity = velocity.normalized * maxSpeed;
            velocity.y = rb.linearVelocity.y;
            rb.linearVelocity = velocity;
        }
    }

    void Update()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundCheckRadius, groundLayer);
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if (context.performed && isGrounded)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
    }
}
