using UnityEngine;
using Zenject;

public class WaveCounter : Counter
{
    private GameManager gameManager;
    [Inject]
    private void Init(GameManager gameManager)
    {
        this.gameManager = gameManager;
    }

    private void Start()
    {
        UpdateCounter(gameManager.CurrentWave.ToString());
    }
}