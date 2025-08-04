using UnityEngine;

public class Bullet : MonoBehaviour
{
    [Header("Movement Parameters")]
    [SerializeField] private float speed = 10f;
    [SerializeField] private float lifetime = 5f;

    [Header("Damage Parameters")]
    [SerializeField] private LayerMask damageLayer;
    [SerializeField] private int damage = 1;

    private ObjectPool pool;
    private float timer;

    public void Initialize(ObjectPool objectPool)
    {
        pool = objectPool;
    }

    private void OnEnable()
    {
        timer = 0f;
    }
    private void Update()
    {
        transform.Translate(Vector3.up * speed * Time.deltaTime);
        timer += Time.deltaTime;

        if (timer >= lifetime)
        {
            pool?.ReturnToPool(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (LayerCheck.IsInLayer(damageLayer, collision.gameObject))
        {
            if (collision.gameObject.TryGetComponent(out Health health))
                health.ChangeHealth(-damage);

            gameObject.SetActive(false);
        }
    }
}
