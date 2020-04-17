using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable_CrankAxis : Interactable, IUsable
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
    Item_HandCrank attachedCrank;

    Vector2 mouseOrientation;
    Vector2 crankOrientation;
    public float speed;

    int framecounter;
    public int fram_limit; //Framecount threshold for starting car

    public AudioSource crankingSound;
    public AudioSource engineStart;

    // Awake is called when the script instance is being loaded
    protected override void Awake()
    {
        base.Awake();
    }

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start(); // runs the code from the base
        CrankAttachmentPoint = transform.Find("CrankAttachmentPoint");
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update(); // runs the code from the base
        Cranking();
    }

    public void UsePrimary(Interactor interactor)
    {
        if (interactor.InteractingWith is Item_HandCrank) //If player is holding HandCrank
        {
            Debug.Log("Attaching Crank");
            AttachHandCrank(interactor.InteractingWith as Item_HandCrank, interactor);
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


    /// <summary>
    /// Attaches <typeparamref name="HandCrank"/> to this <typeparamref name="CrankAxis"/>.
    /// </summary>
    /// <param name="crank">HandCrank to attach.</param>
    /// <param name="interactor">Interactor attaching HandCrank</param>
    public void AttachHandCrank(Item_HandCrank crank, Interactor interactor)
    {
        //Attach crank to axis
        attachedCrank = crank;
        crank.SetAxis(this);
        //Ensure player no longer holds crank
        interactor.InteractingWith = null;
        crank.transform.parent = CrankAttachmentPoint.transform;
        crank.transform.position = CrankAttachmentPoint.transform.position;
        crank.transform.rotation = CrankAttachmentPoint.rotation;
        //Make kinematic and trigger
        crank.rigidbody.isKinematic = true;
        crank.boxCollider.isTrigger = true;
    }

    /// <summary>
    /// Detaches <typeparamref name="HandCrank"/> from this <typeparamref name="CrankAxis"/>.
    /// </summary>
    public void DetachHandCrank()
    {
        attachedCrank.rigidbody.isKinematic = true;
        attachedCrank.boxCollider.isTrigger = false;
        attachedCrank = null;
    }

    private void Cranking()
    {
        if (IsCranking)
        {
            //Run craking minigame

            mouseOrientation = (Input.mousePosition - new Vector3(Screen.width / 2, Screen.height / 2));

            Vector3 localCrankOrientation = CrankAttachmentPoint.transform.InverseTransformPoint(attachedCrank.crankHandleLocation.transform.position);
            crankOrientation.x = -localCrankOrientation.x;
            crankOrientation.y = -localCrankOrientation.z;

            float angle = Vector2.SignedAngle(mouseOrientation, crankOrientation);
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
                    if (crankingSound)
                    {
                        crankingSound.Stop();
                    }
                    framecounter = 0;
                    Debug.Log("The car started!");
                    if (engineStart)
                    {
                        engineStart.Play();
                    }
                    GameManager.Instance.RestartCar();
                }
            }

        }
    }

    private void StartCranking(Interactor interactor)
    {
        Debug.Log("Enabling cranking");
        if (crankingSound)
        {
            crankingSound.Play();
        }
        interactor.InteractingWith = this;
        IsCranking = true;
    }

    private void StopCranking(Interactor interactor)
    {
        Debug.Log("Stopping cranking");
        IsCranking = false; //IsCranking must be set to false before setting InteractingWith to null, else it causes an infinite loop with InteractionEnd
        interactor.InteractingWith = null;
        if (crankingSound)
        {
            crankingSound.Stop();
        }
    }

    public void InteractionStart(Interactor interactor)
    {
        //No additional steps
    }

    public void InteractionEnd(Interactor interactor)
    {
        //Ensure that cranking is stopped when "dropping" axis
        if (IsCranking)
        {
            StopCranking(interactor);
        }
    }

}
