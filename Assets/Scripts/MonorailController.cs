using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonorailController : MonoBehaviour
{
    // Declaration of the things needed to summon the box and move the monorail
    [SerializeField] private GameObject boxPrefab;
    private GameObject leftBoundary;
    private GameObject rightBoundary;
    private GameObject midPosition;

    // Decleration of the variables to check if there is something on the train
    public static bool cargoLoaded = false;
    private bool waitingToLeave = false;

    [SerializeField] private Collider boxStorageCollider;
    [SerializeField] private List<string> cargoList;
    private int cargoListIndex;
    private GameObject monorailNewBox;

    Coroutine runningRoutine = null;

    // Plays at start
    void Start()
    {   
        // Finds the corresponding variables from the monorail spawner script and set them
        leftBoundary = FindObjectOfType<MonorailSpawner>().leftMonorailBoundary;
        rightBoundary = FindObjectOfType<MonorailSpawner>().rightMonorailBoundary;
        midPosition = FindObjectOfType<MonorailSpawner>().midMonorailPosition;

        runningRoutine = StartCoroutine(MoveMonorailToMid());
    }

    // Plays everyframe
    private void Update()
    {
        // Checks to see if there is cargo loaded
        if (cargoLoaded)
        {
            waitingToLeave = true;
            runningRoutine = StartCoroutine("MoveMonorailToEnd");
        }
    }

    // Method that places a box on the train when called
    public void PlaceBoxOnMonorail(string lastTouchedBy)
    {
        Debug.Log("placing box on monorail");
        
        // Spawns the box on the train
        GameObject monorailNewBox =  Instantiate(boxPrefab, boxStorageCollider.transform.position, Quaternion.identity, this.boxStorageCollider.transform);
        
        // Checking who touched the box last and changing the boxs colour
        if (lastTouchedBy == "Player1")
        {
            monorailNewBox.GetComponent<MeshRenderer>().material.color = Color.blue;
        }
        else if (lastTouchedBy == "Player2")
        {
            monorailNewBox.GetComponent<MeshRenderer>().material.color = Color.red;
        }

        monorailNewBox.GetComponent<BoxCollider>().enabled = false;

        // Adds the spawned object to the list to track for scoring
        cargoList.Add(lastTouchedBy);
        cargoListIndex++;
    }

    // Routine that moves the monorail to the midpoint
    private IEnumerator MoveMonorailToMid()
    {
        while (true)
        {
            if (transform.position.x <= -0.5)
            {
                transform.Translate(+0.025f, 0, 0);
            }
            else
            {
                StopAllCoroutines();
            }
            yield return null;
        }
    }

    // Routine that moves the monorail to the end from the midpoint
    private IEnumerator MoveMonorailToEnd()
    {
        while (true)
        {
            if (transform.position.x <= 26f)
            {
                transform.Translate(+0.000075f, 0, 0);
            }
            else
            {
                MonorailSpawner.monorailPresent = false;
                cargoLoaded = false;

                UpdateScore();

                StopAllCoroutines();
                Destroy(monorailNewBox);
                Destroy(gameObject);
                Destroy(this);
            }
            yield return null;
        }
    }

    // Method that updates the score in the UI
    private void UpdateScore()
    {
        ScoreUI scoreUI = FindObjectOfType<ScoreUI>();
        if (scoreUI != null)
        {
            if (cargoList.Count > 0)
            {
                string lastTouchedBy = cargoList[cargoList.Count - 1];
                if (lastTouchedBy == "Player1")
                {
                    scoreUI.IncreasePlayer1Score();
                }
                else if (lastTouchedBy == "Player2")
                {
                    scoreUI.IncreasePlayer2Score();
                }
                
                cargoList.Clear();;
            }
        }
    }
}