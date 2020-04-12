using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Interactable Item superclass.
/// </summary>
public abstract class Item : Interactable, IUsable
{

    public new Rigidbody rigidbody { get; private set; }

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
    /// Activates held item function.
    /// </summary>
    /// <param name="interactor">The interactor.</param>
    public abstract void UsePrimary(Interactor interactor);

    /// <summary>
    /// Activates non-held item function.
    /// </summary>
    /// <param name="interactor">The interactor.</param>
    public abstract void UseSecondary(Interactor interactor);

    /// <summary>
    /// Defines additional events that should happen when <typeparamref name="Item"/> is picked up.
    /// </summary>
    /// <param name="interactor">The interactor.</param>
    public abstract void PickUpEvent(Interactor interactor);

    public void InteractionEndCleanUp(Interactor interactor)
    {
    }
}
