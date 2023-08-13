using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlipperySurfaceHazard : MonoBehaviour
{
    private Collider objectCollider;
    
    private void OnTriggerEnter(Collider other)
    {
        // Check if the entering object has a Rigidbody (to apply forces)
        Rigidbody rb = other.GetComponent<Rigidbody>();

        if (rb != null)
        {
            objectCollider = other.GetComponent<Collider>();
            
            // Reduce friction by setting the material's friction to a low value
            objectCollider.material.dynamicFriction = 0f;
            objectCollider.material.staticFriction = 0f;
            objectCollider.material.frictionCombine = PhysicMaterialCombine.Multiply;

            // Optional: Apply an additional force to make the object slide
            rb.AddForce(other.transform.forward * 100f, ForceMode.Force);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        objectCollider = other.GetComponent<Collider>();

        // Reset friction when the object exits the slippery surface
        objectCollider.material.dynamicFriction = 0.6f;
        objectCollider.material.staticFriction = 0.6f;
        objectCollider.material.frictionCombine = PhysicMaterialCombine.Average;
    }
}
