using UnityEngine;
using UnityEngine.Events;

public class Health : MonoBehaviour
{
    [Header("Health Parameters")]
    [SerializeField] private int initialHealth;
    private int currentHealth;
    public int CurrentHealth => currentHealth;

    [Header("Events")]
    [SerializeField] private UnityEvent OnHurt;
    [SerializeField] private UnityEvent OnDeath;

    private void Awake()
    {
        currentHealth = initialHealth;
    }

    public void ChangeHealth(int change)
    {
        currentHealth = Mathf.Clamp(currentHealth + change, 0, initialHealth);

        if (change > 0) return;

        if (currentHealth > 0) OnHurt?.Invoke();
        else                   OnDeath?.Invoke();
    }
}