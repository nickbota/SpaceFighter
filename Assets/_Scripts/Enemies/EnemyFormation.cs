using UnityEngine;
using System.Collections.Generic;
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

    [Header("Vertical Movement")]
    [SerializeField] private float dropDistance = 0.5f;
    [SerializeField] private float dropDuration = 0.5f;

    private Vector3 direction = Vector3.right;
    private List<Transform> enemies = new List<Transform>();
    private float currentYPosition;

    private void Awake()
    {
        SpawnFormation();
        currentYPosition = transform.position.y;
    }
    private void Update()
    {
        MoveFormation();
    }

    private void SpawnFormation()
    {
        enemies.Clear();
        for (int row = 0; row < rows; row++)
        {
            for (int col = 0; col < columns; col++)
            {
                Vector3 pos = transform.position + new Vector3(col * spacing, -row * spacing, 0);
                GameObject enemy = Instantiate(enemyPrefab, pos, Quaternion.identity, transform);
                enemies.Add(enemy.transform);
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
}
