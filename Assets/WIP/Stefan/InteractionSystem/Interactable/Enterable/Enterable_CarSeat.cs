using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enterable_CarSeat : Enterable
{
    public Enterable_CarDoor rightDoor;
    public GameObject car;
    Rigidbody car_rigid;

    private void OnEnable()
    {
        EventHandler.UserEnterInteraction += PlayerCarInteraction;
    }
    private void OnDisable()
    {
        EventHandler.UserEnterInteraction -= PlayerCarInteraction;
    }

    // Start is called before the first frame update
    void Start()
    {
        //leftDoor.seat = this;
        rightDoor.seat = this;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /// <summary>
    /// Interaction event with car. Used for entering and exiting car.
    /// </summary>
    public void PlayerCarInteraction()
    {
        if (player != null)
        {
            PlayerExitCar();

        }
        else
        {
            PlayerSitInCar();
        }
    }

    /// <summary>
    /// If <typeparamref name="Player"/> is at rightDoor, places player in the car seat, aligns player orientation with seat, and disables <typeparamref name="FirstPersonMovement"/> and <typeparamref name="CharacterController"/> components.
    /// </summary>
    /// <param name="player"></param>
    private void PlayerSitInCar()
    {
        if (rightDoor.PlayerAtDoor())
        {
            Debug.Log("Player entering car");
            car_rigid = car.GetComponent<Rigidbody>();
            car_rigid.constraints = RigidbodyConstraints.None;

            rightDoor.player.GetComponent<CharacterController>().enabled = false;
            rightDoor.player.transform.SetParent(transform);
            rightDoor.player.transform.position = transform.position;
            rightDoor.player.transform.rotation = transform.rotation;
            rightDoor.player.firstPersonLook.ResetRotX();

            rightDoor.player.firstPersonMovement.enabled = false;

            player = rightDoor.player;

            car.GetComponent<SimpleCarController>().enabled = true;

            
            

        }
    }

    /// <summary>
    /// Makes the <typeparamref name="Player"/> exit the car at rightDoor, aligns player orientation, and enables <typeparamref name="FirstPersonMovement"/> and <typeparamref name="CharacterController"/> components.
    /// </summary>
    private void PlayerExitCar()
    {
        Debug.Log("Player exiting car");
        car.GetComponent<SimpleCarController>().enabled = false;
        car_rigid = car.GetComponent<Rigidbody>();
        car_rigid.constraints = RigidbodyConstraints.FreezePosition;

        player.transform.SetParent(null);
        player.transform.position = rightDoor.transform.position;
        player.transform.rotation = rightDoor.transform.rotation; //Had to change this otherwise the player would have a weird camera angle when exiting the car
        player.transform.rotation = Quaternion.Euler(0, player.transform.rotation.y, 0);
        player.firstPersonLook.ResetRotX();
        
        rightDoor.player.firstPersonMovement.enabled = true;

        
        rightDoor.player.GetComponent<CharacterController>().enabled = true;


        
        


        player = null;
    }

    public override void Enter(Interactor interactor)
    {
        throw new System.NotImplementedException();
    }
}
