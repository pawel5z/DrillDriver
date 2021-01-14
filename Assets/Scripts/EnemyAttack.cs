using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    public Rigidbody hammer;
    public float forceMult = 10000;

    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.transform.root.CompareTag("Player")) {
            hammer.AddRelativeTorque(Vector3.right * forceMult, ForceMode.VelocityChange);
        }
    }

    private void OnTriggerExit(Collider other) {
        if (other.gameObject.transform.root.CompareTag("Player")) {
            hammer.AddRelativeTorque(Vector3.right * forceMult, ForceMode.VelocityChange);
        }
    }
}
