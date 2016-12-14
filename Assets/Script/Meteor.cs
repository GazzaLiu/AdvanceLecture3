using UnityEngine;
using System.Collections;

public class Meteor : MonoBehaviour {

    void OnCollisionEnter(Collision collision) {
        Rigidbody rb = collision.gameObject.GetComponent<Rigidbody>();
        if (rb) {
            rb.AddRelativeForce(GetComponent<Rigidbody>().velocity, ForceMode.VelocityChange);
        }
    }
}
