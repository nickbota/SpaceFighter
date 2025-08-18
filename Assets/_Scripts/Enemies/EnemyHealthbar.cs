using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class EnemyHealthbar : MonoBehaviour
{
    [SerializeField] private Health enemyHealth;
    private SpriteRenderer spriteRenderer;
    private Vector3 initialScale;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        initialScale = spriteRenderer.transform.localScale;
    }
    private void OnEnable()
    {
        spriteRenderer.transform.localScale = initialScale;
        enemyHealth.OnHealthChanged += OnHealthChanged;
    }
    private void OnDisable()
    {
        enemyHealth.OnHealthChanged += OnHealthChanged;
    }

    private void OnHealthChanged(int health)
    { 
        float percentage = (float)health / (float)enemyHealth.InitialHealth;
        spriteRenderer.transform.localScale = new Vector3(initialScale.x * percentage, initialScale.y, initialScale.z);
    }
}