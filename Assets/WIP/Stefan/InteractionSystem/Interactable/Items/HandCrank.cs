using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandCrank : Item
{
    public Transform crankHandleLocation;

    private CrankAxis _axis = null;

    /// <summary>
    /// Sets _axis.
    /// </summary>
    /// <param name="axis"></param>
    public void SetAxis(CrankAxis axis)
    {
        _axis = axis;
    }

    /// <summary>
    /// Returns whether this <typeparamref name="HandCrank"/> is attached to an axis.
    /// </summary>
    public bool IsAttached
    {
        get
        {
            return _axis != null;
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

    public override void UsePrimary(Interactor interactor)
    {
        Debug.Log("Hand Crank used");
        //Look for the Crank Axis
        Interactable interactable = interactor.CheckForInteractables();

        //If a Crank Axis is there
        if (interactable is CrankAxis)
        {
            CrankAxis axis = interactable as CrankAxis;

            //Activate the Axis, placing the crank
            axis.UsePrimary(interactor);

            //Make the Crank Axis the active Interactable

            //Crank minigame!
        }


        //On the Axis, detach Hand Crank on right click.
    }

    public override void PickUpEvent(Interactor interactor)
    {
        if(IsAttached)
        {
            _axis.DetachHandCrank();
            _axis = null;
        }
    }

    public override void UseSecondary(Interactor interactor)
    {
        if(IsAttached)
        {
            _axis.UsePrimary(interactor);
        }
        else
        {
            Debug.Log("Handcrank not held or attached to axis.");
        }
    }

    //TODO: Discuss potential change in crank minigame implementation; "if (_interactingWith is Crank && hit is CrankAxis)...


}
