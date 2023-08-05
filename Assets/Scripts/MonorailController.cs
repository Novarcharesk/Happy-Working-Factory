using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonorailController : MonoBehaviour
{
    // Declarartion of the box prefab
    [SerializeField] private GameObject boxPrefab;

    //Declaration of the boundrys needed for the monorail
    private GameObject leftBoundary;
    private GameObject rightBoundary;
    private GameObject midPosition;

    // Declaration of the bool that control if cargo is loaded on the train
    public static bool cargoLoaded = false;
    private bool waitingToLeave = false;

    [SerializeField] private Collider boxStorageCollider;
    [SerializeField] private List<string> cargoList;
    private int cargoListIndex;

    Coroutine runningRoutine = null;

    // Runs at satrt
    void Start()
    {
        // sets the boundries
        leftBoundary = FindObjectOfType<MonorailSpawner>().leftMonorailBoundary;
        rightBoundary = FindObjectOfType<MonorailSpawner>().rightMonorailBoundary;
        midPosition = FindObjectOfType<MonorailSpawner>().midMonorailPosition;

        //Starts the monorail movement routine
        runningRoutine = StartCoroutine(MoveMonorailToMid());
    }

    // Ruins every frame
    private void Update()
    {
        // Checks to see if there is cargo loaded on the monorail
        if (cargoLoaded)
        {
            waitingToLeave = true;
            runningRoutine = StartCoroutine("MoveMonorailToEnd");
        }
    }

    // Method that places a box on the monorail
    public void PlaceBoxOnMonorail(string lastTouchedBy)
    {   
        GameObject newBox = Instantiate(boxPrefab, boxStorageCollider.transform.position, Quaternion.identity, this.boxStorageCollider.transform);
        if (lastTouchedBy == "Player1")
        {
            newBox.GetComponent<MeshRenderer>().material.color = Color.blue;
        }
        
        if (lastTouchedBy == "Player2")
        {
            newBox.GetComponent<MeshRenderer>().material.color = Color.red;
        }

        newBox.GetComponent<BoxCollider>().enabled = false;

        Debug.Log(lastTouchedBy);
        cargoList.Add(lastTouchedBy);
        cargoListIndex++;
    }

    // Routine that moves the monorail to the mid point
    private IEnumerator MoveMonorailToMid()
    {
        while (true)
        {
            if (transform.position.x <= -0.5)
            {
                transform.Translate(+0.01f, 0, 0);
            }
            else
            {
                StopAllCoroutines();
            }
            yield return null;
        }
    }

    // Routine that moves the monorail from the mid point to the end
    private IEnumerator MoveMonorailToEnd()
    {
        while (true)
        {
            if (transform.position.x <= 26f)
            {
                transform.Translate(+0.00005f, 0, 0);
            }
            else
            {
                StopAllCoroutines();
                MonorailSpawner.monorailPresent = false;
                cargoLoaded = false;

                UpdateScore();

                Destroy(gameObject);
            }
            yield return null;
        }

    }

    // Method when updating the score
    private void UpdateScore()
    {
        Debug.Log("score updated");
    }
}
