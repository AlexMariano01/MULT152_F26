using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public InputActionReference moveAction;

    public PlayerTargeting targeting;

    void Update()
    {
        Vector2 move = moveAction.action.ReadValue<Vector2>();

        float horizontal = move.x;
        float vertical = move.y;

        Vector3 movement = new Vector3(horizontal, 0f, vertical);

        transform.Translate(movement * moveSpeed * Time.deltaTime);
        //Debug.Log("Move: " + move + "| Player Position: " + transform.position);

        if (targeting.IsLockedOn && targeting.LockedTarget != null)
        {
            Vector3 direction = targeting.LockedTarget.transform.position - transform.position;
            
            direction.y = 0;

            Quaternion targetRotation = Quaternion.LookRotation(direction);

            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, 8f * Time.deltaTime);
        }
    }
}