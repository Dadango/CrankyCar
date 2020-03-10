using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gas_light : MonoBehaviour
{
    public int Number_Of_Lights = 0;
    public GameObject Parent;
    // Start is called before the first frame update
    void Start()
    {
        
        for (int i = 0; i < Number_Of_Lights; i++)
        {
            // Creates cylinders for "holders" for the light on the gas station
            GameObject light_holder = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
            light_holder.name = "light_holder" + i;
            light_holder.transform.position = new Vector3(12f - (3.0f * i), 5.5f, 0.0f);
            light_holder.transform.localScale = new Vector3(0.2f, 0.2f, 0.2f);
            light_holder.transform.Rotate(-40.0f, 0.0f, 0.0f);
            light_holder.transform.parent = Parent.transform;

            // Creates point light at a specific position and rotation
            GameObject gasLight = new GameObject("gasLight" + i);
            Light lightComp = gasLight.AddComponent<Light>();
            lightComp.transform.position = new Vector3(12f - (3.0f * i), 5.5f, 0.0f);
            lightComp.transform.localScale = new Vector3(0.2f, 0.2f, 0.2f);
            lightComp.transform.Rotate(-40.0f, 0.0f, 0.0f);

            //Changes intensity dependent on which light it is on the gas station 
            if (i < 1 || i > 7)
            {
                lightComp.intensity = 3;
            }
            else
            {
                lightComp.intensity = 2;
            }
            lightComp.transform.parent = light_holder.transform;



        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
