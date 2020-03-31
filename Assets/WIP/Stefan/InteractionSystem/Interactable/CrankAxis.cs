using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrankAxis : Interactable, IUsable
{
    //TODO: stuff for storing crank

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Use(Interactor interactor)
    {
        if (interactor.InteractingWith is HandCrank)
        {
            Debug.Log("CRANK ME");

        }
        else
        {
            Debug.Log("Hand Crank needed.");
        }
    }
}
