using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private FirstPersonLook _firstPersonLook;
    public FirstPersonLook firstPersonLook
    {
        get
        { return _firstPersonLook; }
    }

    private FirstPersonMovement _firstPersonMovement;
    public FirstPersonMovement firstPersonMovement
    {
        get
        { return _firstPersonMovement; }
    }

    // Start is called before the first frame update
    void Start()
    {
        _firstPersonLook = gameObject.GetComponent<FirstPersonLook>();
        _firstPersonMovement = gameObject.GetComponent<FirstPersonMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        Interact();
    }

    void Interact()
    {
        if (Input.GetButtonDown("Interact"))
        {
            EventHandler.Interaction();
        }
    }
}
