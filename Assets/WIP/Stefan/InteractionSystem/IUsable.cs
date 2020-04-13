using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Interface for objects that can be used in some way.
/// </summary>
public interface IUsable
{
    /// <summary>
    /// Defines primary usage (left-click) when object is being interacted with.
    /// </summary>
    /// <param name="interactor">Interactor performing use-action.</param>
    void UsePrimary(Interactor interactor);

    /// <summary>
    /// Performs additional steps when starting interactions.
    /// </summary>
    /// <param name="interactor">Interactor starting interaction.</param>
    void InteractionStart(Interactor interactor);

    /// <summary>
    /// Performs additional steps when ending interactions.
    /// </summary>
    /// <param name="interactor">Interactor ending interaction.</param>
    void InteractionEnd(Interactor interactor);
}

