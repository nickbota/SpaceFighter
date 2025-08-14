using UnityEngine;
using Zenject;

public class HighWaveCounter : Counter
{
    private void Start()
    {
        int highWave = PlayerPrefs.GetInt("HighWave");
        UpdateCounter(highWave.ToString());
    }
}