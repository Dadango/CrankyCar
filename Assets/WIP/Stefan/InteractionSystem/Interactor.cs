using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactor : MonoBehaviour
{
    Camera cam;

    public float interactRange = 2f;
    public float sphereCastRadius = 0.25f;

    public Transform handLocation;
    [SerializeField]
    private Interactable _interactingWith;
    /// <summary>
    /// Property access to _interacting. Parents and places <typeparamref name="Item"/> on assignment
    /// </summary>
    public Interactable InteractingWith
    {
        get
        {
            return _interactingWith;
        }
        set
        {
            if (value != null)
            {
                if (value is Item)
                {
                    value.transform.parent = handLocation;
                    value.transform.position = handLocation.position;
                    value.transform.rotation = handLocation.rotation;
                    //TODO: Disable collisions?
                }
            }
            else
            {
                if (_interactingWith is Item)
                {
                    _interactingWith.transform.parent = null;
                    Drop((_interactingWith as Item));
                    // TODO: Where to place the item after unparenting..?
                    //TODO: Enable collisions?
                }
            }


            _interactingWith = value;
        }
    }

    public bool IsInteracting
    {
        get
        {
            return InteractingWith != null;
        }
    }

    private void Awake()
    {
        cam = Camera.main;
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    private void Update()
    {

    }

    /// <summary>
    /// Stops interacting with current <typeparamref name="Interactable"/>. Drops it as well if of type <typeparamref name="Item"/>.
    /// </summary>
    public void StopInteraction()
    {
        InteractingWith = null;
    }

    /// <summary>
    /// Spherecast in front of the <typeparamref name="Player"/> to find interactable elements. 
    /// </summary>
    public void CheckForInteractables()
    {
        Vector3 origin = cam.transform.position;
        Vector3 direction = cam.transform.forward;
        int layerMask = 1 << 8;
        RaycastHit[] hits = Physics.SphereCastAll(origin, sphereCastRadius, direction, interactRange, layerMask);

        string hitString = "";
        foreach (RaycastHit hit in hits)
        {
            hitString += hit.transform.name + " ";
        }
        Debug.Log(hitString);
        if (hits.Length == 0)
        {
            //Do nothing
        }
        else if (hits.Length == 1)
        {
            PickUp(hits[0].transform.GetComponent<Item>());
        }
        else
        {
            //Find closest Item
            float[] distances = new float[hits.Length];
            int indexSmallestDistance = 0;
            for (int i = 0; i < hits.Length; i++)
            {
                distances[i] = Vector3.Distance(cam.transform.position, hits[i].transform.position);
                if (distances[i] < distances[indexSmallestDistance])
                {
                    indexSmallestDistance = i;
                }
            }
            PickUp(hits[indexSmallestDistance].transform.GetComponent<Item>());

        }

        //HACK: Call interaction properly

    }

    private void PickUp(Item item)
    {
        InteractingWith = item;
        item.rigidbody.isKinematic = true;
    }

    private void Drop(Item item)
    {
        item.rigidbody.isKinematic = false;
    }

    /// <summary>
    /// If _interactingWith is of type <typeparamref name="Item"/>, activate its function.
    /// </summary>
    public void UseItem()
    {
        if (_interactingWith is Item)
        {
            (_interactingWith as Item).UseItem();
        }
    }

    /// <summary>
    /// Finds a location to drop the item.
    /// </summary>
    /// <returns></returns>
    private Vector3 FindDropLocation()
    {
        throw new NotImplementedException(); //TODO: Implement
    }

    private void OnDrawGizmos()
    {
        if (Application.isPlaying)
        {
            Gizmos.color = Color.white;
            //Ray start
            Gizmos.DrawWireSphere(cam.transform.position, sphereCastRadius);

            //Ray bounds
            Gizmos.DrawRay(cam.transform.position + (cam.transform.up * sphereCastRadius), cam.transform.forward * interactRange);
            Gizmos.DrawRay(cam.transform.position + (cam.transform.right * sphereCastRadius), cam.transform.forward * interactRange);
            Gizmos.DrawRay(cam.transform.position + (-cam.transform.up * sphereCastRadius), cam.transform.forward * interactRange);
            Gizmos.DrawRay(cam.transform.position + (-cam.transform.right * sphereCastRadius), cam.transform.forward * interactRange);

            //Ray end
            Gizmos.DrawWireSphere(cam.transform.position + (cam.transform.forward * interactRange), sphereCastRadius);
        }
    }
}
