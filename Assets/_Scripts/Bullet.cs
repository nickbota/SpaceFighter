using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float speed = 10f;
    [SerializeField] private float lifetime = 5f;
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
}
