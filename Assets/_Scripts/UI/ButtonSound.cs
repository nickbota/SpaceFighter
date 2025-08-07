using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using Zenject;

[RequireComponent(typeof(Button))]
public class ButtonSound : MonoBehaviour, IPointerEnterHandler
{
    [Header("Click Sound")]
    [SerializeField] private AudioClip[] onClickSounds;

    [Header("Hover Sound")]
    [SerializeField] private AudioClip[] onHoverSounds;
    private Button button;

    private SoundManager soundManager;
    [Inject]
    private void Init(SoundManager soundManager)
    {
        this.soundManager = soundManager;
    }

    private void Awake()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(PlayButtonSounds);
    }
    private void OnDestroy()
    {
        button.onClick.RemoveAllListeners();
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        PlayHoverSounds();
    }

    private void PlayButtonSounds()
    {
        if (onClickSounds == null || onClickSounds.Length == 0) return;

        foreach (var sound in onClickSounds)
            soundManager?.PlaySound(sound);
    }
    private void PlayHoverSounds()
    {
        if (onHoverSounds == null || onHoverSounds.Length == 0) return;

        foreach (var sound in onHoverSounds)
            soundManager?.PlaySound(sound);
    }
}