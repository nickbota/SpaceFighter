using UnityEngine;

public class LifeCounter : Counter
{
    [SerializeField] private Health health;

    private void OnEnable()
    {
        health.OnHealthChanged += OnHealthChanged;
    }
    private void OnDisable()
    {
        health.OnHealthChanged -= OnHealthChanged;
    }
    private void Start()
    {
        UpdateCounter(health.CurrentHealth.ToString());
    }

    private void OnHealthChanged(int newHealth)
    {
        UpdateCounter(newHealth.ToString());
    }
}