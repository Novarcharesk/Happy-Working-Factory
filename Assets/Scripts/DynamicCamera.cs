using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DynamicCamera : MonoBehaviour
{
    // Declaration of the character objects and the camera needed to find the distance
    [Header("Object Settings")]
    [SerializeField] private Camera mainCamera;
    [SerializeField] private Transform player1Transform;
    [SerializeField] private Transform player2Transform;

    // Declaration of the camera setting variables
    [Header("Camera Settings")]
    [SerializeField] private float minZoom = 5f;
    [SerializeField] private float maxZoom = 15f;
    [SerializeField] private float zoomSpeed = 2f;

    // Runs every frame
    private void Update()
    {
        // Checks to see if the characters are on the current scene
        if (player1Transform == null || player2Transform == null)
            return;

        // Calculate the distance between players
        float distance = Vector3.Distance(player1Transform.position, player2Transform.position);

        // Calculate desired zoom level based on distance
        float desiredZoom = Mathf.Lerp(minZoom, maxZoom, distance / maxZoom);

        // Smoothly interpolate current zoom to desired zoom
        mainCamera.fieldOfView = Mathf.Lerp(mainCamera.fieldOfView, desiredZoom, Time.deltaTime * zoomSpeed);
    }
}
