using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    //Game state
    public enum GameState
    {
        Menu,
        Game,
        Paused,
        Over
    }
    public Action<GameState> OnGameStateChanged { get; set; }
    private GameState currentGameState;

    //Score
    private int currentScore;
    public int CurrentScore => currentScore;
    public Action<int> OnScoreChanged { get; set; }

    private void Awake()
    {
        ChangeGameState(GameState.Menu);
    }
    public void ChangeGameState(GameState newState)
    {
        currentGameState = newState;
        OnGameStateChanged?.Invoke(currentGameState);
    }

    public void StartGame()
    {
        ChangeGameState(GameState.Game);
    }
    public void PauseGame()
    {
        ChangeGameState(GameState.Paused);
    }
    public void EndGame()
    {
        int highScore = PlayerPrefs.GetInt("Highscore");
        if (currentScore > highScore)
            PlayerPrefs.SetInt("Highscore", currentScore);

        ChangeGameState(GameState.Over);
    }
    public void AddScore(int scoreToAdd)
    {
        currentScore += scoreToAdd;
        OnScoreChanged?.Invoke(currentScore);
    }
}