using UnityEngine;
using System.Collections.Generic;
using Zenject;
using DG.Tweening;

public class EnemyFormation : MonoBehaviour
{
    [Header("Enemy Prefab")]
    [SerializeField] private GameObject enemyPrefab;

    [Header("Grid Size")]
    [SerializeField] private int rows = 3;
    [SerializeField] private int columns = 6;
    [SerializeField] private float spacing = 1.5f;

    [Header("Horizontal Movement")]
    [SerializeField] private float xLimit = 4;
    [SerializeField] private float moveSpeed = 1f;
    [SerializeField] private float accelerationPerKilledEnemy = 0.05f;

    [Header("Vertical Movement")]
    [SerializeField] private float dropDistance = 0.5f;
    [SerializeField] private float dropDuration = 0.5f;

    [Header("Shooting Parameters")]
    [SerializeField] private float shotCooldown = 3f;
    [SerializeField] private ObjectPool enemyBulletPool;
    private float shotTimer;

    private Vector3 direction = Vector3.right;
    private List<Transform> enemies = new List<Transform>();
    private float currentYPosition;
    private int flyInsToComplete;
    private bool ready;

    private GameManager gameManager;
    [Inject]
    private void Init(GameManager gameManager)
    {
        this.gameManager = gameManager;
    }

    private void Awake()
    {
        enabled = false;
    }
    private void Start()
    {
        SpawnFormation();
        currentYPosition = transform.position.y;
    }
    private void Update()
    {
        if (!ready) return;

        MoveFormation();
        moveSpeed = 1f + (enemies.Count - ActiveEnemyCount()) * accelerationPerKilledEnemy;

        shotTimer += Time.deltaTime;
        if (shotTimer >= shotCooldown)
            PerformShot();
    }

    public void AddScore(int score)
    {
        gameManager.AddScore(score);
    }

    private void SpawnFormation()
    {
        ready = false;
        enemies.Clear();
        flyInsToComplete = rows * columns;

        for (int row = 0; row < rows; row++)
        {
            for (int col = 0; col < columns; col++)
            {
                Vector3 finalPos = transform.position + new Vector3(col * spacing, -row * spacing, 0);
                Vector3 initPos = finalPos + new Vector3(0, 15, 0);

                GameObject enemy = Instantiate(enemyPrefab, initPos, Quaternion.identity, transform);
                enemies.Add(enemy.transform);

                //Fly-in animation
                float duration = Random.Range(0.25f, 1);
                float delay = row * 0.1f + Random.Range(0f, 0.1f);

                enemy.transform.DOMove(finalPos, duration)
                    .SetEase(Ease.InOutBack).SetDelay(delay).OnComplete(()=>
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
                direction *= -1;
                currentYPosition -= dropDistance;
                transform.DOMoveY(currentYPosition, dropDuration);

                break;
            }
        }
    }
    private void PerformShot()
    {
        shotTimer = 0;

        //Choose random bottom enemy to perform shot
        List<Transform> bottomEnemies = GetBottomEnemies();
        Transform firePoint = bottomEnemies[Random.Range(0, bottomEnemies.Count)];

        if (enemyBulletPool != null && firePoint != null)
            enemyBulletPool.GetFromPool(firePoint.position, Quaternion.identity);
    }
    private int ActiveEnemyCount()
    {
        int count = 0;
        foreach (var enemy in enemies)
        {
            if (enemy != null)
                count++;
        }
        return count;
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