using UnityEngine;

public class ConveyerBelt : MonoBehaviour
{
    public float conveyerSpeed = 2f;
    public Transform designatedSpot;

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Box"))
        {
            Rigidbody rb = other.GetComponent<Rigidbody>();

            // Calculate the velocity of the conveyor belt's surface in world space
            Vector3 conveyerVelocity = transform.TransformVector(Vector3.left) * conveyerSpeed;

            // Move the box along with the conveyor belt's surface velocity
            rb.velocity = conveyerVelocity;
            other.GetComponent<AudioSource>().enabled = false;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Box"))
        {
            Rigidbody rb = other.GetComponent<Rigidbody>();
            rb.velocity = Vector3.zero;
            rb.useGravity = true;

            // Detach the box from the conveyor belt's parent to let it fall naturally
            other.transform.SetParent(null);
            other.GetComponent<AudioSource>().enabled = true;
        }
    }
}