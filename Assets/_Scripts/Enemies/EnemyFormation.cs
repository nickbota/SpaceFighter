using UnityEngine;
using System.Collections.Generic;
using Zenject;
using DG.Tweening;

public class EnemyFormation : MonoBehaviour
{
    [Header("Enemy Formation")]
    [SerializeField] private ObjectPool enemyPool;
    [SerializeField] private int rows = 3;
    [SerializeField] private int columns = 6;
    [SerializeField] private float spacing = 1.5f;

    [Header("Horizontal Movement")]
    [SerializeField] private float xLimit = 4;
    [SerializeField] private float moveSpeed = 1f;
    [SerializeField] private float accelerationPerKilledEnemy = 0.1f;

    [Header("Vertical Movement")]
    [SerializeField] private float dropDistance = 0.5f;
    [SerializeField] private float dropDuration = 0.5f;

    [Header("Shooting Parameters")]
    [SerializeField] private float shotCooldown = 3f;
    [SerializeField] private ObjectPool enemyBulletPool;
    private float shotTimer;

    [Header("Sounds")]
    [SerializeField] private AudioClip enemyShotSound;
    [SerializeField] private AudioClip enemyDeathSound;
    [SerializeField] private AudioClip flyInSound;

    private Vector3 direction = Vector3.right;
    private List<Transform> enemies = new List<Transform>();
    private float currentYPosition;
    private int flyInsToComplete;
    private bool ready;
    private int activeEnemyCount;

    private GameManager gameManager;
    private SoundManager soundManager;
    [Inject]
    private void Init(GameManager gameManager, SoundManager soundManager)
    {
        this.gameManager = gameManager;
        this.soundManager = soundManager;
    }

    private void Awake()
    {
        enabled = false;
    }
    private void Start()
    {
        SpawnFormation();
        UpdateEnemyCount();
    }
    private void Update()
    {
        if (!ready) return;

        MoveFormation();
        moveSpeed = 1f + (enemies.Count - activeEnemyCount) * accelerationPerKilledEnemy;

        shotTimer += Time.deltaTime;
        if (shotTimer >= shotCooldown)
            PerformShot();
    }

    public void AddScore(int score, GameObject enemy)
    {
        enemyPool?.ReturnToPool(enemy);
        gameManager.AddScore(score);
        soundManager.PlaySound(enemyDeathSound);
        UpdateEnemyCount();

        if (activeEnemyCount == 0 && ready)
            SpawnFormation();
    }

    private void SpawnFormation()
    {
        currentYPosition = transform.position.y;
        ready = false;
        enemies.Clear();
        flyInsToComplete = rows * columns;
        Debug.Log("Spawning new formation");

        for (int row = 0; row < rows; row++)
        {
            for (int col = 0; col < columns; col++)
            {
                Vector3 finalPos = transform.position + new Vector3(col * spacing, -row * spacing, 0);
                Vector3 initPos = finalPos + new Vector3(0, 15, 0);
                GameObject enemy = null;

                if (enemyPool != null)
                    enemy = enemyPool.GetFromPool(initPos, Quaternion.identity);

                if(!enemies.Contains(enemy.transform))
                    enemies.Add(enemy.transform);

                //Fly-in animation
                float duration = Random.Range(0.25f, 1);
                float delay = row * 0.1f + Random.Range(0f, 0.1f);

                enemy.transform.DOMove(finalPos, duration)
                    .SetEase(Ease.InOutBack).SetDelay(delay)
                    .OnStart(() => soundManager.PlaySound(flyInSound))
                    .OnComplete(() =>
                    {
                        flyInsToComplete--;
                        if (flyInsToComplete == 0)
                            ready = true;
                    });
            }
        }
    }
    private void MoveFormation()
    {
        transform.position += direction * moveSpeed * Time.deltaTime;

        //Check each enemy to see if they're outside of the movement bouds
        foreach (var enemy in enemies)
        {
            if (enemy == null) continue;
            float enemyX = enemy.position.x;

            //If yes, drop the formation and change the movement direction
            if (enemyX < -xLimit || enemyX > xLimit)
            {
                ready = false;

                currentYPosition -= dropDistance;
                transform.DOMoveY(currentYPosition, dropDuration).OnComplete(()=>
                {
                    direction *= -1;
                    transform.position += direction * 0.1f;
                    ready = true;
                });

                break;
            }
        }
    }
    private void PerformShot()
    {
        shotTimer = 0;

        //Choose random bottom enemy to perform shot
        List<Transform> bottomEnemies = GetBottomEnemies();
        if (bottomEnemies.Count == 0) return;

        Transform firePoint = bottomEnemies[Random.Range(0, bottomEnemies.Count)];

        if (enemyBulletPool != null && firePoint != null)
            enemyBulletPool.GetFromPool(firePoint.position, Quaternion.identity);

        soundManager.PlaySound(enemyShotSound);
    }
    private void UpdateEnemyCount()
    {
        int count = 0;
        foreach (var enemy in enemies)
        {
            if (enemy.gameObject.activeInHierarchy)
                count++;
        }
        activeEnemyCount = count;
    }
    private List<Transform> GetBottomEnemies()
    {
        Dictionary<int, Transform> bottomEnemies = new Dictionary<int, Transform>();

        foreach (var enemy in enemies)
        {
            if (enemy == null || !enemy.gameObject.activeInHierarchy) continue;

            int xKey = Mathf.RoundToInt(enemy.localPosition.x * 100f); // group by X
            if (!bottomEnemies.ContainsKey(xKey) || enemy.localPosition.y < bottomEnemies[xKey].localPosition.y)
                bottomEnemies[xKey] = enemy;
        }

        return new List<Transform>(bottomEnemies.Values);
    }
}