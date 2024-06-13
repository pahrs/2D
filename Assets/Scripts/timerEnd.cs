using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;


public class timerEnd : MonoBehaviour
{

    public int minutes = 2;
    public int additionalMinutes = 30;
    private float seconds;
    private TextMeshProUGUI timerText;
    
    void Start()
    {
        seconds = (minutes * 60) + additionalMinutes;
        timerText = GetComponent<TextMeshProUGUI>();
        UpdateTimerDisplay();
    
    }

    
    void Update()
    {
        if(seconds > 0)
        {
            seconds -= Time.deltaTime;
            UpdateTimerDisplay(); 
        }
        else
        {
            EndGame();
        }
    }

    private void UpdateTimerDisplay()
    {
        int minutesLeft = Mathf.FloorToInt(seconds / 60);
        int secondsLeft = Mathf.FloorToInt(seconds % 60);
        timerText.text = string.Format("{0:00}:{1:00}", minutesLeft, secondsLeft); 
    }

        private void EndGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

}
