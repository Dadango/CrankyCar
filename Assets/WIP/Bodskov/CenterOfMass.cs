using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class CenterOfMass : MonoBehaviour
{

    public Vector3 ObjectCenterOfMass;
    protected Rigidbody r;

    void start()
    {
        r.centerOfMass = ObjectCenterOfMass;
        r.WakeUp();
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(transform.position + transform.rotation * ObjectCenterOfMass, 1f);
    }
}