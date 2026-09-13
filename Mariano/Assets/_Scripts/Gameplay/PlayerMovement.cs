using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public InputActionReference moveAction;

    void Update()
    {
        Vector2 move = moveAction.action.ReadValue<Vector2>();

        float horizontal = move.x;
        float vertical = move.y;

        Vector3 movement = new Vector3(horizontal, 0f, vertical);

        transform.Translate(movement * moveSpeed * Time.deltaTime);
    }
}
