using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockObjectY : MonoBehaviour
{
    // Reference to the Rigidbody component
    private Rigidbody rb;

    void Start()
    {
        // Get the reference to the Rigidbody component attached to this GameObject
        rb = GetComponent<Rigidbody>();

        // Lock the Rigidbody's position on the Y axis
        rb.constraints = RigidbodyConstraints.FreezePositionY;
    }
}
