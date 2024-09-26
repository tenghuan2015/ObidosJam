using System.Collections;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance = null;
    private AudioSource sfxSource;               // AudioSource for sound effects
    private AudioSource musicSource;

    public AudioClip DoorKnock;
    public AudioClip Footsteps;
    public AudioClip BreakDoorNoise;
    public AudioClip ChildCrying;
    public AudioClip HardDoorKnock;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
        sfxSource = gameObject.AddComponent<AudioSource>();    // For sound effects
        musicSource = gameObject.AddComponent<AudioSource>();
        musicSource.loop = true;
    }

    public void PlaySound(AudioClip clip)
    {
        if (clip != null)
        {
            sfxSource.clip = clip;
            sfxSource.loop = false;  // Ensure it's not looped
            sfxSource.Play();
        }
    }

    public IEnumerator PlaySoundLoop(AudioClip clip, float delayBetweenLoops)
    {
        if (clip != null)
        {
            while (true) // Infinite loop until you manually stop it
            {
                sfxSource.clip = clip;
                sfxSource.Play();

                // Wait for the clip to finish playing
                yield return new WaitForSeconds(clip.length);

                // Wait for the additional delay between loops
                yield return new WaitForSeconds(delayBetweenLoops);
            }
        }
    }
    public void StopSound()
    {
        sfxSource.Stop();
    }
    public bool IsPlaying()
    {
        return sfxSource.isPlaying;
    }
    public void PlayBackgroundMusic(AudioClip musicClip)
    {
        if (musicClip != null && !musicSource.isPlaying)
        {
            musicSource.clip = musicClip;
            musicSource.Play();
        }
    }

    // Function to stop background music
    public void StopBackgroundMusic()
    {
        if (musicSource.isPlaying)
        {
            musicSource.Stop();
        }
    }

    // Function to check if background music is playing
    public bool IsBackgroundMusicPlaying()
    {
        return musicSource.isPlaying;
    }
}
