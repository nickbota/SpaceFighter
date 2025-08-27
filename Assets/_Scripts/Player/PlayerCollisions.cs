using UnityEngine;

public class PlayerCollisions : MonoBehaviour
{
    [SerializeField] private Health playerHealth;
    [SerializeField] private PlayerShooting playerShooting;

    [Header("Damage")]
    [SerializeField] private LayerMask damageOnContactLayer;
    [SerializeField] private int damageOnContact = 1;

    [Header("Items")]
    [SerializeField] private LayerMask itemLayer;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Check if collided with an enemy
        if (LayerCheck.IsInLayer(damageOnContactLayer, collision.gameObject))
        {
            if (collision.gameObject.TryGetComponent(out Health health))
                health.Die();
            else
                collision.gameObject.SetActive(false);

            playerHealth.ChangeHealth(-damageOnContact);
        }

        //Check if picked up an item
        if (LayerCheck.IsInLayer(itemLayer, collision.gameObject))
        {
            //Activate item effect and deactivate object
            collision.gameObject.SetActive(false);
        }
    }
}