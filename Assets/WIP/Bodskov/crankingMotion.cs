using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class crankingMotion : MonoBehaviour
{

    public Transform crankAnchor;
    public Transform crankHandle;

    Vector3 mouseOrientation;
    Vector3 crankOrientation;
    public float speed;

    int framecounter;
    public int fram_limit;


    private void Start()
    {
        Cursor.lockState = CursorLockMode.None;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0)) {
            mouseOrientation = (Input.mousePosition - new Vector3(Screen.width / 2, Screen.height / 2, 20)) - crankAnchor.position;
            crankOrientation = crankHandle.position - crankAnchor.position;

            float angle = Vector3.SignedAngle(mouseOrientation, crankOrientation, Vector3.forward);
            if (!(angle - 10 <= 0 && angle + 10 >= 0)) {
                int sign = -1;
                if (angle <= 0) { sign = 1; }
                framecounter += sign;
                if (framecounter < 0) { framecounter = 0; }
                transform.rotation *= Quaternion.AngleAxis((speed * Time.deltaTime)*sign, Vector3.forward);
                //if (framecounter%10 == 0 ) { Debug.Log(framecounter); }
                if (framecounter >= fram_limit) { framecounter = 0; Debug.Log("The car started!"); EventHandler.EngineStart(); }
            }
        }
    }
}