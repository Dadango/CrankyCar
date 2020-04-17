using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enterable_CarDoor : Enterable
{
    public Enterable_CarSeat seat;

    // Start is called before the first frame update

    // Awake is called when the script instance is being loaded
    protected override void Awake()
    {
        base.Awake();
    }

    protected override void Start()
    {
        base.Start(); // runs the code from the base
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update(); // runs the code from the base

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

    public override void Enter(Interactor interactor)
    {
        throw new System.NotImplementedException();
    }
}
