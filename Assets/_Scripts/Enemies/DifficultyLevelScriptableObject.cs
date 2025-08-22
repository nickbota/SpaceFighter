using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "SpaceFighter/Difficulty Setting")]
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