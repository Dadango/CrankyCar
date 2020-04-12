using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrankAxis : Interactable, IUsable
{
    private bool _cranking = false;
    private bool IsCranking
    {
        get { return _cranking; }
        set
        {
            _cranking = value;
            if (_cranking)
            {
                //Lock player cam and movement
                GameManager.Instance.Player.SetPlayerInteractivity(false);
                Cursor.lockState = CursorLockMode.None;
            }
            else
            {
                //Unlock player cam and movement
                GameManager.Instance.Player.SetPlayerInteractivity(true);
                Cursor.lockState = CursorLockMode.Locked;
            }
        }
    }

    Transform CrankAttachmentPoint;
    HandCrank attachedCrank;

    Vector3 mouseOrientation;
    Vector3 crankOrientation;
    public float speed;

    int framecounter;
    public int fram_limit; //Framecount threshold for starting car

    // Start is called before the first frame update
    void Start()
    {
        CrankAttachmentPoint = transform.Find("CrankAttachmentPoint");
    }

    // Update is called once per frame
    void Update()
    {
        Cranking();
    }

    public void UsePrimary(Interactor interactor)
    {
        if (interactor.InteractingWith is HandCrank) //If player is holding HandCrank
        {
            Debug.Log("Attaching Crank");
            AttachHandCrank(interactor.InteractingWith as HandCrank, interactor);
        }
        else if (interactor.InteractingWith == this) //If player is currently cranking
        {
            StopCranking(interactor);
        }
        else if (!IsCranking && attachedCrank != null) //If player is not cranking, and crank is attached
        {
            StartCranking(interactor);
        }
        else
        {
            Debug.Log("Hand Crank needed.");
        }
    }

    private void StartCranking(Interactor interactor)
    {
        Debug.Log("Enabling cranking");

        interactor.InteractingWith = this;
        IsCranking = true;
    }

    private void StopCranking(Interactor interactor)
    {
        Debug.Log("Stopping cranking");
        interactor.InteractingWith = null;
        IsCranking = false;
    }

    /// <summary>
    /// Attaches <typeparamref name="HandCrank"/> to this <typeparamref name="CrankAxis"/>.
    /// </summary>
    /// <param name="crank">HandCrank to attach.</param>
    /// <param name="interactor">Interactor attaching HandCrank</param>
    public void AttachHandCrank(HandCrank crank, Interactor interactor)
    {
        attachedCrank = crank;
        crank.SetAxis(this);
        interactor.InteractingWith = null;
        crank.transform.parent = CrankAttachmentPoint.transform;
        crank.transform.position = CrankAttachmentPoint.transform.position;
        crank.transform.rotation = CrankAttachmentPoint.rotation;
        crank.rigidbody.isKinematic = true;
    }

    /// <summary>
    /// Detaches <typeparamref name="HandCrank"/> from this <typeparamref name="CrankAxis"/>.
    /// </summary>
    public void DetachHandCrank()
    {
        attachedCrank = null;
    }

    private void Cranking()
    {
        if (IsCranking)
        {
            //Run craking minigame

            mouseOrientation = (Input.mousePosition - new Vector3(Screen.width / 2, Screen.height / 2, 20)) - transform.position;
            crankOrientation = attachedCrank.crankHandleLocation.position - transform.position;

            float angle = Vector3.SignedAngle(mouseOrientation, crankOrientation, Vector3.up);
            if (!(angle - 10 <= 0 && angle + 10 >= 0))
            {
                int sign = -1;
                if (angle <= 0)
                {
                    sign = 1;
                }
                framecounter += sign;
                if (framecounter < 0)
                {
                    framecounter = 0;
                }
                attachedCrank.transform.rotation *= Quaternion.AngleAxis((speed * Time.deltaTime) * sign, Vector3.up);
                //if (framecounter%10 == 0 ) { Debug.Log(framecounter); }
                if (framecounter >= fram_limit)
                {
                    framecounter = 0;
                    Debug.Log("The car started!");
                    EventHandler.current.EngineStart();
                }
            }

        }
    }

    public void InteractionEndCleanUp(Interactor interactor)
    {
        if(IsCranking)
        {
            StopCranking(interactor);
        }
    }
}
