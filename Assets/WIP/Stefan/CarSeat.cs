using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarSeat : MonoBehaviour
{
    public Player playerInSeat;
    public CarDoor rightDoor;

    private void OnEnable()
    {
        EventHandler.UserInteraction += PlayerCarInteraction;
    }
    private void OnDisable()
    {
        EventHandler.UserInteraction -= PlayerCarInteraction;
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
        if (playerInSeat != null)
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

            rightDoor.player.GetComponent<CharacterController>().enabled = false;

            rightDoor.player.transform.SetParent(transform);
            rightDoor.player.transform.position = transform.position;
            rightDoor.player.transform.rotation = transform.rotation;
            rightDoor.player.firstPersonLook.ResetRotX();

            rightDoor.player.firstPersonMovement.enabled = false;

            playerInSeat = rightDoor.player;
        }
    }

    /// <summary>
    /// Makes the <typeparamref name="Player"/> exit the car at rightDoor, aligns player orientation, and enables <typeparamref name="FirstPersonMovement"/> and <typeparamref name="CharacterController"/> components.
    /// </summary>
    private void PlayerExitCar()
    {
        Debug.Log("Player exiting car");

        playerInSeat.transform.SetParent(null);
        playerInSeat.transform.position = rightDoor.transform.position;
        playerInSeat.transform.rotation = rightDoor.transform.rotation;
        playerInSeat.firstPersonLook.ResetRotX();

        rightDoor.player.firstPersonMovement.enabled = true;

        rightDoor.player.GetComponent<CharacterController>().enabled = true;

        playerInSeat = null;
    }
}
