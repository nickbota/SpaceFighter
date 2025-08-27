using System;
using UnityEngine;

[CreateAssetMenu(menuName = "Space Fighter/Difficulty Setting")]
public class DifficultyLevelScriptableObject : ScriptableObject
{
    [Header("Enemy Distribution")]
    [SerializeField] private EnemyDistributionEntry[] enemyDistribution;
    public EnemyDistributionEntry[] EnemyDistribution => enemyDistribution;

    [Serializable]
    public struct EnemyDistributionEntry
    {
        public EnemyScriptableObject Enemy;
        public int Percentage;
    }
}