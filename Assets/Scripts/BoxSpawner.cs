using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxSpawner : MonoBehaviour
{
    public GameObject prefabToSpawn;
    public float spawnDelay = 2f;
    public int maxSpawns = 10;
    public GameObject conveyerBelt;

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

        // Attach the box to the conveyer belt as a child
        newBox.transform.SetParent(conveyerBelt.transform, true);
    }
}