using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Interactable Item superclass.
/// </summary>
public abstract class Item : Interactable
{

    public Rigidbody rigidbody { get; private set; }

    // Start is called before the first frame update
    protected virtual void Start()
    {
        rigidbody = GetComponent<Rigidbody>(); 
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /// <summary>
    /// Activates item function.
    /// </summary>
    public abstract void UseItem();

    //TODO: Discuss potential change in crank minigame implementation; "if (_interactingWith is Crank && hit is CrankAxel)...

}
