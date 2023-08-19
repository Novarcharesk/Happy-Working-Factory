using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TimerHandler : MonoBehaviour
{
    // Declaration of the variables needed to make timer function
    [SerializeField] private float maxTime;
    private float timeRemaining;
    [SerializeField] private TMP_Text timerDisplay;

    // Plays at start
    private void Start()
    {
        // Sets the time reamining to the max time
        timeRemaining = LobySettings.matchTimeInSeconds;

        // Calls the update timer display method
        UpdateTimerDisplay();
    }

    // plays every frame
    private void Update()
    {
        // makes sure the timer is above zero
        if (timeRemaining > 0)
        {
            // reduced the time remaining by time.deltatime
            timeRemaining -= Time.deltaTime;

            // Calls the update timer display method
            UpdateTimerDisplay();
        }
        else
        {
            SceneManager.LoadScene(1);
        }
    }

    // Method that updates the timer display
    private void UpdateTimerDisplay()
    {
        // Creates variables to display time in minutes and seconds
        float minutes = Mathf.FloorToInt(timeRemaining / 60);
        float seconds = Mathf.FloorToInt(timeRemaining % 60);

        // Updates the UI text element to display the time
        timerDisplay.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }
}
