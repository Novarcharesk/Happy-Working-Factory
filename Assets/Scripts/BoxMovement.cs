using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxMovement : MonoBehaviour
{
    public float conveyerSpeed = 2f;
    public Transform designatedSpot;

    private bool isMoving = false;

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

    private void OnTriggerStay(Collider other)
    {
        // Move the box along the conveyer belt towards the designated spot
        if (other.CompareTag("Box"))
        {
            Rigidbody rb = other.GetComponent<Rigidbody>();
            Vector3 movement = (transform.position - other.transform.position).normalized;
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