using UnityEngine;
using System.Collections;
using Cinemachine;

public class PlayerDrillScript : MonoBehaviour
{
    public Rigidbody rb;
    public float minSpeedToSpin;
    public float maxAngVel = 100;
    public GameObject drillFX;
    public AudioClip destroyClip;
    public CinemachineVirtualCamera[] killCams;
    public float minAngSpeedToKill;

    private void Start()
    {
        rb.maxAngularVelocity = maxAngVel;

        GameObject[] killCamsObjects = GameObject.FindGameObjectsWithTag("Kill Cam");
        killCams = new CinemachineVirtualCamera[killCamsObjects.Length];
        for (int i = 0; i < killCamsObjects.Length; i++)
            killCams[i] = killCamsObjects[i].GetComponent<CinemachineVirtualCamera>();
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
        // able to kill enemy
        if (other.transform.CompareTag("Enemy") && rb.angularVelocity.sqrMagnitude >= Mathf.Pow(minAngSpeedToKill, 2f))
        {
            other.transform.root.GetComponent<ExplodeAndDestroy>().Execute();
            SoundController.instance.PlayVariation(destroyClip);
            ScoreController.instance.Inc();
            StartCoroutine(KillFX());
        }
    }

    private IEnumerator KillFX()
    {
        Time.timeScale = .5f;
        CinemachineVirtualCamera tempCam = killCams[Random.Range(0, killCams.Length)];
        int priority = tempCam.Priority;
        tempCam.Priority += 10;
        yield return new WaitForSecondsRealtime(2f);
        tempCam.Priority = priority;
        Time.timeScale = 1f;
    }
}
