using UnityEngine;

[ExecuteInEditMode]
public class EasySuspension : MonoBehaviour
{
  
    public float naturalFrequency = 10;
    public float dampingRatio = 0.8f;
    public float forceShift = 0.03f;
    public bool setSuspensionDistance = true;

    Rigidbody car_root;

    void Start()
    {
        car_root = GetComponent<Rigidbody>();
    }

    void Update()
    {
        // Work out the stiffness and damper parameters based on the better spring model.
        foreach (WheelCollider wc in GetComponentsInChildren<WheelCollider>())
        {
            JointSpring spring = wc.suspensionSpring;

            float sqrtWcSprungMass = Mathf.Sqrt(wc.sprungMass);
            spring.spring = sqrtWcSprungMass * naturalFrequency * sqrtWcSprungMass * naturalFrequency;
            spring.damper = 2f * dampingRatio * Mathf.Sqrt(spring.spring * wc.sprungMass);

            wc.suspensionSpring = spring;

            Vector3 wheelRelativeBody = transform.InverseTransformPoint(wc.transform.position);
            float distance = car_root.centerOfMass.y - wheelRelativeBody.y + wc.radius;

            wc.forceAppPointDistance = distance - forceShift;

            // Make sure the spring force at maximum droop is exactly zero
            if (spring.targetPosition > 0 && setSuspensionDistance)
                wc.suspensionDistance = wc.sprungMass * Physics.gravity.magnitude / (spring.targetPosition * spring.spring);
        }
    }
}
