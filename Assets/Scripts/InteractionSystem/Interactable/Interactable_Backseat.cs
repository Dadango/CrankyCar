using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable_Backseat : Interactable, IUsable
{
    private Item _storedItem;

    public bool IsStoringItem
    {
        get
        {
            return _storedItem != null;
        }
    }

    // Awake is called when the script instance is being loaded
    protected override void Awake()
    {
        base.Awake();
    }

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start(); // runs the code from the base
        CheckForStoredAtStart();
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update(); // runs the code from the base

    }

    private void CheckForStoredAtStart()
    {
        if (transform.childCount > 0)
        {
            foreach (Transform child in transform)
            {
                Item temp = child.GetComponent<Item>();
                if (temp)
                {
                    StoreItem(temp);
                    break;
                }
            }
        }
    }

    /// <summary>
    /// Stores <typeparamref name="Item"/> in this <typeparamref name="Backseat"/>.
    /// </summary>
    /// <param name="item">Item to store.</param>
    private void StoreItem(Item item)
    {
        //Attach item to backseat
        _storedItem = item;
        item.transform.parent = this.transform;
        item.transform.position = this.transform.position;
        item.transform.rotation = this.transform.rotation;
        //Make kinematic and trigger
        item.rigidbody.isKinematic = true;
        item.boxCollider.isTrigger = true;
        //Set items layer to default
        item.SetLayer(0);
    }

    /// <summary>
    /// Stores <typeparamref name="Item"/> in this <typeparamref name="Backseat"/>.
    /// </summary>
    /// <param name="item">Item to store.</param>
    /// <param name="interactor">Interactor storing item</param>
    public void StoreItem(Item item, Interactor interactor)
    {
        //Attach item to backseat
        _storedItem = item;
        //Ensure player no longer holds crank
        interactor.InteractingWith = null;
        item.transform.parent = this.transform;
        item.transform.position = this.transform.position;
        item.transform.rotation = this.transform.rotation;
        //Make kinematic and trigger
        item.rigidbody.isKinematic = true;
        item.boxCollider.isTrigger = true;
        //Set items layer to default
        item.SetLayer(0);
    }


    /// <summary>
    /// Detaches and gives <typeparamref name="Item"/> to <typeparamref name="Interactor"/>.
    /// </summary>
    /// <param name="interactor">Interactor fetching item</param>
    public void FetchItem(Interactor interactor)
    {
        _storedItem.rigidbody.isKinematic = true;
        _storedItem.boxCollider.isTrigger = false;
        interactor.InteractingWith = _storedItem;
        //Set items layer to interactable
        _storedItem.SetLayer(8);
        _storedItem = null;

    }

    public void InteractionEnd(Interactor interactor)
    {
    }

    public void InteractionStart(Interactor interactor)
    {
    }

    public void UsePrimary(Interactor interactor)
    {
        if (!IsStoringItem)
        {
            if (interactor.IsInteracting && interactor.InteractingWith is Item) //If player is holding an Item
            {
                Debug.Log("Storing Item");
                StoreItem(interactor.InteractingWith as Item, interactor);
            }
        }
        else
        {
            if (!interactor.IsInteracting)
            {
                FetchItem(interactor);
            }
        }
    }
}
