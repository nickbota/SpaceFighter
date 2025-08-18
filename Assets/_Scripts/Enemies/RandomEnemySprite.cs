using UnityEngine;

public class RandomEnemySprite : MonoBehaviour
{
    [SerializeField] private Sprite[] enemySprites;
    [SerializeField] private SpriteRenderer spriteRenderer;

    private void OnEnable()
    {
        SetRandomSprite();
    }
    public void SetRandomSprite()
    {
        spriteRenderer.sprite = enemySprites[Random.Range(0, enemySprites.Length)];
    }
}