using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Subclass of <typeparamref name="Interactable"/>. Defines objects that can be picked up. Implements <typeparamref name="IUsable"/>.
/// </summary>
[RequireComponent(typeof(Rigidbody))]
public abstract class Item : Interactable, IUsable
{
    public new Rigidbody rigidbody { get; private set; }
    public BoxCollider boxCollider { get; protected set; }

    // Awake is called when the script instance is being loaded
    protected override void Awake()
    {
        base.Awake();
        rigidbody = GetComponent<Rigidbody>();
        boxCollider = GetComponent<BoxCollider>();
    }

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start(); // runs the code from the base
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update(); // runs the code from the base
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
    /// Defines additional steps that should happen when <typeparamref name="Item"/> is picked up.
    /// </summary>
    /// <param name="interactor">The interactor.</param>
    public abstract void InteractionStart(Interactor interactor);

    /// <summary>
    /// Defines additional events that should happen when <typeparamref name="Item"/> is dropped.
    /// </summary>
    /// <param name="interactor">The interactor.</param>
    public abstract void InteractionEnd(Interactor interactor);
}
