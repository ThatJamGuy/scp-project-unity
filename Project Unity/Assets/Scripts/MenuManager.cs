using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public string gameScene;

    public void NewGame()
    {
        SceneManager.LoadScene(gameScene);
    }

    public void QuiteGame()
    {
        Application.Quit();
    }
}