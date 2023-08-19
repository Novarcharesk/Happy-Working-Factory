using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CharacterKick : MonoBehaviour
{
    private KeyCode kickKey;
    private float kickForce;
    private Gamepad playerGamepad;

    private Animation kickAnimation;
    [SerializeField] private AudioSource sfxSource;
    private int playerInputIndex;

    private void Start()
    {
        kickKey = this.GetComponentInParent<PlayerController>().kickKey;
        kickForce = this.GetComponentInParent<PlayerController>().kickForce;
        playerGamepad = this.GetComponentInParent<PlayerController>().playerGamepad;

        kickAnimation = this.gameObject.GetComponentInParent<Animation>();

        if (gameObject.CompareTag("Player1"))
        {
            playerInputIndex = 0;
        }
        if (gameObject.CompareTag("Player2"))
        {
            playerInputIndex = 1;
        }
    }

    private void Update()
    {
        if (Input.GetKey(kickKey))
        {
            kickAnimation.Play("Kick");
            sfxSource.Play();
        }

        if (playerGamepad != null && playerGamepad.rightTrigger.wasPressedThisFrame)
        {
            kickAnimation.Play("Kick");
            sfxSource.Play();
        }

        playerGamepad = Gamepad.all[playerInputIndex];
        if (playerGamepad == null)
            return;
    }

    private void OnTriggerStay(Collider other)
    {
        Rigidbody rb = other.GetComponent<Rigidbody>();

        if (other.CompareTag("Player1") || other.CompareTag("Player2") || other.CompareTag("Box"))
        {
            if (Input.GetKey(kickKey))
            {
                if (other.CompareTag("Box"))
                {
                    ChangeBoxColour(other.gameObject);
                }

                Vector3 direction = transform.forward;
                rb.AddForce(direction * kickForce, ForceMode.Impulse);
                kickAnimation.Play("Kick");
                sfxSource.Play();
            }

            if (playerGamepad != null && playerGamepad.rightTrigger.wasPressedThisFrame)
            {
                if (other.CompareTag("Box"))
                {
                    ChangeBoxColour(other.gameObject);
                }

                Vector3 direction = transform.forward;
                rb.AddForce(direction * kickForce, ForceMode.Impulse);
                kickAnimation.Play("Kick");
                sfxSource.Play();
            }
        }
    }

    private void ChangeBoxColour(GameObject box)
    {
        if (this.gameObject.CompareTag("Player1"))
        {
            box.GetComponent<BoxHandler>().lastTouch = "Player1";
        }
        if (this.gameObject.CompareTag("Player2"))
        {
            box.GetComponent<BoxHandler>().lastTouch = "Player2";
        }
    }
}
