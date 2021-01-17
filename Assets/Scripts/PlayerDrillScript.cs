using UnityEngine;

public class PlayerDrillScript : MonoBehaviour
{
    public Rigidbody rb;
    public float minSpeedToSpin;
    public float maxAngVel = 100;

    private void Start()
    {
        rb.maxAngularVelocity = maxAngVel;
    }

    private void FixedUpdate()
    {
        if (rb.velocity.magnitude >= minSpeedToSpin)
        {
            rb.AddRelativeTorque(Vector3.up * rb.velocity.magnitude, ForceMode.VelocityChange);
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.transform.parent.CompareTag("Enemy") && rb.velocity.magnitude >= minSpeedToSpin)
            other.transform.root.GetComponent<ExplodeAndDestroy>().Execute();
    }
}
