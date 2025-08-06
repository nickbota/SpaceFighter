using UnityEngine;

public class BackgroundParallax : MonoBehaviour
{
    [SerializeField] private Transform background1;
    [SerializeField] private Transform background2;
    [SerializeField] private float scrollSpeed = 2f;

    private float spriteHeight;

    private void Start()
    {
        if (background1 == null || background2 == null)
        {
            Debug.LogError("Assign both background sprites in the inspector!");
            enabled = false;
            return;
        }

        // Assuming both sprites are the same height
        spriteHeight = background1.GetComponent<SpriteRenderer>().bounds.size.y;
    }

    private void Update()
    {
        Vector3 move = Vector3.down * scrollSpeed * Time.deltaTime;

        background1.position += move;
        background2.position += move;

        // Reposition if a background has moved completely out of view
        if (background1.position.y <= -spriteHeight)
            background1.position += Vector3.up * spriteHeight * 2f;

        if (background2.position.y <= -spriteHeight)
            background2.position += Vector3.up * spriteHeight * 2f;
    }
}
