using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstPersonLook : MonoBehaviour
{
    private bool lockCursor = true;

    public Vector2 mouseSmoothing = new Vector2(2, 2); //TODO: Try to apply smoothing?
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


        transform.Rotate(0, mouseInput.x, 0);
    }
}
