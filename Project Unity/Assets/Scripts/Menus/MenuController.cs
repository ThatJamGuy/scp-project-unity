using TMPro;
using UnityEngine;

public class MenuController : MonoBehaviour
{
    [SerializeField] private GameObject randomizedInfoTextObject;
    [SerializeField] private TMP_InputField seedInputField;

    public static int seed = 0;

    [SerializeField] private string firstLevel;

    [SerializeField] private GameObject optionsScreen;
    [SerializeField] private GameObject newGameScreen;

    // Checks if the seed input is 0. If so, display the info saying that the seed will be random upon generation
    private void Update()
    {
        if (seedInputField.text == "0")
            randomizedInfoTextObject.SetActive(true);
        else
            randomizedInfoTextObject.SetActive(false);
    }

    // Get the seed from the seed input and set the seed value to it
    // If no valid seed is detected, automatcically set it to 0
    public void TransferSeedValue()
    {
        seed = int.Parse(seedInputField.text);

        if (seedInputField == null)
        {
            seedInputField.text = "0";

            seed = int.Parse(seedInputField.text);
        }
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
        Application.Quit();
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