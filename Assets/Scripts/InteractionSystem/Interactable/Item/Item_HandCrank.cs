using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item_HandCrank : Item
{
    public Transform crankHandleLocation;

    private Interactable_CrankAxis _axis = null;

    /// <summary>
    /// Sets _axis where <typeparamref name="HandCrank"/> is attached.
    /// </summary>
    /// <param name="axis"></param>
    public void SetAxis(Interactable_CrankAxis axis)
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

    // Awake is called when the script instance is being loaded
    protected override void Awake()
    {
        base.Awake();
    }

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start(); // runs the code from the base
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update(); // runs the code from the base;

    }

    public override void UsePrimary(Interactor interactor)
    {
        Debug.Log("Hand Crank used");
        //Look for the Crank Axis
        Interactable interactable = interactor.CheckForInteractables();

        //If a Crank Axis is there
        if (interactable is Interactable_CrankAxis)
        {
            Interactable_CrankAxis axis = interactable as Interactable_CrankAxis;

            //Place the crank
            axis.UsePrimary(interactor);
        }
    }

    public override void UseSecondary(Interactor interactor)
    {
        if (IsAttached)
        {
            _axis.UsePrimary(interactor);
        }
        else
        {
            Debug.Log("Handcrank not held or attached to axis.");
        }
    }

    public override void InteractionStart(Interactor interactor)
    {
        if (IsAttached)
        {
            _axis.DetachHandCrank();
            _axis = null;
        }
    }

    public override void InteractionEnd(Interactor interactor)
    {
        //No additional steps
    }
}
