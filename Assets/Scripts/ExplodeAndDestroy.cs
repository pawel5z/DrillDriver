using UnityEngine;
using Cinemachine;

public class ExplodeAndDestroy : MonoBehaviour
{
    public Transform explodeEffect;
    public Transform wreckage;

    private CinemachineImpulseSource source;

    private void Start()
    {
        source = GetComponent<CinemachineImpulseSource>();
    }

    public void Execute() {
        if (explodeEffect != null)
            Instantiate(explodeEffect, transform.position, Quaternion.Euler(Vector3.up));
        if (wreckage != null)
            Instantiate(wreckage, transform.position, transform.rotation);
        source?.GenerateImpulse();
        Destroy(gameObject);
    }
}
