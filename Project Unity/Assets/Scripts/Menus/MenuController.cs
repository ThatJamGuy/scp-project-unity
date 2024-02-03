using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    public string firstLevel;

    public GameObject optionsScreen;

    public void StartGame()
    {
        SceneManager.LoadScene(firstLevel);
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