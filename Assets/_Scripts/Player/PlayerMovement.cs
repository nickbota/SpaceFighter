using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement Speed")]
    [SerializeField] private float movementSpeed = 0.01f;

    [Header("X Limit")]
    [SerializeField] private float boundsX = -5f;
    private Vector3 lastInputPosition;

    private void Update()
    {
        HandleInput();
    }

    private void HandleInput()
    {
        if (Input.GetMouseButtonDown(0))
            lastInputPosition = Input.mousePosition;

        if (Input.GetMouseButton(0))
        {
            Vector3 delta = Input.mousePosition - lastInputPosition;
            MovePlayer(delta.x * movementSpeed * Time.deltaTime);
            lastInputPosition = Input.mousePosition;
        }
    }
    private void MovePlayer(float deltaX)
    {
        Vector3 pos = transform.position;
        pos.x += deltaX;
        pos.x = Mathf.Clamp(pos.x, -boundsX, boundsX);
        transform.position = pos;
    }
}