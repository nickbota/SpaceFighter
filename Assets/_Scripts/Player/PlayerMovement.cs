using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float boundsX = -5f;
    [SerializeField] private float sensitivity = 0.01f;
    private Vector3 lastInputPosition;

    private void Update()
    {
        HandleInput();
    }

    private void HandleInput()
    {
        if (Input.GetMouseButtonDown(0))
        {
            lastInputPosition = Input.mousePosition;
            Vector3 delta = Input.mousePosition - lastInputPosition;
            MovePlayer(delta.x * sensitivity);
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