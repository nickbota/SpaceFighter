using DG.Tweening;
using UnityEngine;

public class ItemMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 1f;
    [SerializeField] private float yLimit = 10;
    private Vector3 direction = Vector3.down;

    private void Update()
    {
        MoveItem();
    }

    private void MoveItem()
    {
        transform.position += direction * moveSpeed * Time.deltaTime;

        //Disable item if out of bounds
        float itemY = transform.position.y;
        if (itemY < -yLimit || itemY > yLimit)
            gameObject.SetActive(false);
    }
}