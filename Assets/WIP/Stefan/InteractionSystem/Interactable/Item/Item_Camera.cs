using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item_Camera : Item
{
    [Header("Camera parameters")]
    [Tooltip("Determines how much the player should be facing towards the enemy to hit. -1 is exact opposite direction, +1 is exactly towards.")]
    [Range(-1f, 1f)]
    public float facingHitThreshold = 0.5f;
    [Tooltip("Determines the max distance the player can be from the enemy to hit. ")]
    public float distanceHitThreshold = 50f;
    [Tooltip("Controls the light intensity animation from the camera's flashbulb.")]
    public AnimationCurve flashFallof = new AnimationCurve();
    public float flashDuration = 1f;
    public int startingCharges = 5;
    private int _charges;
    public Transform enemyTransform; //TODO: Kinda placeholder for now. Integrate with EntityScript!
    private Vector3 enemyLocation
    { get { return enemyTransform.position; } }

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start(); // runs the code from the base
        _charges = startingCharges;
    }

    // Update is called once per frame
    void Update()
    {

    }


    public override void UsePrimary(Interactor interactor)
    {
        ShootCamera();
    }

    public override void UseSecondary(Interactor interactor)
    {
        throw new System.NotImplementedException(); //TODO: Maybe prompt about remaining charges?
    }

    private void ShootCamera()
    {
        if (_charges > 0)
        {
            _charges -= 1;

            Player player = GameManager.Instance.Player;

            //Find vector and distance to enemy
            Vector3 vectorToEnemy = enemyLocation - player.transform.position;
            float distanceToEnemy = vectorToEnemy.magnitude;

            //Find player orientation
            Vector3 playerViewDirection = player.firstPersonLook.playerCam.transform.forward;

            //Dot product the two (normalized) directions to get how much the player is looking towards the enemy
            float facingAmount = Vector3.Dot(playerViewDirection, (vectorToEnemy / distanceToEnemy));

            Debug.Log($"Facing: {facingAmount}, Distance: {distanceToEnemy}, FacingTreshold: {facingAmount >= facingHitThreshold}, DistanceThreshold: {distanceToEnemy <= distanceHitThreshold}");


            //Do something if player is looking enough towards enemy (+ close enough?).
            //TODO: some sort of flash. Intensity curve?
        }
        else
        {
            //TODO: Out of charges. What do?
        }
    }

    public override void InteractionEnd(Interactor interactor)
    {
        //No additional steps
    }

    public override void InteractionStart(Interactor interactor)
    {
        //TODO: Could be used to trigger a quick tutorial when picked up for the first time
    }

}
