using TMPro;
using UnityEngine;

public class MenuController : MonoBehaviour
{
    [SerializeField] private AudioClip menuMusic;

    [Header("Other")]
    [SerializeField] private GameObject randomizedInfoTextObject;
    [SerializeField] private TMP_InputField seedInputField;

    public static int seed = 0;

    [SerializeField] private string firstLevel;

    [SerializeField] private GameObject optionsScreen;
    [SerializeField] private GameObject newGameScreen;

    private void Start()
    {
        MusicPlayer.instance.StartMusic(menuMusic);

        ValidateSeed();
    }

    private void Update()
    {
        ValidateSeed();
    }

    // Get the last generated seed saved in BreachManager and set the input to that
    public void GetLastGeneratedSeed()
    {
        int lastGeneratedSeed = PlayerPrefs.GetInt("LastGeneratedSeed", 0);
        seedInputField.text = lastGeneratedSeed.ToString();
    }

    private void ValidateSeed()
    {
        seed = int.TryParse(seedInputField.text, out var parsedSeed) ? parsedSeed : 0;
        randomizedInfoTextObject.SetActive(seed == 0);
    }

    public void TransferSeedValue()
    {
        seed = int.TryParse(seedInputField.text, out var parsedSeed) ? parsedSeed : 0;
    }

    public void OpenNewGame()
    {
        newGameScreen.SetActive(true);
    }

    public void CloseNewGame()
    {
        newGameScreen.SetActive(false);
    }

    public void OpenOptions()
    {
        optionsScreen.SetActive(true);
    }

    public void CloseOptions()
    {
        optionsScreen.SetActive(false);
    }

    public void QuitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
        Debug.Log("Closing Executable.");
    }

    public void OpenSkethfab()
    {
        Application.OpenURL("https://sketchfab.com/ThatJamGuy");
    }

    public void OpenItch()
    {
        Application.OpenURL("https://thatjamguy.itch.io");
    }

    public void OpenYouTube()
    {
        Application.OpenURL("https://www.youtube.com/channel/UCFKWusqC2MCykahD247P3bw");
    }

    public void OpenDiscord()
    {
        Application.OpenURL("https://discord.gg/fthrWUtZyS");
    }
}