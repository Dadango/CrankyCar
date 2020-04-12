using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IUsable
{
    /// <summary>
    /// Defines primary usage (left-click) when object is being interacted with.
    /// </summary>
    /// <param name="interactor">Interactor performing use-action.</param>
    void UsePrimary(Interactor interactor);

    /// <summary>
    /// Performs additional cleanup steps when ending interactions.
    /// </summary>
    /// <param name="interactor">Interactor ending interaction.</param>
    void InteractionEndCleanUp(Interactor interactor);
}

