using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxSpawner : MonoBehaviour
{
    public GameObject prefabToSpawn;
    public float spawnDelay = 2f;
    public int maxSpawns = 10;
    public BoxMovement conveyerBelt;
    public Transform designatedSpot;
    public ScoreUI scoreUI; // Reference to the ScoreUI script

    private int spawnCount = 0;

    private void Start()
    {
        InvokeRepeating("SpawnObject", 0f, spawnDelay);
    }

    private void SpawnObject()
    {
        if (spawnCount >= maxSpawns)
            return;

        GameObject newBox = Instantiate(prefabToSpawn, transform.position, Quaternion.identity);
        spawnCount++;

        // Set the designated spot for the box movement
        BoxMovement boxMovement = newBox.GetComponent<BoxMovement>();
        if (boxMovement != null)
            boxMovement.designatedSpot = designatedSpot;

        // Set the box as delivered and specify which player's box it is (for scorekeeping)
        bool isPlayer1Box = Random.value < 0.5f; // Randomly decide which player's box it is
        boxMovement.SetBoxAsDelivered(isPlayer1Box);
    }

    public void BoxDelivered()
    {
        // This method will be called by the MonorailController when a box is delivered
        // Increment the score for the player who delivered the box
        if (designatedSpot != null)
        {
            BoxMovement boxMovement = designatedSpot.GetComponent<BoxMovement>();
            if (boxMovement != null && boxMovement.IsDelivered())
            {
                if (boxMovement.IsPlayer1Box())
                {
                    scoreUI.IncreasePlayer1Score();
                }
                else
                {
                    scoreUI.IncreasePlayer2Score();
                }
            }
        }
    }
}