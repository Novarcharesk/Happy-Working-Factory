using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxPusherHazard : MonoBehaviour
{
    [SerializeField] private float pushForce = 10f;

    [SerializeField] private Animation pistonActivated;

    [SerializeField] private AudioSource sfxSource;

    private void OnTriggerEnter(Collider other)
    {
        Rigidbody rb = other.GetComponent<Rigidbody>();

        if (rb != null && other.CompareTag("Box"))
        {
            Vector3 direction = transform.up;
            rb.AddForce(direction * pushForce, ForceMode.Impulse);
            pistonActivated.Play("BoxPusherHazardActivated");
            sfxSource.Play();
        }
    }
}