using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public FirstPersonLook firstPersonLook { get; private set; }
    public FirstPersonMovement firstPersonMovement { get; private set; }
    public Interactor interactor { get; private set; }

    // Start is called before the first frame update
    void Start()
    {
        firstPersonLook = gameObject.GetComponent<FirstPersonLook>();
        firstPersonMovement = gameObject.GetComponent<FirstPersonMovement>();
        interactor = gameObject.GetComponent<Interactor>();
    }

    // Update is called once per frame
    void Update()
    {
        Interact();
    }

    void Interact()
    {
        if (Input.GetButtonDown("Interact")) //TODO: Rename to enter
        {
            EventHandler.Interaction();
        }
        else if (Input.GetMouseButtonDown(1)) //TODO: Use "fire"?
        {
            if (interactor.IsInteracting)
            {
                interactor.StopInteraction();
            }
            else
            {
                interactor.CheckForInteractables();
            }
        }
        else if (Input.GetMouseButtonDown(0)) //TODO: Use "fire"?
        {
            interactor.UseItem();
        }
    }
}
