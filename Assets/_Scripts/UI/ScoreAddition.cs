using TMPro;
using UnityEngine;
using Zenject;

public class ScoreAddition : MonoBehaviour
{
    [SerializeField] private AudioClip scoreSound;
    [SerializeField] private TextMeshProUGUI scoreAdditionText;
    [SerializeField] private RectTransform rectTransform;

    private SoundManager soundManager;
    [Inject]
    private void Init(SoundManager soundManager)
    {
        this.soundManager = soundManager;
    }

    public void ShowScoreAddition(Vector3 position, string text)
    {
        scoreAdditionText.text = text;
        rectTransform.position = Camera.main.WorldToScreenPoint(position);
        scoreAdditionText.gameObject.SetActive(true);
        soundManager.PlaySound(scoreSound, true);
    }
}