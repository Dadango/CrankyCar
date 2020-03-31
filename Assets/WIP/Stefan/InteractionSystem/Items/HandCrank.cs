using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandCranck : Item
{
    //public override void Interact(Interactor interactor)
    //{
    //    throw new System.NotImplementedException();
    //}

    public override void UseItem()
    {
        Debug.Log("Hand Crank used");
        //Look for the Crank Axel
        //If the Crank Axel is there, place the Hand Crank there
        //Make the Crank Axel the active Interactable
        //Crank minigame!
        //On the Axel, detach Hand Crank on right click.
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
}
