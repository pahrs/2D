using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public class mainMenu : MonoBehaviour
{
    public void playButton()
    {
        SceneManager.LoadScene(2);
    }

    public void quitButton()
    {
        Application.Quit();
    }
}
