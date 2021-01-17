using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    public Rigidbody hammer;
    public float forceMult = 10000;
    public GameObject hitFX;

    private void OnTriggerEnter(Collider other) {
        UseHammer(other);
    }

    private void OnTriggerExit(Collider other) {
        UseHammer(other);
    }

    private void UseHammer(Collider other)
    {
        if (other.gameObject.transform.root.CompareTag("Player")) {
            hammer.AddRelativeTorque(Vector3.right * forceMult, ForceMode.VelocityChange);
            Instantiate(hitFX, transform.position, Quaternion.Euler(Vector3.up));
        }
    }
}
