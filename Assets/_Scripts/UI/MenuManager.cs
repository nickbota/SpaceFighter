using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

public class MenuManager : MonoBehaviour
{
    [SerializeField] private GameObject menuUI;
    [SerializeField] private GameObject gameUI;
    [SerializeField] private GameObject pauseUI;
    [SerializeField] private GameObject gameOverUI;

    private GameManager gameManager;
    [Inject]
    private void Init(GameManager gameManager)
    {
        this.gameManager = gameManager;
    }

    private void Awake()
    {
        ActivateMenu(menuUI);
    }
    private void OnEnable()
    {
        gameManager.OnGameStateChanged += OnGameStateChanged;
    }
    private void OnDisable()
    {
        gameManager.OnGameStateChanged -= OnGameStateChanged;
    }

    private void OnGameStateChanged(GameManager.GameState gameState)
    {
        switch (gameState)
        {
            case GameManager.GameState.Menu:
                ActivateMenu(menuUI);
                break;
            case GameManager.GameState.Game:
                ActivateMenu(gameUI);
                break;
            case GameManager.GameState.Paused:
                ActivateMenu(pauseUI);
                break;
            case GameManager.GameState.Over:
                ActivateMenu(gameOverUI);
                break;
        }
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void ActivateMenu(GameObject menu)
    {
        DeactivateAllMenus();
        menu.SetActive(true);
    }
    private void DeactivateAllMenus()
    {
        menuUI.SetActive(false);
        gameUI.SetActive(false);
        pauseUI.SetActive(false);
        gameOverUI.SetActive(false);
    }
}