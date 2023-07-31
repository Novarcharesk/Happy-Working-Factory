using System.Collections;
using UnityEngine;

public class MonorailController : MonoBehaviour
{
    private bool isWaitingForPlayer = false;
    private Vector3 initialPosition;
    public Transform leftBoundary; // Reference to the left boundary GameObject
    public Transform rightBoundary; // Reference to the right boundary GameObject

    void Start()
    {
        // Find the left and right boundary objects under the MonorailSpawner
        leftBoundary = transform.parent.Find("LeftBoundary");
        rightBoundary = transform.parent.Find("RightBoundary");

        initialPosition = transform.position;
        MoveMonorail();
    }

    public void MoveMonorail()
    {
        StartCoroutine(MoveRoutine());
    }

    public bool IsWaitingForPlayer
    {
        get { return isWaitingForPlayer; }
    }

    public void PlaceBoxOnMonorail()
    {
        // Implement the logic to place the box on the monorail.
        // For example, instantiate the box prefab and position it on the monorail.
        // You may want to adjust the height and position of the box based on your monorail's design.
        // You can also attach the box to the monorail using parenting or other methods.
    }

    private IEnumerator MoveRoutine()
    {
        // Move monorail to the center position between left and right boundaries
        Vector3 targetPosition = (leftBoundary.position + rightBoundary.position) / 2f;
        float journeyLength = Vector3.Distance(initialPosition, targetPosition);
        float startTime = Time.time;
        float distanceCovered = 0f;

        while (distanceCovered < journeyLength)
        {
            float fractionOfJourney = distanceCovered / journeyLength;
            transform.position = Vector3.Lerp(initialPosition, targetPosition, fractionOfJourney);
            distanceCovered = Vector3.Distance(initialPosition, transform.position);
            yield return null;
        }

        // Monorail has reached the center position, wait for player input
        isWaitingForPlayer = true;

        // Keep checking for player input until spacebar is pressed
        while (isWaitingForPlayer)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                // Player pressed spacebar, start moving again
                isWaitingForPlayer = false;
                StartCoroutine(MoveBackRoutine());
            }
            yield return null;
        }
    }

    private IEnumerator MoveBackRoutine()
    {
        // Move monorail back to its initial position
        float journeyLength = Vector3.Distance(initialPosition, transform.position);
        float startTime = Time.time;
        float distanceCovered = 0f;

        while (distanceCovered < journeyLength)
        {
            float fractionOfJourney = distanceCovered / journeyLength;
            transform.position = Vector3.Lerp(transform.position, initialPosition, fractionOfJourney);
            distanceCovered = Vector3.Distance(initialPosition, transform.position);
            yield return null;
        }

        // Monorail has reached its initial position, start moving to the right boundary again
        MoveMonorail();
    }
}