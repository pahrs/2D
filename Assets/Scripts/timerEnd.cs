using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Mail;
using TMPro;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class timerEnd : MonoBehaviour
{

    public int minutes = 2;
    public int additionalMinutes = 30;
    private float seconds;
    private TextMeshProUGUI timerText;
    public ligthHero ligthHero;
    public potatoFire potatoFire;
    private bool eventOneTriggered = false;
    private bool eventSecondTriggered = false;
    public float timeToEventOne = 10f;
    public float timeToEventTwo = 10f;

    void Start()
    {
        seconds = (minutes * 60) + additionalMinutes;
        timerText = GetComponent<TextMeshProUGUI>();
        UpdateTimerDisplay();

        StartCoroutine(EventManager());
    
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

    private IEnumerator EventManager()
    {
        // Wait for timeToEventOne minutes 
        yield return new WaitForSeconds(timeToEventOne);
        EventOne();

        // Wait for an additional timeToEventTwo seconds
        yield return new WaitForSeconds(timeToEventTwo);
        EventSecond();
    }

    public void EventOne()
    {
        // Randomly choose between lightHero and potatoFire
        if (UnityEngine.Random.Range(0, 2) == 0)
        {
            ligthHero.Activate();
        }
        else
        {
            potatoFire.Activate();
        }
        
        eventOneTriggered = true;
    }

    public void EventSecond()
    {
        // Ensure a different event is chosen for EventSecond
        if (eventOneTriggered)
        {
            // If lightHero was chosen for EventOne, choose potatoFire for EventSecond and vice versa
            if (ligthHero == null || potatoFire == null)
            {
                Debug.LogError("lightHero or potatoFire is not assigned!");
                return;
            }

            if (UnityEngine.Random.Range(0, 2) == 0)
            {
                if (ligthHero != null) ligthHero.Activate();
            }
            else
            {
                if (potatoFire != null) potatoFire.Activate();
            }
        }
        
        eventSecondTriggered = true;
    }
}
