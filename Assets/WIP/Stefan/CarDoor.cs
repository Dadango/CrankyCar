using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarDoor : MonoBehaviour
{
    public CarSeat seat;
    public GameObject player;

    private void OnEnable()
    {
        EventHandler.UserInteraction += EnterCar;
    }
    private void OnDisable()
    {
        EventHandler.UserInteraction -= EnterCar;
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider col)
    {
        if (col.tag == "Player")
        {
            player = col.gameObject;
        }
    }

    private void OnTriggerExit(Collider col)
    {
        if (col.tag == "Player")
        {
            player = null;
        }
    }

    void EnterCar()
    {
        if (player != null)
        {
            //TODO: Move player
            Debug.Log("enter car");
        }
    }
}
