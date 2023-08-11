using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterKick : MonoBehaviour
{
    private KeyCode kickKey;
    private float kickForce;
    private bool canKick;
    private float kickCooldown;

    private void Start()
    {
        kickKey = this.GetComponentInParent<PlayerController>().kickKey;
        kickForce = this.GetComponentInParent<PlayerController>().kickForce;
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

        if (rb != null && Input.GetKey(kickKey) && canKick == true)
        {
            Vector3 direction = transform.forward;
            rb.AddForce(direction * kickForce, ForceMode.Impulse);
            canKick = false;
            kickCooldown = 0;
        }
    }
}
