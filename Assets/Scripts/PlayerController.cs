// https://docs.unity3d.com/2020.2/Documentation/Manual/WheelColliderTutorial.html
using UnityEngine;
using System.Collections.Generic;

public class PlayerController : MonoBehaviour {
    public List<AxleInfo> axleInfos; 
    public float maxMotorTorque;
    public float brakeTorque;
    public float maxSteeringAngle;


    private float motor;
    private float steering;
    private void Update()
    {
        motor = maxMotorTorque * Input.GetAxis("Vertical");
        steering = maxSteeringAngle * Input.GetAxis("Horizontal");
    }

    private void FixedUpdate()
    {
        foreach (AxleInfo axleInfo in axleInfos) {
            if (axleInfo.steering) {
                axleInfo.leftWheel.steerAngle = steering;
                axleInfo.rightWheel.steerAngle = steering;
            }
            if (axleInfo.motor) {
                float lRpm = axleInfo.leftWheel.rpm;
                if (Mathf.Sign(lRpm) == Mathf.Sign(motor) || lRpm == 0)
                {
                    axleInfo.leftWheel.brakeTorque = 0;
                    axleInfo.leftWheel.motorTorque = motor;
                }
                else
                    axleInfo.leftWheel.brakeTorque = brakeTorque;
                float rRpm = axleInfo.rightWheel.rpm;
                if (Mathf.Sign(rRpm) == Mathf.Sign(motor) || rRpm == 0)
                {
                    axleInfo.rightWheel.brakeTorque = 0;
                    axleInfo.rightWheel.motorTorque = motor;
                }
                else
                    axleInfo.rightWheel.brakeTorque = brakeTorque;
            }
            ApplyLocalPositionToVisuals(axleInfo.leftWheel);
            ApplyLocalPositionToVisuals(axleInfo.rightWheel);
        }
    }

    // finds the corresponding visual wheel
    // correctly applies the transform
    public void ApplyLocalPositionToVisuals(WheelCollider collider)
    {
        if (collider.transform.childCount == 0) {
            return;
        }
     
        Transform visualWheel = collider.transform.GetChild(0);
     
        Vector3 position;
        Quaternion rotation;
        collider.GetWorldPose(out position, out rotation);
     
        visualWheel.transform.position = position;
        visualWheel.transform.rotation = rotation;
    }
}
