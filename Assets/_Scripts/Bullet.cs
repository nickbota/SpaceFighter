using UnityEngine;
using DG.Tweening;

public class Bullet : MonoBehaviour
{
    [Header("Movement Parameters")]
    [SerializeField] private float speed = 10f;
    [SerializeField] private float lifetime = 5f;

    [Header("Damage Parameters")]
    [SerializeField] private LayerMask damageLayer;
    [SerializeField] private int damage = 1;

    [Header("Bullet Animator")]
    [SerializeField] private Animator bulletAnimator;
    [SerializeField] private Collider2D coll;

    private ObjectPool pool;
    private float timer;
    private bool moving;

    public void Initialize(ObjectPool objectPool)
    {
        pool = objectPool;
        coll = GetComponent<Collider2D>();
    }
    private void OnEnable()
    {
        ResetBullet();
    }
    private void OnDisable()
    {
        pool?.ReturnToPool(gameObject);
    }
    private void Update()
    {
        if (!moving) return;

        transform.Translate(Vector3.up * speed * Time.deltaTime);
        timer += Time.deltaTime;

        if (timer >= lifetime)
            gameObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (LayerCheck.IsInLayer(damageLayer, collision.gameObject))
        {
            if (collision.gameObject.TryGetComponent(out Health health))
                health.ChangeHealth(-damage);

            bulletAnimator.SetTrigger("hit");
            moving = false;
            coll.enabled = false;

            DOTween.Sequence()
                .AppendInterval(0.1f)
                .AppendCallback(() => gameObject.SetActive(false));
        }
    }

    private void ResetBullet()
    {
        timer = 0f;
        moving = true;
        coll.enabled = true;
    }
}
