using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    [SerializeField] private string firstLevel;

    [SerializeField] private GameObject optionsScreen;
    [SerializeField] private GameObject newGameScreen;

    public void StartGame()
    {
        SceneManager.LoadScene(firstLevel);
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