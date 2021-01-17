// https://docs.unity3d.com/2020.2/Documentation/Manual/WheelColliderTutorial.html
using UnityEngine;
using System.Collections.Generic;
using System;

public class PlayerController : MonoBehaviour {
    public List<AxleInfo> axleInfos; 
    public float maxMotorTorque;
    public float brakeTorque;
    public float maxSteeringAngle;
    public Rigidbody rb;
    public float jumpForceMult;


    private float verticalInput;
    private float steering;
    private bool jump;
    private void Update()
    {
        verticalInput = Input.GetAxis("Vertical");
        steering = maxSteeringAngle * Input.GetAxis("Horizontal");
        jump = Input.GetButtonDown("Jump");
    }

    private void FixedUpdate()
    {
        if (jump && axleInfos.Exists(x => x.isGrounded))
            rb.AddRelativeForce(transform.up * jumpForceMult, ForceMode.VelocityChange);

        foreach (AxleInfo axleInfo in axleInfos) {
            if (axleInfo.steering) {
                axleInfo.leftWheel.steerAngle = steering;
                axleInfo.rightWheel.steerAngle = steering;
            }
            if (axleInfo.motor) {
                float lRpm = axleInfo.leftWheel.rpm;
                if (lRpm * verticalInput != 0 && Math.Sign(lRpm) != Math.Sign(verticalInput))
                    axleInfo.leftWheel.brakeTorque = Mathf.Abs(verticalInput) * brakeTorque;
                else
                {
                    axleInfo.leftWheel.brakeTorque = 0;
                    axleInfo.leftWheel.motorTorque = verticalInput * maxMotorTorque;
                }
                float rRpm = axleInfo.rightWheel.rpm;
                if (rRpm * verticalInput != 0 && Math.Sign(rRpm) != Math.Sign(verticalInput))
                    axleInfo.rightWheel.brakeTorque = Mathf.Abs(verticalInput) * brakeTorque;
                else
                {
                    axleInfo.rightWheel.brakeTorque = 0;
                    axleInfo.rightWheel.motorTorque = verticalInput * maxMotorTorque;
                }
            }
            WheelUtils.ApplyLocalPositionToVisuals(axleInfo.leftWheel);
            WheelUtils.ApplyLocalPositionToVisuals(axleInfo.rightWheel);
        }
    }
}
