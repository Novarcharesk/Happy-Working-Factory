using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CharacterKick : MonoBehaviour
{
    private KeyCode kickKey;
    private float kickForce;
    private bool canKick;
    private float kickCooldown;
    private Gamepad playerGamepad;

    private void Start()
    {
        kickKey = this.GetComponentInParent<PlayerController>().kickKey;
        kickForce = this.GetComponentInParent<PlayerController>().kickForce;
        playerGamepad = this.GetComponentInParent<PlayerController>().playerGamepad;
    }

    private void Update()
    {
        kickCooldown += Time.deltaTime;

        if (kickCooldown >= 5f)
        {
            canKick = true;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        Rigidbody rb = other.GetComponent<Rigidbody>();

        if (rb != null && (Input.GetKey(kickKey) || playerGamepad.rightTrigger.isPressed) && canKick == true)
        {
            Vector3 direction = transform.forward;
            rb.AddForce(direction * kickForce, ForceMode.Impulse);
            canKick = false;
            kickCooldown = 0;
        }
    }
}
