using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item_Debug : Item
{
    //public override void Interact(Interactor interactor)
    //{
    //    throw new System.NotImplementedException();
    //}


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
        Debug.Log("Debug Item used - Held function.");
    }

    public override void UseSecondary(Interactor interactor)
    {
        Debug.Log("Debug Item used - Secondary function.");
    }

    public override void InteractionStart(Interactor interactor)
    {
        Debug.Log("Debug Item picked up.");
    }

    public override void InteractionEnd(Interactor interactor)
    {
        Debug.Log("Debug Item dropped.");
    }
}
