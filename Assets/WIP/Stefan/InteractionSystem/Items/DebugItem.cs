using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugItem : Item
{
    //public override void Interact(Interactor interactor)
    //{
    //    throw new System.NotImplementedException();
    //}

    public override void UseItem()
    {
        Debug.Log("Debug Item used");
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
