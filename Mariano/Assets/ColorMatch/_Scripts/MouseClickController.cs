using UnityEngine;
using UnityEngine.InputSystem;

public class MouseClickController : MonoBehaviour
{
    private PlayerInputActions inputActions;

    private void Awake()
    {
        inputActions = new PlayerInputActions();
    }

    private void OnEnable()
    {
        inputActions.Enable();
        inputActions.Player.Click.performed += OnClick;
    }

    private void OnDisable()
    {
        inputActions.Player.Click.performed -= OnClick;
        inputActions.Disable();
    }

    private void OnClick(InputAction.CallbackContext context)
    {
        Vector2 mousePosition =
            Mouse.current.position.ReadValue();

        Ray ray =
            Camera.main.ScreenPointToRay(mousePosition);

        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            ColorObject colorObject =
                hit.collider.GetComponent<ColorObject>();

            if (colorObject != null)
            {
                colorObject.CycleColor();
            }
        }
    }
}