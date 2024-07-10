using ALOB.Map;
using System.Collections;
using UnityEngine;

public class BreachManager : MonoBehaviour
{
    [Header("Map Generation")]
    [Tooltip("Set value to 0 for random")]
    public int generationSeed;

    [Tooltip("What the current seed of the map is")]
    public int currentSeed;

    [Header("Zone Music")]
    public AudioClip zone1Music;

    private void OnEnable()
    {
        generationSeed = MenuController.seed;
        RegenerateNewGen.seed = generationSeed;

        StartCoroutine(GetCurrentSeedAfterSeconds(0.1f));
    }

    private void Start()
    {
        MusicPlayer.instance.ChangeMusic(zone1Music);
    }

    public void SavePrefs()
    {
        PlayerPrefs.SetInt("LastGeneratedSeed", currentSeed);
        PlayerPrefs.Save();
    }

    IEnumerator GetCurrentSeedAfterSeconds(float seconds)
    {
        yield return new WaitForSeconds(seconds);

        currentSeed = NewGen.newGenGeneratedSeed;
        Debug.Log("The current seed is " + currentSeed);

        SavePrefs();
    }
}