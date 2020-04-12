using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(FirstPersonLook))]
[RequireComponent(typeof(FirstPersonMovement))]
[RequireComponent(typeof(Interactor))]
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
            EventHandler.PlayerEnter();
        }
        else if (Input.GetButtonDown("TakeDropInteract"))
        {
            if (interactor.IsInteracting)
            {
                interactor.StopInteraction();
            }
            else
            {
                Interactable interactable = interactor.CheckForInteractables();

                if (interactable is Item) //If interactable is an Item
                {
                    interactor.PickUp((Item)interactable);
                }
                else
                {
                    // When in car and such???
                }
            }
        }
        else if (Input.GetButtonDown("UseActive"))
        {
            if (interactor.IsInteracting)
            {
                interactor.UseIUsablePrimary();
            }
            else
            {
                Interactable interactable = interactor.CheckForInteractables();
                if (interactable is IUsable)
                {
                    if (!(interactable is Item))
                    {
                        (interactable as IUsable).UsePrimary(interactor);
                    }
                    else
                    {
                        (interactable as Item).UseSecondary(interactor);
                    }
                }
            }
        }
    }

    /// <summary>
    /// Sets whether the player can move and look using <typeparamref name="FirstPersonMovement"/> and <typeparamref name="FirstPersonLook"/>.
    /// </summary>
    /// <param name="enabled"></param>
    public void SetPlayerInteractivity(bool enabledBool)
    {
        firstPersonMovement.enabled = enabledBool;
        firstPersonLook.enabled = enabledBool;
    }
}
