using UnityEngine;
using UnityEngine.SceneManagement;

public class MusicAndSceneController : MonoBehaviour
{
    public AudioSource musicSource; 
    public string nextSceneName; 
    private float sceneTransitionTime = 135.0f; 

    void Start()
    {
        if (musicSource != null)
        {
            musicSource.Play();
        }

        Invoke("ChangeScene", sceneTransitionTime);
    }

    void ChangeScene()
    {
        SceneManager.LoadScene(nextSceneName);
    }
}