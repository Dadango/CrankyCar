using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandCrank : Item
{
    CrankAxis Axis = null;
    public bool IsAttached
    {
        get
        {
            return Axis != null;
        }
    }

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start(); // runs the code from the base
    }

    // Update is called once per frame
    void Update()
    {

    }

    public override void Use(Interactor interactor)
    {
        Debug.Log("Hand Crank used");
        //Look for the Crank Axel
        Interactable interactable = interactor.CheckForInteractables();

        //If the Crank Axel is there
        if (interactable is CrankAxis)
        {
            CrankAxis axis = interactable as CrankAxis;

            //Activate the Axel, placing the crank
            axis.Use(interactor);

            //Make the Crank Axel the active Interactable

            //Crank minigame!
        }


        //On the Axel, detach Hand Crank on right click.
    }

    //TODO: Discuss potential change in crank minigame implementation; "if (_interactingWith is Crank && hit is CrankAxel)...


}
