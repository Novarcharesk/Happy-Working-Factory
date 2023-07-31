using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonorailSpawner : MonoBehaviour
{
    public GameObject monorailPrefab;
    public float spawnInterval = 5f; // Time interval between monorail spawns

    private GameObject spawnedMonorail;
    private MonorailController monorailController;

    private void Start()
    {
        StartCoroutine(SpawnAndMoveMonorail());
    }

    private IEnumerator SpawnAndMoveMonorail()
    {
        while (true) // Continue spawning monorails indefinitely
        {
            SpawnMonorail();
            yield return new WaitForSeconds(spawnInterval);
        }
    }

    private void SpawnMonorail()
    {
        spawnedMonorail = Instantiate(monorailPrefab, transform.position, Quaternion.identity);

        // Get the MonorailController component from the spawned monorail
        monorailController = spawnedMonorail.GetComponent<MonorailController>();
        if (monorailController != null)
        {
            // Move the monorail to the center and wait for player input
            StartCoroutine(MoveToCenterAndWait());
        }
    }

    private IEnumerator MoveToCenterAndWait()
    {
        monorailController.MoveMonorail(); // Move the monorail to the center

        while (monorailController.IsWaitingForPlayer) // Wait for player input
        {
            yield return null;
        }

        // The player pressed spacebar, place the box on the monorail
        monorailController.PlaceBoxOnMonorail();

        // Move the monorail back to the right boundary
        MoveToRightBoundary();
    }

    private void MoveToRightBoundary()
    {
        monorailController.MoveMonorail(); // Move the monorail to the right boundary

        // Optional: You can add a brief pause here if needed before destroying the monorail.
        // For example, WaitForSeconds(2f) would pause for 2 seconds.

        Destroy(spawnedMonorail); // Destroy the monorail game object
    }
}