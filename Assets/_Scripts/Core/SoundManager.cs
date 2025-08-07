using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class SoundManager : MonoBehaviour
{
    private AudioSource soundSource;

    private void Awake()
    {
        soundSource = GetComponent<AudioSource>();
    }

    public void PlaySound(AudioClip sound)
    {
        if (sound == null || soundSource == null) return;

        //Randomize pitch to make SFX sound different
        soundSource.pitch = Random.Range(0.8f, 1.2f);
        soundSource.PlayOneShot(sound);
    }
}