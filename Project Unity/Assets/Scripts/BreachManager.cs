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

    // This will be set to the result of the seed upon generation and will be saved for the menu to access
    [Tooltip("What the current seed of the map is")]
    public int currentSeed;

    [Header("Zone Music")]
    [SerializeField] private AudioClip zone1Music;

    [Header("General Audio")]
    [SerializeField] AudioClip[] ambience;
    [SerializeField] AudioClip[] commotion;

    [Header("Audio Sources")]
    [SerializeField] AudioSource ambienceSource;
    [SerializeField] AudioSource commotionSource;

    private int currentCommotionIndex = 0;

    private void OnEnable()
    {
        // Set the seed to the one defined in the menu
        generationSeed = MenuController.seed;
        RegenerateNewGen.seed = generationSeed;

        StartCoroutine(GetCurrentSeedAfterSeconds(0.1f));
    }

    private void Start()
    {
        PlayNextCommotion();
        StartCoroutine(Ambience());

        MusicPlayer.instance.ChangeMusic(zone1Music);
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

    // Save lesser data like that last generated seed. Will be accessed by main menu
    public void SavePrefs()
    {
        PlayerPrefs.SetInt("LastGeneratedSeed", currentSeed);
        PlayerPrefs.Save();
    }

    IEnumerator GetCurrentSeedAfterSeconds(float seconds)
    {
        yield return new WaitForSeconds(seconds);

        // Get the processed seed from NewGen after seconds and set the currentSeed to that
        currentSeed = NewGen.newGenGeneratedSeed;
        Debug.Log("The current seed is " + currentSeed);

        SavePrefs();
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
}