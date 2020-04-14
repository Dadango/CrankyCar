using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Interactable object superclass.
/// </summary>
public abstract class Enterable : Interactable
{
    /// <summary>
    /// <typeparamref name="Player"/> currently inside or within <typeparamref name="Enterable"/> object.
    /// </summary>
    public Player player;

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
    /// Function to perform enter-action with object.
    /// </summary>
    /// <param name="interactor">The interactor.</param>
    public abstract void Enter(Interactor interactor);
}
