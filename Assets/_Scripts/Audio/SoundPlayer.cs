using UnityEngine;
using Zenject;

public class SoundPlayer : MonoBehaviour
{
    private SoundManager soundManager;
    [Inject]
    private void Init(SoundManager soundManager)
    {
        this.soundManager = soundManager;
    }

    public void PlaySound(AudioClip sound)
    {
        soundManager.PlaySound(sound, true);
    }
}