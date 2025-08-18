using UnityEngine;
using UnityEngine.Events;

public class PlayerShooting : MonoBehaviour
{
    [Header("Object Pool")]
    [SerializeField] private ObjectPool bulletPool;

    [Header("Shot Parameters")]
    [SerializeField] private Transform firePoint;
    [SerializeField] private float shootCooldown = 0.5f;
    [SerializeField] private EnemyFormation enemyFormation;
    [SerializeField] private UnityEvent OnShoot;
    private float lastShootTime = 0f;
    private float shotTimer = 1000;

    private void Update()
    {
        if (!enemyFormation.Ready) return;

        shotTimer += Time.deltaTime;
        if (shotTimer >= shootCooldown)
            TryShoot();
    }

    private void TryShoot()
    {
        if (Time.time - lastShootTime < shootCooldown) return;

        lastShootTime = Time.time;
        PerformShot();
    }
    private void PerformShot()
    {
        bulletPool.GetFromPool(firePoint.position, Quaternion.identity);
        OnShoot?.Invoke();
    }

    #region Input
    private void OnAttack()
    {
        TryShoot();
    }
    #endregion
}