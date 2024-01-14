using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class MenuManager : MonoBehaviour
{
    public string gameScene;

    [Header("Game Values")]
    public TMP_InputField saveName;

    private string localGameName;

    public void NewGame()
    {
        localGameName = saveName.text;
    }

    public void QuiteGame()
    {
        Application.Quit();
    }
}