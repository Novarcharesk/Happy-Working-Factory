using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonorailSpawner : MonoBehaviour
{
    public GameObject monorailPrefab;

    // bool that tells wether there is a monorail already present
    public static bool monorailPresent;

    // Declaration of the variables needed to movce the monorail
    private GameObject spawnedMonorail;
    [SerializeField] public GameObject leftMonorailBoundary;
    [SerializeField] public GameObject rightMonorailBoundary;
    [SerializeField] public GameObject midMonorailPosition;

    // Plays at start
    private void Start()
    {
        SpawnMonorail();
    }

    private void Update()
    {
        // CHecks to see if there is already a monorail
        if (monorailPresent == false)
        {
            // If there isnt one then spawn a new one
            SpawnMonorail();
        }
    }

    // Method that spawns a new monorail
    private void SpawnMonorail()
    {
        monorailPresent = true;
        spawnedMonorail = Instantiate(monorailPrefab, new Vector3(leftMonorailBoundary.transform.position.x -10, 0, -11.5f), Quaternion.identity);
    }
}