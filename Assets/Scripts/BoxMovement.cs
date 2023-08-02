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

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("BurnerHazard"))
        {
            // Call the OnBurnerCollision method when the box collides with the burner hazard
            OnBurnerCollision(other.gameObject);
        }
    }

    public void OnBurnerCollision(GameObject burner)
    {
        // Disable the box's collider and renderer while it's burning
        GetComponent<Collider>().enabled = false;
        GetComponent<Renderer>().enabled = false;

        // Start the burning process using the BurnerHazard script (if available)
        BurnerHazard burnerHazard = burner.GetComponent<BurnerHazard>();
        if (burnerHazard != null)
        {
            StartCoroutine(burnerHazard.StopBurning(gameObject));
        }
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
}