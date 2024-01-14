using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class MenuManager : MonoBehaviour
{
    public string gameScene;

    [Header("Game Values")]
    public TMP_InputField saveName;

    private string localGameName;

    public void RefreshGameName()
    {
        localGameName = saveName.text;
        Debug.Log(localGameName);
    }

    public void NewGame()
    {
        Debug.Log("Creating game '" + localGameName + "' under seed (_Seed)");
        SceneManager.LoadScene(gameScene);
    }

    public void QuiteGame()
    {
        Application.Quit();
    }
}