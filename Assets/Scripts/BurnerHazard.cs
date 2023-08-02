using System.Collections;
using UnityEngine;

public class BurnerHazard : MonoBehaviour
{
    public float burnDuration = 2f; // Duration in seconds for which the box is burned
    public int damageAmount = 1; // Amount of damage the burner inflicts on the box (optional)

    private void OnTriggerEnter(Collider other)
    {
        // Check if the collided object is the box (you may need to adjust the tag name based on your box setup)
        if (other.CompareTag("Box"))
        {
            // Start burning the box
            BurnBox(other.gameObject);
        }
    }

    private void BurnBox(GameObject box)
    {
        // Disable the box's collider and renderer while it's burning
        box.GetComponent<Collider>().enabled = false;
        box.GetComponent<Renderer>().enabled = false;

        // Wait for a specified duration and then stop burning
        StartCoroutine(StopBurning(box));
    }

    // Make the StopBurning method public
    public IEnumerator StopBurning(GameObject box)
    {
        yield return new WaitForSeconds(burnDuration);

        // Re-enable the box's collider and renderer after burning is done
        box.GetComponent<Collider>().enabled = true;
        box.GetComponent<Renderer>().enabled = true;

        // Optionally, you can inflict damage to the box upon burning
        // (e.g., you can have a health script on the box to handle damage and destruction)
        BoxHealth boxHealth = box.GetComponent<BoxHealth>(); // Changed from HealthComponent to BoxHealth
        if (boxHealth != null)
        {
            boxHealth.TakeDamage(damageAmount);
        }
    }
}