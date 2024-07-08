using UnityEngine;

public class MusicPlayer : MonoBehaviour
{
    public static MusicPlayer instance = null;
    public AudioSource Music;

    private bool changeTrack, changed;
    private AudioClip trackTo;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != null)
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);
        Music.ignoreListenerPause = true;
        Debug.Log("MusicPlayer initialized.");
    }

    private void FixedUpdate()
    {
        if (changeTrack == true)
        {
            Debug.Log("Attempting to change music track.");
            MusicChanging();
        }
    }

    public void ChangeMusic(AudioClip newMusic)
    {
        changeTrack = true;
        trackTo = newMusic;
        changed = false;
        Debug.Log($"Changed music clip to {newMusic.name}");
    }

    public void StartMusic(AudioClip newMusic)
    {
        Music.Stop();
        Music.volume = 1f;
        Music.clip = newMusic;
        Music.Play();
        Debug.Log($"Started playing music clip: {newMusic.name}");
    }

    public void StopMusic()
    {
        changeTrack = true;
        trackTo = null;
        changed = false;
        Debug.Log("Stopping music.");
    }

    void MusicChanging()
    {
        if (changed == false)
        {
            Debug.Log("Fading out current music track.");
            Music.volume -= (Time.deltaTime) / 4;
        }

        if (Music.volume <= 0.1 && changed == false && trackTo != null)
        {
            changed = true;
            Debug.Log("Switching to new music track.");
            Music.clip = trackTo;
            Music.Play();
        }

        if (changed == true)
        {
            Debug.Log("Fading in new music track.");
            Music.volume += Time.deltaTime;
        }

        if (Music.volume >= 1 && changed == true)
        {
            changeTrack = false;
            Debug.Log("Finished fading in new music track.");
        }
    }
}