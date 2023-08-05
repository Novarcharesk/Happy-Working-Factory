using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonorailController : MonoBehaviour
{
    [SerializeField] private GameObject boxPrefab;
    private GameObject leftBoundary;
    private GameObject rightBoundary;
    private GameObject midPosition;

    public static bool cargoLoaded = false;
    private bool waitingToLeave = false;

    [SerializeField] private Collider boxStorageCollider;
    [SerializeField] private List<string> cargoList;
    private int cargoListIndex;

    Coroutine runningRoutine = null;

    void Start()
    {
        leftBoundary = FindObjectOfType<MonorailSpawner>().leftMonorailBoundary;
        rightBoundary = FindObjectOfType<MonorailSpawner>().rightMonorailBoundary;
        midPosition = FindObjectOfType<MonorailSpawner>().midMonorailPosition;

        runningRoutine = StartCoroutine(MoveMonorailToMid());
    }

    private void Update()
    {
        if (cargoLoaded)
        {
            waitingToLeave = true;
            runningRoutine = StartCoroutine("MoveMonorailToEnd");
        }
    }

    public void PlaceBoxOnMonorail(string lastTouchedBy)
    {
        GameObject newBox = Instantiate(boxPrefab, boxStorageCollider.transform.position, Quaternion.identity, this.boxStorageCollider.transform);
        
        if (lastTouchedBy == "Player1")
        {
            newBox.GetComponent<MeshRenderer>().material.color = Color.blue;
        }
        else if (lastTouchedBy == "Player2")
        {
            newBox.GetComponent<MeshRenderer>().material.color = Color.red;
        }

        newBox.GetComponent<BoxCollider>().enabled = false;

        cargoList.Add(lastTouchedBy);
        cargoListIndex++;
    }

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
            }
        }
    }
}