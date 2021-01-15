using UnityEngine;

public class PlayerDrillScript : MonoBehaviour
{
    public Rigidbody rb;
    public float minSpeedToSpin;
    public float maxAngVel = 100;
    public float angVelMult;

    private void Start()
    {
        rb.maxAngularVelocity = maxAngVel;
    }

    private void FixedUpdate()
    {
        if (rb.velocity.magnitude >= minSpeedToSpin)
        {
            rb.angularVelocity = -1 * Vector3.up * rb.velocity.magnitude * angVelMult;
        }
    }
}
