using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class RandomEnemySprite : MonoBehaviour
{
    [SerializeField] private Sprite[] enemySprites;
    private SpriteRenderer spriteRenderer;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        SetRandomSprite();
    }
    private void SetRandomSprite()
    {
        spriteRenderer.sprite = enemySprites[Random.Range(0, enemySprites.Length)];
    }
}
