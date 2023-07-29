using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSpawner : MonoBehaviour
{
    public GameObject prefabToSpawn;
    public float spawnDelay = 2f;
    public int maxSpawns = 10;
    public GameObject conveyerBelt;
    public Transform designatedSpot;

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

public class ConveyerBelt : MonoBehaviour
{
    public float conveyerSpeed = 2f;

    private void OnTriggerStay(Collider other)
    {
        // Move the box along the conveyer belt towards the designated spot
        if (other.CompareTag("Box"))
        {
            Rigidbody rb = other.GetComponent<Rigidbody>();
            Vector3 movement = (transform.parent.position - other.transform.position).normalized;
            rb.velocity = movement * conveyerSpeed;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // Stop the box when it exits the conveyer belt area
        if (other.CompareTag("Box"))
        {
            Rigidbody rb = other.GetComponent<Rigidbody>();
            rb.velocity = Vector3.zero;
        }
    }
}