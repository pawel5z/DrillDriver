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
    public float rotateTorqueMult;


    private float verticalInput;
    private float horizontalInput;
    private bool jump;
    private void Update()
    {
        verticalInput = Input.GetAxis("Vertical");
        horizontalInput = Input.GetAxis("Horizontal");
        jump = Input.GetButton("Jump");
    }

    private void FixedUpdate()
    {
        if (jump && IsHalfGrounded())
            rb.AddRelativeForce(transform.up * jumpForceMult, ForceMode.VelocityChange);

        if (!isGrounded)
        {
            rb.AddRelativeTorque(-Vector3.forward * horizontalInput * rotateTorqueMult, ForceMode.VelocityChange);
            rb.AddRelativeTorque(Vector3.right * verticalInput * rotateTorqueMult, ForceMode.VelocityChange);
        }

        foreach (AxleInfo axleInfo in axleInfos) {
            if (axleInfo.steering) {
                axleInfo.leftWheel.steerAngle = horizontalInput * maxSteeringAngle;
                axleInfo.rightWheel.steerAngle = horizontalInput * maxSteeringAngle;
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

    private bool IsHalfGrounded() {
        WheelCollider[] wheelCols = GetComponentsInChildren<WheelCollider>();
        int groundedCnt = 0;
        foreach (WheelCollider wc in wheelCols)
            if (wc.isGrounded)
                groundedCnt++;
        if (groundedCnt >= 2)
            return true;
        return false;
    }
    
    private bool isGrounded => axleInfos.TrueForAll(a => a.isGrounded);
}
