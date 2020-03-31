using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Interactable Item superclass.
/// </summary>
public abstract class Item : Interactable, IUsable
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
    /// <param name="interactor">The interactor.</param>
    public abstract void Use(Interactor interactor);
}
