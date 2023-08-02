using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxMovement : MonoBehaviour
{
    public float conveyerSpeed = 2f;
    public Transform designatedSpot;

    private bool isMoving = false;
    private bool isDelivered = false; // Flag to check if the box is delivered
    private bool isPlayer1Box = false; // Flag to check if the box belongs to Player 1

    private void Update()
    {
        // Move the box along the conveyer belt towards the designated spot
        if (isMoving && designatedSpot != null)
        {
            Vector3 targetPosition = designatedSpot.position;
            Vector3 movementDirection = (targetPosition - transform.position).normalized;
            transform.position += movementDirection * conveyerSpeed * Time.deltaTime;

            // Check if the box is close enough to the designated spot and stop its movement
            float distanceToTarget = Vector3.Distance(transform.position, targetPosition);
            if (distanceToTarget < 0.1f)
            {
                isMoving = false;
                Rigidbody rb = GetComponent<Rigidbody>();
                rb.velocity = Vector3.zero;
                rb.isKinematic = true;
                transform.position = targetPosition;

                // Call the BoxDelivered method in the BoxSpawner script to handle the score increment
                BoxSpawner boxSpawner = designatedSpot.GetComponent<BoxSpawner>();
                if (boxSpawner != null)
                {
                    boxSpawner.BoxDelivered();
                }
            }
        }
    }

    public void StartMoving()
    {
        isMoving = true;
        Rigidbody rb = GetComponent<Rigidbody>();
        rb.isKinematic = false;
        rb.velocity = Vector3.zero;
    }

    public bool IsDelivered()
    {
        return isDelivered;
    }

    public bool IsPlayer1Box()
    {
        return isPlayer1Box;
    }

    public void SetBoxAsDelivered(bool player1Box)
    {
        isDelivered = true;
        isPlayer1Box = player1Box;
    }

    private void OnTriggerStay(Collider other)
    {
        // Move the box along the conveyer belt towards the designated spot
        if (other.CompareTag("ConveyerBelt"))
        {
            Rigidbody rb = GetComponent<Rigidbody>();
            Vector3 movement = (other.transform.right * -1f).normalized * conveyerSpeed;
            rb.velocity = movement;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // Stop the box when it exits the conveyer belt area
        if (other.CompareTag("ConveyerBelt"))
        {
            Rigidbody rb = GetComponent<Rigidbody>();
            rb.velocity = Vector3.zero;
            rb.isKinematic = true;

            // Drop the box at its current position
            transform.SetParent(null);
            designatedSpot = null;
        }
    }
}