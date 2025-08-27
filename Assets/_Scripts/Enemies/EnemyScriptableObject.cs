using UnityEngine;

[CreateAssetMenu(menuName = "Space Fighter/Enemy Variant")]
public class EnemyScriptableObject : ScriptableObject
{
    [Header("Sprite")]
    [SerializeField] private Sprite enemySprite;
    public Sprite EnemySprite => enemySprite;

    [Header("HP")]
    [SerializeField] private int enemyHP;
    public int EnemyHP => enemyHP;

    [Header("Score")]
    [SerializeField] private int enemyScore;
    public int EnemyScore => enemyScore;
}