// https://docs.unity3d.com/2020.2/Documentation/Manual/WheelColliderTutorial.html
using UnityEngine;
using System.Collections.Generic;

public class EnemyFollow : MonoBehaviour {
    public Rigidbody hammer;
    /* idx 0 - front wheels, idx 1 - rear wheels */
    public List<AxleInfo> axleInfos; 
    public Transform target;
    public float maxMotorTorque;
    public float maxSteeringAngle;
    public float steeringSens = 3f;
    public float steeringAngleMargin = 5f;

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

    public void FixedUpdate()
    {
        Vector3 targetDir = target.position - transform.position;
        float diffAngle = Vector3.SignedAngle(targetDir,
                                              transform.forward,
                                              Vector3.up);

        float motor;
        float steering = axleInfos[0].leftWheel.steerAngle;
        float targetSteering;

        if (Mathf.Abs(diffAngle) <= steeringAngleMargin)
        {
            motor = maxMotorTorque;
            targetSteering = 0f;
        }
        else
        {
            motor = maxMotorTorque * .5f;
            targetSteering = Mathf.Clamp(diffAngle, -maxSteeringAngle, maxSteeringAngle);
        }
        steering = Mathf.MoveTowards(steering,
                                     targetSteering,
                                     steeringSens * Time.fixedDeltaTime);

        foreach (AxleInfo axleInfo in axleInfos) {
            if (axleInfo.steering) {
                axleInfo.leftWheel.steerAngle = steering;
                axleInfo.rightWheel.steerAngle = steering;
            }
            if (axleInfo.motor) {
                axleInfo.leftWheel.motorTorque = motor;
                axleInfo.rightWheel.motorTorque = motor;
            }
            ApplyLocalPositionToVisuals(axleInfo.leftWheel);
            ApplyLocalPositionToVisuals(axleInfo.rightWheel);
        }
    }
}
