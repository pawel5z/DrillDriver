using UnityEngine;

[System.Serializable]
public class AxleInfo {
    public WheelCollider leftWheel;
    public WheelCollider rightWheel;
    public bool motor;
    public bool steering;

    public bool isGrounded => leftWheel.isGrounded && rightWheel.isGrounded;
}

public class WheelUtils
{
    // finds the corresponding visual wheel
    // correctly applies the transform
    public static void ApplyLocalPositionToVisuals(WheelCollider collider)
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