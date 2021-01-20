using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    public Rigidbody hammer;
    public float forceMult = 10000;
    public GameObject hitFX;

    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.transform.root.CompareTag("Player"))
            UseHammer();
        if (other.gameObject.name == "Hammer")
            Instantiate(hitFX, transform.position, Quaternion.Euler(Vector3.up));
    }

    private void OnTriggerExit(Collider other) {
        if (other.gameObject.transform.root.CompareTag("Player"))
            UseHammer();
        if (other.gameObject.name == "Hammer")
            Instantiate(hitFX, transform.position, Quaternion.Euler(Vector3.up));
    }

    private void UseHammer()
    {
        hammer.AddRelativeTorque(Vector3.right * forceMult, ForceMode.VelocityChange);
    }
}
