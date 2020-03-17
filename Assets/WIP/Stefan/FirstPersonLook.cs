using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstPersonLook : MonoBehaviour
{
    private bool lockCursor = true;
    public bool invertedY = false;
    public float yAxisRotRange = 90f;

    public Camera playerCam;
    private float rotationY = 0f;

    // public Vector2 mouseSmoothing = new Vector2(2, 2); //TODO: Try to apply smoothing?
    public float mouseYSensitivity = 1f;
    private Vector2 _smoothMouse;


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        MouseLook();
    }

    private void MouseLook()
    {
        if (lockCursor)
        {
            Cursor.lockState = CursorLockMode.Locked;
        }

        Vector2 mouseInput = new Vector2(Input.GetAxisRaw("Mouse X"), Input.GetAxisRaw("Mouse Y"));
        if (playerCam != null)
        {
            // playerCam.transform.Rotate(invertedY ? mouseInput.y : -mouseInput.y, 0, 0); //NOTE: Works, but is unclamped

            rotationY += mouseInput.y * mouseYSensitivity;
            rotationY = Mathf.Clamp(rotationY, -yAxisRotRange, yAxisRotRange);

            playerCam.transform.localEulerAngles = new Vector3(-rotationY, playerCam.transform.localEulerAngles.y, playerCam.transform.localEulerAngles.z);
        }

        transform.Rotate(0, mouseInput.x, 0);
    }
}
