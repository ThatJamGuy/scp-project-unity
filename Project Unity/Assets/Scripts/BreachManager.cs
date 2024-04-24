using System.Collections;
using UnityEngine;

public class BreachManager : MonoBehaviour
{
    [Header("Values")]
    public float musicFadeDuration = 1.0f;

    //private bool isFading = false;

    [Header("Audio")]
    [SerializeField] AudioClip breachSequence;
    [SerializeField] AudioClip[] ambience;
    [SerializeField] AudioClip[] commotion;
    public AudioClip zone1Music;

    [Header("Audio Sources")]
    [SerializeField] AudioSource breachSource;
    [SerializeField] AudioSource ambienceSource;
    [SerializeField] AudioSource commotionSource;
    [SerializeField] AudioSource musicSource;

    private int currentCommotionIndex = 0;

    private void Start()
    {
        StartBreach();
        PlayNextCommotion();
        StartCoroutine(Ambience());

        // Set initial music source
        musicSource.clip = zone1Music;
        musicSource.Play();
    }

    public void StartBreach()
    {
        breachSource.clip = breachSequence;
        breachSource.Play();
    }

    public void ChangeMusic(AudioClip newTrack)
    {
        StartCoroutine(CrossfadeMusic(newTrack));
    }

    private void PlayNextCommotion()
    {
        if (currentCommotionIndex < commotion.Length)
        {
            commotionSource.clip = commotion[currentCommotionIndex];
            commotionSource.Play();
            currentCommotionIndex++;
            Invoke("PlayNextCommotion", 15f);
        }
        else
        {
            commotionSource.Stop();
        }
    }

    IEnumerator Ambience()
    {
        while(true)
        {
            if (ambience != null)
            {
                int rand = Random.Range(0, ambience.Length);
                ambienceSource.panStereo = Random.Range(-1, 1);
                ambienceSource.clip = ambience[rand];
                ambienceSource.Play();
                yield return new WaitForSeconds(Random.Range(10, 30));
            }
        }
    }

    public IEnumerator CrossfadeMusic(AudioClip newTrack)
    {
        //isFading = true;

        float currentTime = 0f;
        float startVolume = musicSource.volume;

        while (currentTime < musicFadeDuration)
        {
            currentTime += Time.deltaTime;
            float t = currentTime / musicFadeDuration;
            musicSource.volume = Mathf.Lerp(startVolume, 0f, t);
            yield return null;
        }

        musicSource.Stop();

        musicSource.clip = newTrack;
        musicSource.Play();

        currentTime = 0f;

        while (currentTime < musicFadeDuration)
        {
            currentTime += Time.deltaTime;
            float t = currentTime / musicFadeDuration;
            musicSource.volume = Mathf.Lerp(0f, startVolume, t);
            yield return null;
        }

        //isFading = false;
    }
}