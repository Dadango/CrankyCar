using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class NewBehaviourScript : MonoBehaviour
{

    public Vector3 ObjectCenterOfMass;
    public bool Awake;
    protected Rigidbody r;

    void start()
    {
        r.centerOfMass = ObjectCenterOfMass;
        r.WakeUp();
        Awake = !r.IsSleeping();
    }
}