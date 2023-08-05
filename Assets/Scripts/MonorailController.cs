using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonorailController : MonoBehaviour
{
    [SerializeField] private GameObject boxPrefab;

    private GameObject leftBoundary;
    private GameObject rightBoundary;
    private GameObject midPosition;

    private bool cargoLoaded = false;

    [SerializeField] private Collider boxStorageCollider;
    private List<GameObject> cargoList;

    void Start()
    {
        leftBoundary = FindObjectOfType<MonorailSpawner>().leftMonorailBoundary;
        rightBoundary = FindObjectOfType<MonorailSpawner>().rightMonorailBoundary;
        midPosition = FindObjectOfType<MonorailSpawner>().midMonorailPosition;

        StartCoroutine(MoveMonorailToMid());
    }

    public void PlaceBoxOnMonorail(string lastTouchedBy)
    {
        // Implement the logic to place the box on the monorail.
        // For example, instantiate the box prefab and position it on the monorail.
        // You may want to adjust the height and position of the box based on your monorail's design.
        // You can also attach the box to the monorail using parenting or other methods.
        
        GameObject newBox = Instantiate(boxPrefab, boxStorageCollider.transform.position, Quaternion.identity);
        cargoList.Add(newBox);
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
                StopCoroutine(MoveMonorailToMid());
                StartCoroutine(MoveMonorailToEnd());
            }
            yield return null;
        }
    }

    private IEnumerator MoveMonorailToEnd()
    {
        while (true)
        {
            if (transform.position.x >= 0 && !cargoLoaded) 
            {
                transform.Translate(+0.01f, 0, 0);
            }
            else if (transform.position.x >= rightBoundary.transform.position.x)
            {
                StopCoroutine(MoveMonorailToEnd());
                Destroy(gameObject);
            }
            yield return new WaitForSeconds(5);
        }
    }
}