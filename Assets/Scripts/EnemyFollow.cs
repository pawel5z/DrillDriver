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
    public float steeringSens = 45f;
    public float steeringAngleMargin = 5f;



    private void FixedUpdate()
    {
        if (target == null) return;

        Vector3 targetDir = target.position - transform.position;
        float diffAngle = Vector3.SignedAngle(transform.forward,
                                              targetDir,
                                              Vector3.up);
        float motor = maxMotorTorque;
        float steering = axleInfos[0].leftWheel.steerAngle;

        float targetSteering;
        if (Mathf.Abs(diffAngle) <= steeringAngleMargin)
            targetSteering = 0f;
        else
            targetSteering = Mathf.Clamp(diffAngle, -maxSteeringAngle, maxSteeringAngle);
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
            WheelUtils.ApplyLocalPositionToVisuals(axleInfo.leftWheel);
            WheelUtils.ApplyLocalPositionToVisuals(axleInfo.rightWheel);
        }
    }
}
