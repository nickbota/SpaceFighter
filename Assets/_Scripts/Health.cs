using UnityEngine;
using UnityEngine.Events;
using System;

public class Health : MonoBehaviour
{
    [Header("Health Parameters")]
    [SerializeField] private int initialHealth;
    private int currentHealth;
    public int CurrentHealth => currentHealth;
    public Action<int> OnHealthChanged { get; set; }

    [Header("Events")]
    [SerializeField] private UnityEvent OnHurt;
    [SerializeField] private UnityEvent OnDeath;

    [Header("Score")]
    [SerializeField] private int scoreToAdd;
    private EnemyFormation enemyFormation;

    private void Awake()
    {
        currentHealth = initialHealth;
        enemyFormation = FindFirstObjectByType<EnemyFormation>();
    }
    private void OnDisable()
    {
        if (scoreToAdd == 0 || enemyFormation == null) return;
        enemyFormation.AddScore(scoreToAdd, gameObject);
    }

    public void ChangeHealth(int change)
    {
        currentHealth = Mathf.Clamp(currentHealth + change, 0, initialHealth);
        OnHealthChanged?.Invoke(currentHealth);

        if (change > 0) return;

        if (currentHealth > 0) OnHurt?.Invoke();
        else                   OnDeath?.Invoke();
    }
}