using UnityEngine;

public class HighScoreCounter : Counter
{
    private void Start()
    {
        int highScore = PlayerPrefs.GetInt("Highscore");
        UpdateCounter(highScore.ToString());
    }
}