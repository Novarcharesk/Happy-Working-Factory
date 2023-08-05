using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BoxHandler : MonoBehaviour
{
    private MonorailController monorailController;

    // String variable needed to tell who last touched the box
    private string lastTouch;

    private void Start()
    {
        monorailController = FindObjectOfType<MonorailController>().GetComponent<MonorailController>();
    }

    // Runs every frame
    void Update()
    {
        // Checks to see who touched the box last
        if (lastTouch == "Player1")
        {
            // Updates the colour of the box to m,atch the player colour
            this.gameObject.GetComponent<MeshRenderer>().material.color = Color.blue;
        }

        // Checks to see who touched the box last
        if (lastTouch == "Player2")
        {
            // Updates the colour of the box to m,atch the player colour
            this.gameObject.GetComponent<MeshRenderer>().material.color = Color.red;
        }
    }

    // Checks to see if there has been a collision
    private void OnCollisionEnter(Collision collision)
    {
        // Checks to see if it has collided with a player and then updates the string variable
        if (collision.collider.CompareTag("Player1"))
        {
            lastTouch = "Player1";
        }
        
        if (collision.collider.CompareTag("Player2"))
        {
            lastTouch = "Player2";
        }
    }

    private void OnMonorailCollision(GameObject box)
    {
        MonorailController.cargoLoaded = true;
        monorailController.PlaceBoxOnMonorail(lastTouch);
        Object.Destroy(box);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Monorail"))
        {
            OnMonorailCollision(gameObject);
        }
    }
}
