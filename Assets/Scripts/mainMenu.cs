using UnityEngine;
using UnityEngine.SceneManagement;

public class mainMenu : MonoBehaviour
{
    public void playButton()
    {
        SceneManager.LoadScene("Jogo");
    }

    public void quitButton()
    {
        Application.Quit();
    }

    public void backToMenu()
    {
        SceneManager.LoadScene("Menu");
    }
}