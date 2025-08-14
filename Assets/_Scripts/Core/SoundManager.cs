using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class SoundManager : MonoBehaviour
{
    private AudioSource soundSource;

    private void Awake()
    {
        soundSource = GetComponent<AudioSource>();
    }

    public void PlaySound(AudioClip sound, bool randomizePitch)
    {
        if (sound == null || soundSource == null) return;

        //Randomize pitch to make SFX sound different
        if(randomizePitch)
            soundSource.pitch = Random.Range(0.8f, 1.2f);
        else
            soundSource.pitch = 1;

        soundSource.PlayOneShot(sound);
    }
}