using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxPusher : MonoBehaviour
{
    [SerializeField] private GameObject pushStick;

    private bool atResetPosition = true;

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Player is here");
        if (atResetPosition == true)
        {
            if (other.CompareTag("Player1") || other.CompareTag("Player2") || other.CompareTag("Box"))
            {
                pushStick.GetComponent<Rigidbody>().velocity = new Vector3(0, 20, 0);
                atResetPosition = false;
            }
        }
        else if (atResetPosition == false)
        {
            pushStick.GetComponent<Rigidbody>().velocity = Vector3.zero;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // Reset atResetPosition to true when the player or box exits the trigger area.
        atResetPosition = true;
    }
}