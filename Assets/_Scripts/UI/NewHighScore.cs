using UnityEngine;
using Zenject;

public class NewHighScore : MonoBehaviour
{
    [SerializeField] private GameObject newHighScore;

    private GameManager gameManager;
    [Inject]
    private void Init(GameManager gameManager)
    {
        this.gameManager = gameManager;
    }

    private void Start()
    {
        int highScore = PlayerPrefs.GetInt("Highscore");
        newHighScore.SetActive(gameManager.CurrentScore == highScore);
    }
}