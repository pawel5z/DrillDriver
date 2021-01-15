using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraViewToggle : MonoBehaviour
{
    public GameObject frontviewCam;
    public GameObject rearviewCam;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire2"))
        {
            rearviewCam.SetActive(true);
            frontviewCam.SetActive(false);
        }
        if (Input.GetButtonUp("Fire2"))
        {
            frontviewCam.SetActive(true);
            rearviewCam.SetActive(false);
        }
    }
}
