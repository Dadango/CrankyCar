using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarPhysics : MonoBehaviour
{
    //Unity's built-in wheel collider
    public WheelCollider leftFrontWheel, rightFrontWheel;
    public WheelCollider leftRearWheel, rightRearWheel;

    public Transform leftFrontT, rightFrontT;
    public Transform leftRearT, rightRearT;

    public float maxTorque = 1500;
    public float maxAngle = 30;

    private float horInput;
    private float verInput;
    private float steeringAngle;

    public void carInput()
    {
        float horInput = Input.GetAxis("Horizontal");
        float verInput = Input.GetAxis("Vertical");
    }

    private void Steering()
    {
        steeringAngle = maxAngle * horInput;
        leftFrontWheel.steerAngle = steeringAngle;
        rightFrontWheel.steerAngle = steeringAngle;
    }

    private void Acceleration()
    {
        leftFrontWheel.motorTorque = verInput * maxTorque;
        rightFrontWheel.motorTorque = verInput * maxTorque;
    }

    private void updateWheels()
    {
        updateWheelPositions(leftFrontWheel, leftFrontT);
        updateWheelPositions(rightFrontWheel, rightFrontT);
        updateWheelPositions(leftRearWheel, leftRearT);
        updateWheelPositions(rightRearWheel, rightRearT);
    }

    private void updateWheelPositions(WheelCollider collider, Transform transf)
    {
        Vector3 pos = transf.position;
        Quaternion rot = transf.rotation;

        collider.GetWorldPose(out pos, out rot);

        transf.position = pos;
        transf.rotation = rot;
    }
/*
    private void updateWheelPositions(WheelCollider collider)
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
*/

    // Update is called once per frame
    void FixedUpdate()
    {
        carInput();
        Steering();
        Acceleration();
        updateWheels();
    }
}
