using UnityEngine;

public class EnemyAnimationHelper : MonoBehaviour
{
    public void DeactivateEnemy()
    {
        transform.parent.gameObject.SetActive(false);
    }
}