using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrankAxis : Interactable, IUsable
{
    //TODO: stuff for storing crank
    private bool cranking = false;
    HandCrank handCrank;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Use(Interactor interactor)
    {
        if (interactor.InteractingWith is HandCrank)
        {
            Debug.Log("CRANK ME");
            AttachHandCrank(interactor.InteractingWith as HandCrank);




        }
        else if(interactor.InteractingWith == this)
        {
            Debug.Log("Stopping cranking");
            interactor.InteractingWith = null;
            cranking = false;

        }
        else if(!cranking)
        {
            interactor.InteractingWith = this;
            cranking = true;
            //CRANKING MINIGAME

        }
        else
        {
            Debug.Log("Hand Crank needed.");
        }
    }

    public void AttachHandCrank(HandCrank crank)
    {

    }

    public void DetachHandCrank(Interactor interactor)
    {

    }
}
