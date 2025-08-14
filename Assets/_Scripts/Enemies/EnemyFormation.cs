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
    [SerializeField] private float initialMoveSpeed = 1f;
    [SerializeField] private float accelerationPerKilledEnemy = 0.1f;
    [SerializeField] private float accelerationPerWave = 0.15f;

    [Header("Vertical Movement")]
    [SerializeField] private float dropDistance = 0.5f;
    [SerializeField] private float dropDuration = 0.5f;

    [Header("Shooting Parameters")]
    [SerializeField] private float shotCooldown = 3f;
    [SerializeField] private ObjectPool enemyBulletPool;
    private float shotTimer;

    [Header("Sounds")]
    [SerializeField] private AudioClip newWaveSound;
    [SerializeField] private AudioClip enemyShotSound;
    [SerializeField] private AudioClip enemyDeathSound;
    [SerializeField] private AudioClip flyInSound;

    private Vector3 direction = Vector3.right;
    private Vector3 initialPosition;
    private List<Transform> enemies = new List<Transform>();
    private float currentYPosition;
    private int flyInsToComplete;
    private bool ready;
    private int activeEnemyCount = 999;

    //Wave incoming parameters
    private bool waveIncoming;
    private float newWaveTimer;
    private float newWaveDelay = 2;
    private int waveNumber = -1;

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
        initialPosition = transform.position;
    }
    private void Start()
    {
        SpawnNewWave();
    }
    private void Update()
    {
        if (waveIncoming)
        {
            newWaveTimer += Time.deltaTime;
            if (newWaveTimer > newWaveDelay)
            {
                SpawnFormation();
                waveIncoming = false;
            }
        }

        if (!ready) return;

        initialMoveSpeed = 1f + (enemies.Count - activeEnemyCount) * accelerationPerKilledEnemy + (accelerationPerWave * waveNumber);
        MoveFormation();

        shotTimer += Time.deltaTime;
        if (shotTimer >= shotCooldown)
            PerformShot();
    }

    public void AddScore(int score, GameObject enemy)
    {
        gameManager.AddScore(score);
        soundManager.PlaySound(enemyDeathSound);
        UpdateEnemyCount();
    }

    private void SpawnFormation()
    {
        waveNumber++;
        transform.position = initialPosition;
        currentYPosition = transform.position.y;
        enemies.Clear();
        flyInsToComplete = rows * columns;

        for (int row = 0; row < rows; row++)
        {
            for (int col = 0; col < columns; col++)
            {
                Vector3 finalPos = transform.position + new Vector3(col * spacing, -row * spacing, 0);
                Vector3 initPos = finalPos + new Vector3(0, 15, 0);
                GameObject enemy = null;
                enemy = enemyPool.GetFromPool(initPos, Quaternion.identity);

                Debug.Log($"{enemy.name} spawned!");

                if (!enemies.Contains(enemy.transform))
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

        UpdateEnemyCount();
    }
    private void MoveFormation()
    {
        transform.position += direction * initialMoveSpeed * Time.deltaTime;

        //Check each enemy to see if they're outside of the movement bouds
        foreach (var enemy in enemies)
        {
            if (enemy == null || !enemy.gameObject.activeInHierarchy) continue;
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

        if (activeEnemyCount == 0 && ready)
            SpawnNewWave();
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
    private void SpawnNewWave()
    {
        ready = false;
        newWaveTimer = 0;
        waveIncoming = true;
        soundManager.PlaySound(newWaveSound);
    }
}