using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class MenuManager : MonoBehaviour
{
    public string menuScene;
    public string gameScene;

    [Header("Game Values")]
    public TMP_InputField saveName;

    [Header("Other")]
    public GameObject pauseMenu;
    public bool isPaused;

    private string localGameName;

    public void RefreshGameName()
    {
        localGameName = saveName.text;
        Debug.Log(localGameName);
    }

    public void ToggleMouseSettings()
    {
        if (!isPaused)
        {
            isPaused = true;
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }

        if (isPaused)
        {
            isPaused = false;
            Cursor.lockState = CursorLockMode.Locked; 
            Cursor.visible = false;
        }
    }

    public void NewGame()
    {
        Debug.Log("Creating game '" + localGameName + "' under seed (_Seed)");
        SceneManager.LoadScene(gameScene);
    }

    public void PauseGame()
    {
        pauseMenu.gameObject.SetActive(true);
        isPaused = true;
        //ToggleMouseSettings();
    }

    public void QuitToMenu()
    {
        SceneManager.LoadScene(menuScene);
    }

    public void QuiteGame()
    {
        Application.Quit();
    }
}