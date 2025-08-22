using UnityEngine;

public class EnemyConstructor : MonoBehaviour
{
    [Header("Enemy Components")]
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private Health enemyHealth;

    public void ConstructEnemy(EnemyScriptableObject enemyScriptableObject)
    {
        spriteRenderer.sprite = enemyScriptableObject.EnemySprite;
        enemyHealth.SetEnemy(enemyScriptableObject.EnemyHP, enemyScriptableObject.EnemyScore);
    }
}