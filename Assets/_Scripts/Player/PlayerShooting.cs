using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    [Header("Object Pool")]
    [SerializeField] private ObjectPool bulletPool;

    [Header("Shot Parameters")]
    [SerializeField] private Transform firePoint;
    [SerializeField] private float shootCooldown = 0.5f;
    private float lastShootTime = 0f;

    public void Shoot()
    {
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
        if (bulletPool != null && firePoint != null)
            bulletPool.GetFromPool(firePoint.position, Quaternion.identity);
    }
}