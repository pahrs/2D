using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public class mainMenu : MonoBehaviour
{
    public void playButton()
    {
        SceneManager.LoadScene("Paulo");
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