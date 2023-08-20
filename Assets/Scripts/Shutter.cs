using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shutter : MonoBehaviour
{
    public Animator animator; // Reference to the Animator component
    public float openDuration = 20f; // Duration for which the shutter remains open in seconds
    public float repeatDelay = 120f; // Delay between subsequent openings in seconds

    private bool isOpen = false;
    private float timer = 0f;
    private bool gameStarted = false;

    private void Start()
    {
        // Initialize the timer to the open duration
        timer = openDuration;
    }

    private void Update()
    {
        // Only start the timer and update behavior if the game has started
        if (!gameStarted)
        {
            return;
        }

        timer -= Time.deltaTime;

        if (timer <= 0f)
        {
            if (!isOpen)
            {
                OpenShutter();
            }
            else
            {
                CloseShutter();
            }

            // Reset the timer based on whether the shutter is currently open or closed
            timer = isOpen ? repeatDelay : openDuration;
        }
    }

    public void StartGame()
    {
        // Call this method to start the game and initiate the shutter behavior
        gameStarted = true;
    }

    private void OpenShutter()
    {
        // Play the opening animation by setting the "RollerDoorOpen" parameter in the Animator
        animator.SetBool("RollerDoorOpen", true);

        isOpen = true;
    }

    private void CloseShutter()
    {
        // Play the closing animation by setting the "RollerDoorOpen" parameter to false in the Animator
        animator.SetBool("RollerDoorOpen", false);

        isOpen = false;
    }
}