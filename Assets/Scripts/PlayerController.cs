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
    public float rotationMult;
    public AudioClip jumpClip;

    private float verticalInput;
    private float horizontalInput;
    private bool jump;

    private void Update()
    {
        verticalInput = Input.GetAxis("Vertical");
        horizontalInput = Input.GetAxis("Horizontal");
        jump = Input.GetButton("Jump");
        if (transform.position.y <= -10f)
            GameController.instance.GameOver();
    }

    private void FixedUpdate()
    {
        if (jump && IsAtLeastHalfGrounded()) {
            rb.AddRelativeForce(Vector3.up * jumpForceMult, ForceMode.VelocityChange);
            SoundController.instance.PlayVariation(jumpClip);
        }

        if (!IsAtLeastHalfGrounded())
        {
            rb.angularVelocity = -transform.forward * horizontalInput * rotationMult
                               + transform.right * verticalInput * rotationMult;
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

    private bool IsAtLeastHalfGrounded() {
        WheelCollider[] wheelCols = GetComponentsInChildren<WheelCollider>();
        int groundedCnt = 0;
        foreach (WheelCollider wc in wheelCols)
            if (wc.isGrounded)
                groundedCnt++;
        if (groundedCnt >= 2)
            return true;
        return false;
    }
}
