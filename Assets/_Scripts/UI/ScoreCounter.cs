using UnityEngine;
using Zenject;

public class ScoreCounter : Counter
{
    private GameManager gameManager;
    [Inject]
    private void Init(GameManager gameManager)
    {
        this.gameManager = gameManager;
    }

    private void OnEnable()
    {
        gameManager.OnScoreChanged += OnScoreChanged;
    }
    private void OnDisable()
    {
        gameManager.OnScoreChanged -= OnScoreChanged;
    }
    private void Start()
    {
        OnScoreChanged(gameManager.CurrentScore);
    }

    private void OnScoreChanged(int score)
    {
        string finalScoreString = "";
        if (score < 10)                   finalScoreString = "000" + score;
        if (score >= 10 && score < 100)   finalScoreString = "00" + score;
        if (score >= 100 && score < 1000) finalScoreString = "0" + score;
        if (score >= 1000)                finalScoreString = score.ToString();

        UpdateCounter(finalScoreString);
    }
}