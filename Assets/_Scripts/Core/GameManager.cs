using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public enum GameState
    { 
        Menu,
        Game,
        Paused,
        Over
    }
    public Action<GameState> OnGameStateChanged { get; set; }
    private GameState currentGameState;

    private void Awake()
    {
        ChangeGameState(GameState.Menu);
    }
    public void ChangeGameState(GameState newState)
    {
        currentGameState = newState;
        OnGameStateChanged?.Invoke(currentGameState);
    }
}