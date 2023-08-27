using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shutter : MonoBehaviour
{
    public Animator animator; // Reference to the Animator component
    public float openDuration = 20f; // Duration for which the shutter remains open in seconds
    public float cycleDelay = 120f; // Delay between cycles in seconds
    public float initialDelay = 120f; // Initial delay before the first cycle in seconds

    private bool isOpen = false;
    private float timer = 0f;

    private void Start()
    {
        // Initialize the timer to the initial delay
        timer = initialDelay;

        // Start the coroutine for the shutter cycle
        StartCoroutine(ShutterCycle());
    }

    private IEnumerator ShutterCycle()
    {
        // Wait for the initial delay before starting the first cycle
        yield return new WaitForSeconds(initialDelay);

        while (true)
        {
            // Open the shutter
            OpenShutter();
            isOpen = true;

            // Wait for the open duration
            yield return new WaitForSeconds(openDuration);

            // Close the shutter
            CloseShutter();
            isOpen = false;

            // Wait for the cycle delay
            yield return new WaitForSeconds(cycleDelay - openDuration);
        }
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

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Box") && isOpen == true)
        {
            Destroy(collision.gameObject);

            ScoreUI scoreUI = FindObjectOfType<ScoreUI>();

            if (scoreUI != null)
            {
                string lastTouchedBy = collision.gameObject.GetComponent<BoxHandler>().lastTouch;
                if (lastTouchedBy == "Player1")
                {
                    scoreUI.IncreasePlayer1Score();
                    scoreUI.IncreasePlayer1Score();
                }
                else if (lastTouchedBy == "Player2")
                {
                    scoreUI.IncreasePlayer2Score();
                    scoreUI.IncreasePlayer2Score();
                }
            }
        }
    }
}