using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplodeAndDestroy : MonoBehaviour
{
    public Transform explodeEffect;
    public Transform wreckage;

    public void Execute() {
        if (explodeEffect != null)
            Instantiate(explodeEffect, transform.position, Quaternion.Euler(Vector3.up));
        if (wreckage != null)
            Instantiate(wreckage, transform.position, transform.rotation);
        Destroy(gameObject);
    }
}
