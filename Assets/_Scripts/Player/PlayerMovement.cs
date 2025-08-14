using Terresquall;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement Speed")]
    [SerializeField] private float movementSpeed = 0.01f;

    [Header("X Limit")]
    [SerializeField] private float boundsX = -5f;

    [Header("Joystick")]
    [SerializeField] private VirtualJoystick joystick;

    private Vector3 lastInputPosition;
    private float moveInput;

    private void Update()
    {
        HandleInput();
        HandleKeyboardMovement();
    }

    private void HandleInput()
    {
        //if (Input.GetMouseButtonDown(0))
        //    lastInputPosition = Input.mousePosition;

        if (Input.GetMouseButton(0))
        {
            //Vector3 delta = Input.mousePosition - lastInputPosition;
            float delta = joystick.GetAxisRaw("Horizontal");
            MovePlayer(delta * movementSpeed * Time.deltaTime);
            //lastInputPosition = Input.mousePosition;
        }
    }
    private void MovePlayer(float deltaX)
    {
        Vector3 pos = transform.position;
        pos.x += deltaX;
        pos.x = Mathf.Clamp(pos.x, -boundsX, boundsX);
        transform.position = pos;
    }
    private void HandleKeyboardMovement()
    {
        if (Mathf.Abs(moveInput) > 0.01f)
            MovePlayer(moveInput * movementSpeed * Time.deltaTime);
    }

    #region Input
    private void OnMove(InputValue inputValue)
    {
        moveInput = inputValue.Get<Vector2>().x;
    }
    private void OnMoveCanceled(InputAction.CallbackContext context)
    {
        moveInput = 0;
    }
    #endregion
}