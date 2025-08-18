using DG.Tweening;
using UnityEngine;

public class PlayerCollisions : MonoBehaviour
{
    [SerializeField] private LayerMask damageOnContactLayer;
    [SerializeField] private Health playerHealth;
    [SerializeField] private int damageOnContact = 1;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (LayerCheck.IsInLayer(damageOnContactLayer, collision.gameObject))
        {
            if (collision.gameObject.TryGetComponent(out Health health))
                health.Die();
            else
                collision.gameObject.SetActive(false);

            playerHealth.ChangeHealth(-damageOnContact);
        }
    }
}