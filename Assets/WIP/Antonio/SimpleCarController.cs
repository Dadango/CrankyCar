using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

[System.Serializable]
public class AxleInfo
{
    public WheelCollider leftWheel;
    public WheelCollider rightWheel;
    public bool motor;
    public bool steering;
}

public class SimpleCarController : MonoBehaviour
{
    public List<AxleInfo> axleInfos;
    public float maxMotorTorque;
    public float maxSteeringAngle;
    public bool engineRunning = false;
    public bool isBeingDriven = false;

    private void OnEnable()
    {
        EventHandler.OnEngineStart += StartEngine;
        EventHandler.OnEngineStop += StopEngine;
    }

    private void OnDisable()
    {
        EventHandler.OnEngineStart -= StartEngine;
        EventHandler.OnEngineStop -= StopEngine;
    }

    public void StopEngine()
    {
        engineRunning = false;
    }

    public void StartEngine()
    {
        engineRunning = true;
    }

    public void ApplyLocalPositionToVisuals(WheelCollider collider)
    {
        if (collider.transform.childCount == 0)
        {
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
        if (isBeingDriven)
        {
            float motor = engineRunning ? maxMotorTorque * Input.GetAxis("Vertical") : 0f;
            float steering = maxSteeringAngle * Input.GetAxis("Horizontal");
            foreach (AxleInfo axleInfo in axleInfos)
            {
                if (axleInfo.steering)
                {
                    axleInfo.leftWheel.steerAngle = steering;
                    axleInfo.rightWheel.steerAngle = steering;
                }
                if (axleInfo.motor)
                {
                    axleInfo.leftWheel.motorTorque = motor;
                    axleInfo.rightWheel.motorTorque = motor;
                }
                ApplyLocalPositionToVisuals(axleInfo.leftWheel);
                ApplyLocalPositionToVisuals(axleInfo.rightWheel);
            }
        }
    }
}