using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CharacterKick : MonoBehaviour
{
    private KeyCode kickKey;
    private float kickForce;

    private Animation kickAnimation;
    [SerializeField] private AudioSource sfxSource;
    public bool rightTriggerPressed;

    private void Start()
    {
        kickKey = this.GetComponentInParent<PlayerController>().kickKey;
        kickForce = this.GetComponentInParent<PlayerController>().kickForce;

        kickAnimation = this.gameObject.GetComponentInParent<Animation>();
    }

    private void Update()
    {
        kickForce = this.GetComponentInParent<PlayerController>().kickForce;

        if (Input.GetKey(kickKey))
        {
            kickAnimation.Play("Kick");
            sfxSource.Play();
        }

        if (rightTriggerPressed == true)
        {
            kickAnimation.Play("Kick");
            sfxSource.Play();
        }
    }

    private void OnTriggerStay(Collider other)
    { 
        if (other.CompareTag("Player1") || other.CompareTag("Player2") || other.CompareTag("Box"))
        {
            Rigidbody rb = other.GetComponent<Rigidbody>();

            if (Input.GetKey(kickKey))
            {
                Vector3 direction = transform.forward;
                rb.AddForce(direction * kickForce, ForceMode.Impulse);
                kickAnimation.Play("Kick");
                sfxSource.Play();

                ChangeBoxColour(other.gameObject);
            }

            if (rightTriggerPressed == true)
            {
                Debug.Log("Gamepad Kick");
                Vector3 direction = transform.forward;
                rb.AddForce(direction * kickForce, ForceMode.Impulse);
                kickAnimation.Play("Kick");
                sfxSource.Play();

                ChangeBoxColour(other.gameObject);
            }
        }
    }

    private void ChangeBoxColour(GameObject box)
    {
        if (this.gameObject.CompareTag("Player1"))
        {
            box.GetComponent<BoxHandler>().lastTouch = "Player1";
            //box.gameObject.GetComponent<MeshRenderer>().material.color = Color.blue;
        }
        if (this.gameObject.CompareTag("Player2"))
        {
            box.GetComponent<BoxHandler>().lastTouch = "Player2";
            //box.gameObject.GetComponent<MeshRenderer>().material.color = Color.red;
        }
    }

}
