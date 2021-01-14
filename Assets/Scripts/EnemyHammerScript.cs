using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHammerScript : MonoBehaviour
{
    public Rigidbody rb;
    public float maxAngVel = 30;

    // Start is called before the first frame update
    void Start()
    {
        rb.maxAngularVelocity = maxAngVel;
    }
}
