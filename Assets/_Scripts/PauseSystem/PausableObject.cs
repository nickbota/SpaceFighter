using UnityEngine;
using System.Collections.Generic;
using Zenject;

public class PausableObject : MonoBehaviour
{
    private List<Behaviour> components = new List<Behaviour>();

    private GameManager gameManager;
    [Inject]
    private void Init(GameManager gameManager)
    {
        this.gameManager = gameManager;
    }
    public void SetGameManager(GameManager gameManager)
    {
        this.gameManager = gameManager;
        gameManager.OnGameStateChanged += OnGameStateChanged;
    }

    private void Awake()
    {
        GetPausableComponents();
    }
    private void OnEnable()
    {
        if (gameManager == null) return;
        gameManager.OnGameStateChanged += OnGameStateChanged;
    }
    private void OnDisable()
    {
        if (gameManager == null) return;
        gameManager.OnGameStateChanged -= OnGameStateChanged;
    }

    private void GetPausableComponents()
    {
        // Get all enabled MonoBehaviours on this object (excluding this script)
        foreach (var component in GetComponents<MonoBehaviour>())
        {
            if (component != this)
                components.Add(component);
        }
    }

    public void OnGameStateChanged(GameManager.GameState gameState)
    {
        bool paused = (gameState != GameManager.GameState.Game);

        foreach (var component in components)
        {
            if (component != null)
                component.enabled = !paused;
        }
    }
}