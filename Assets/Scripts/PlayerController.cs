using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Speed")]
    [SerializeField] private float movementSpeed = 10f;

    [Header("Controls")]
    [SerializeField] private KeyCode forwardKey;
    [SerializeField] private KeyCode rightKey;
    [SerializeField] private KeyCode leftKey;
    [SerializeField] private KeyCode backKey;
    [SerializeField] private KeyCode pickupKey; // New key to pick up and drop the box

    [Header("Bobbing")]
    [SerializeField] private float bobbingSpeed = 2f;
    [SerializeField] private float bobbingHeight = 0.1f;
    private Vector3 originalPosition;
    private bool isMoving = false;
    private GameObject heldBox; // Reference to the currently held box, if any

    private void Start()
    {
        originalPosition = transform.localPosition;
    }

    private void Update()
    {
        // Input handling for movement
        Vector3 movementDirection = Vector3.zero;

        if (Input.GetKey(forwardKey) || Input.GetKey(backKey) || Input.GetKey(leftKey) || Input.GetKey(rightKey))
        {
            isMoving = true;
            // Handling the movement input
            if (Input.GetKey(forwardKey))
            {
                movementDirection += Vector3.forward;
            }
            if (Input.GetKey(backKey))
            {
                movementDirection += Vector3.back;
            }
            if (Input.GetKey(leftKey))
            {
                movementDirection += Vector3.left;
            }
            if (Input.GetKey(rightKey))
            {
                movementDirection += Vector3.right;
            }

            // Normalize the movement direction to ensure consistent speed in all directions
            movementDirection.Normalize();
            transform.position += movementDirection * Time.deltaTime * movementSpeed;
        }
        else
        {
            isMoving = false;
        }

        // Bobbing motion only when moving
        if (isMoving)
        {
            float newY = originalPosition.y + Mathf.Sin(Time.time * bobbingSpeed) * bobbingHeight;
            transform.localPosition = new Vector3(transform.localPosition.x, newY, transform.localPosition.z);
        }

        // Handle box pickup
        if (Input.GetKeyDown(pickupKey))
        {
            if (heldBox == null)
            {
                RaycastHit hit;
                if (Physics.Raycast(transform.position, transform.forward, out hit, 3f))
                {
                    if (hit.collider.CompareTag("Box"))
                    {
                        Debug.Log("Box hit by raycast!");
                        heldBox = hit.collider.gameObject;
                        heldBox.GetComponent<Rigidbody>().isKinematic = true;
                        heldBox.transform.SetParent(transform);
                        heldBox.transform.localPosition = Vector3.forward * 1.5f;
                    }
                }
            }
            else
            {
                heldBox.GetComponent<Rigidbody>().isKinematic = false;
                heldBox.transform.SetParent(null);
                heldBox = null;
            }
            Debug.Log("Box picked up or dropped!");
        }
    }
}