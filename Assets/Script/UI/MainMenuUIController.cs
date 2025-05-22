using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuUIController : MonoBehaviour
{
    public void StartGame()
    {
        SceneManager.LoadScene("Snake2D");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
