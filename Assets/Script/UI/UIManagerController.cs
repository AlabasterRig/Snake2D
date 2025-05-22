using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManagerController : MonoBehaviour
{
    public GameObject PauseMenuUI;
    public GameObject GameOverUI;
    public GameObject WinUI;
    public GameObject HUD;

    private bool isPaused = false;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePause();
        }
    }

    public void TogglePause()
    {
        isPaused = !isPaused;
        PauseMenuUI.SetActive(isPaused);
        HUD.SetActive(!isPaused);
        Time.timeScale = isPaused ? 0 : 1;
    }

    public void ShowGameOver()
    {
        Time.timeScale = 0;
        HUD.SetActive(false);
        GameOverUI.SetActive(true);
    }

    public void ShowWin()
    {
        Time.timeScale = 0;
        HUD.SetActive(false);
        WinUI.SetActive(true);
    }

    public void ResumeGame()
    {
        TogglePause();
    }

    public void RestartGame()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
