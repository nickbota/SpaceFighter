
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement Speed")]
    [SerializeField] private float touchMovementSpeed = 2.5f;
    [SerializeField] private float keyboardMovementSpeed = 5f;

    [Header("X Limit")]
    [SerializeField] private float boundsX = -5f;

    [Header("Thruster Audio")]
    [SerializeField] private AudioSource thrusterAudioSource;

    private Vector2 lastInputPosition;
    private bool isPointerDown;
    private float moveInput;

    private void Update()
    {
        HandleInput();
        HandleKeyboardMovement();
    }

    private void HandleInput()
    {
        if (isPointerDown)
        {
            Vector2 currentPos = Touchscreen.current != null
                ? Touchscreen.current.position.ReadValue()
                : Mouse.current.position.ReadValue();

            Vector2 delta = currentPos - lastInputPosition;
            MovePlayer(delta.x * touchMovementSpeed * Time.deltaTime);
            lastInputPosition = currentPos;
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
            MovePlayer(moveInput * keyboardMovementSpeed * Time.deltaTime);
    }

    #region Input
    private void OnPointerDown(InputValue value)
    {
        isPointerDown = true;
        thrusterAudioSource.Play();

        lastInputPosition = Touchscreen.current != null
                ? Touchscreen.current.position.ReadValue()
                : Mouse.current.position.ReadValue();
    }
    private void OnPointerUp(InputValue value)
    {
        isPointerDown = false;
        thrusterAudioSource.Stop();
    }
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