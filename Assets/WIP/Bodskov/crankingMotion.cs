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
            //Debug.DrawRay(transform.position, crankOrientation, Color.red);
            //Debug.DrawRay(transform.position, mouseOrientation, Color.blue);
            float angle = Vector3.SignedAngle(mouseOrientation, crankOrientation, Vector3.forward);
            if (!(angle - 10 <= 0 && angle + 10 >= 0)) {
                int sign = -1;
                if (angle <= 0) { sign = 1; }
                transform.rotation *= Quaternion.AngleAxis((speed * Time.deltaTime)*sign, Vector3.forward);
            }
        }
    }
}

//sounds of dying car before breakdown (or just to get you worried for no reason)

//Curser.lockState = CurserLockMode.None;
//crank center - cran pos = vector (change rotation to match this vector)