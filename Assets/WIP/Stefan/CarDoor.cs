using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarDoor : MonoBehaviour
{
    public CarSeat seat;
    public Player player;

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
            player = col.GetComponent<Player>();
        }
    }

    private void OnTriggerExit(Collider col)
    {
        if (col.tag == "Player")
        {
            player = null;
        }
    }

    /// <summary>
    /// Returns whether <typeparamref name="Player"/> is at this door.
    /// </summary>
    /// <returns>Player presence.</returns>
    public bool PlayerAtDoor()
    {
        if (player != null)
        {
            Debug.Log("Player at car door");
            return true;
        }
        return false;
    }
}
