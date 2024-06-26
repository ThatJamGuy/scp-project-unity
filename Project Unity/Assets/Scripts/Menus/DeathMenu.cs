using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathMenu : MonoBehaviour
{
    public GameObject deathPanel;
    public bool isDeadMenu = false;

    public void Die()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        deathPanel.SetActive(true);
        //Time.timeScale = 0;
        isDeadMenu = true;
    }

    public void QuitToMenu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("MainMenu");
    }
}