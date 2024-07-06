using ALOB.Map;
using System.Collections;
using UnityEngine;

/// <summary>
/// Manages various Breach Mode aspects, mostly centered around audio at the moment
/// </summary>
public class BreachManager : MonoBehaviour
{
    [Header("Map Generation")]
    [Tooltip("Set value to 0 for random")]
    public int generationSeed;

    [Header("Values")]
    public float musicFadeDuration = 1.0f;

    [Header("Audio")]
    [SerializeField] AudioClip breachSequence;
    [SerializeField] AudioClip alarmSource;
    [SerializeField] AudioClip[] ambience;
    [SerializeField] AudioClip[] commotion;
    public AudioClip zone1Music;

    [Header("Audio Sources")]
    [SerializeField] AudioSource breachSource;
    [SerializeField] AudioSource breachAlarm;
    [SerializeField] AudioSource ambienceSource;
    [SerializeField] AudioSource commotionSource;
    [SerializeField] AudioSource musicSource;

    private int currentCommotionIndex = 0;

    private void OnEnable()
    {
        generationSeed = MenuController.seed;
        RegenerateNewGen.seed = generationSeed;
    }

    private void Start()
    {
        StartBreach();
        PlayNextCommotion();
        StartCoroutine(Ambience());
        SetInitialMusicSource(zone1Music);
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
            Invoke(nameof(PlayNextCommotion), 15f);
        }
        else
        {
            commotionSource.Stop();
        }
    }

    IEnumerator Ambience()
    {
        while (true)
        {
            if (ambience != null && ambience.Length > 0)
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
    }

    private void SetInitialMusicSource(AudioClip clip)
    {
        musicSource.clip = clip;
        musicSource.Play();
    }
}