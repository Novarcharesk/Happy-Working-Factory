using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonorailSpawner : MonoBehaviour
{
    public GameObject monorailPrefab;
    public float spawnInterval = 5f; // Time interval between monorail spawns

    private GameObject spawnedMonorail;
    [SerializeField] public GameObject leftMonorailBoundary;
    [SerializeField] public GameObject rightMonorailBoundary;
    [SerializeField] public GameObject midMonorailPosition;

    private void Start()
    {
        SpawnMonorail();
    }

    private void SpawnMonorail()
    {
        spawnedMonorail = Instantiate(monorailPrefab, new Vector3(leftMonorailBoundary.transform.position.x -10, 0, -11.5f), Quaternion.identity);
    }
}