using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxHandler : MonoBehaviour
{
    private MonorailController monorailController;

    private string lastTouch;

    void Update()
    {
        if (lastTouch == "Player1")
        {
            this.gameObject.GetComponent<MeshRenderer>().material.color = Color.blue;
        }
        
        if (lastTouch == "Player2")
        {
            this.gameObject.GetComponent<MeshRenderer>().material.color = Color.red;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Player1"))
        {
            lastTouch = "Player1";
            Debug.Log("Touched by player1");
        }
        
        if (collision.collider.CompareTag("Player2"))
        {
            lastTouch = "Player2";
            Debug.Log("Touched by player2");
        }
        
        if (collision.collider.CompareTag("Monorail"))
        {
            Debug.Log("coliding with monorail");
            OnMonorailCollision(this.gameObject);
        }
    }

    private void OnMonorailCollision(GameObject monorail)
    { 

        monorailController.PlaceBoxOnMonorail(lastTouch);
        Destroy(this.gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Monorail"))
        {
            OnMonorailCollision(other.gameObject);
        }
    }
}
