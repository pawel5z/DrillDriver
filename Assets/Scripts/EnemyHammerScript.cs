using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHammerScript : MonoBehaviour
{
    public Rigidbody rb;
    public float maxAngVel = 30;
    public float force2Destroy;
    public AudioClip destroyClip;

    // Start is called before the first frame update
    void Start()
    {
        rb.maxAngularVelocity = maxAngVel;
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.transform.CompareTag("Player")
            && (other.impulse / Time.fixedDeltaTime).sqrMagnitude >= Mathf.Pow(force2Destroy, 2f))
        {
            other.transform.root.GetComponent<ExplodeAndDestroy>().Execute();
            SoundController.instance.PlayVariation(destroyClip);
            GameController.instance.GameOver();
        }
    }
}
