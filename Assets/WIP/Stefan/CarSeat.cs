using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarSeat : MonoBehaviour
{
    public bool playerInSeat = false;
    //public CarDoor leftDoor;
    public CarDoor rightDoor;


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
}
