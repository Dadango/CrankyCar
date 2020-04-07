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
        if (Input.GetButtonDown("Enter"))
        {
            EventHandler.Interaction();
        }
        else if (Input.GetButtonDown("Interact"))
        {
            if (interactor.IsInteracting)
            {
                interactor.StopInteraction();
            }
            else
            {
                Interactable interactable = interactor.CheckForInteractables();
                //TODO: CheckForInteractables will return stuff. Handle logic here
                if (interactable is Item)
                {
                    interactor.PickUp((Item)interactable);
                }
                else
                {
                    // When in car and such???
                }
            }
        }
        else if (Input.GetButtonDown("Use"))
        {
            if (interactor.IsInteracting)
            {
                interactor.UseItem();
            }
            else
            {
                Interactable interactable = interactor.CheckForInteractables();
                if (!(interactable is Item) && interactable is IUsable) 
                {
                    (interactable as IUsable).Use(interactor);
                }
            }
        }
    }
}
