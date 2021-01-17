using UnityEngine;

public class PlayerDrillScript : MonoBehaviour
{
    public Rigidbody rb;
    public float minSpeedToSpin;
    public float maxAngVel = 100;
    public GameObject drillFX;

    private void Start()
    {
        rb.maxAngularVelocity = maxAngVel;
    }

    private void FixedUpdate()
    {
        if (rb.velocity.magnitude >= minSpeedToSpin)
        {
            drillFX.SetActive(true);
            rb.AddRelativeTorque(Vector3.up * rb.velocity.magnitude, ForceMode.VelocityChange);
        }
        else
            drillFX.SetActive(false);
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.transform.CompareTag("Enemy") && rb.velocity.magnitude >= minSpeedToSpin)
            other.transform.root.GetComponent<ExplodeAndDestroy>().Execute();
    }
}
