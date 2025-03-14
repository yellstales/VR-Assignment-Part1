using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddGravityForce : MonoBehaviour
{
    public string targetTag = "assyq";
    public Vector3 forceDirection = Vector3.up;
    public float forceStrength = 9.81f;
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag(targetTag))
        {
            Rigidbody rb = GetComponent<Rigidbody>();
            rb.AddForce(forceDirection.normalized * forceStrength, ForceMode.Impulse);
        }
    }
}
