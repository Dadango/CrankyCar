using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item_Camera : Item
{
    [Range(0f, 1f)]
    public float hitDirectionThreshold = 0.5f;
    public AnimationCurve flashFallof = new AnimationCurve();

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    public override void PickUpEvent(Interactor interactor)
    {
        throw new System.NotImplementedException();
    }

    public override void UsePrimary(Interactor interactor)
    {
        throw new System.NotImplementedException();
    }

    public override void UseSecondary(Interactor interactor)
    {
        throw new System.NotImplementedException();
    }

    private void ShootCamera()
    {
        //Find direction to enemy
        //Find player orientation
        //Dot product the two (normalized) vectors to get how much the player is looking towards the enemy


    }

    //TODO: some sort of flash. Intensity curve?
}
