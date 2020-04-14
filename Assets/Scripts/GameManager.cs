using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    /// <summary>
    /// Reference to active <typeparamref name="GameManager"/> instance.
    /// </summary>
    public static GameManager Instance { get; private set; }

    [Header("Breakdown Settings")]
    [Tooltip("Minimum time until breakdown.")]
    public uint minBreakdownTime = 0;
    [Tooltip("Maximum time until breakdown.")]
    public uint maxBreakdownTime = 0;
    [SerializeField]
    private float _timeToBreakdown = 0;
    [SerializeField]
    private bool _carBrokenDown = false;
    public bool carBrokenDown
    {
        get
        { return _carBrokenDown; }
    }
    [SerializeField] 
    private bool _carRunning = false;
    public bool carRunning
    {
        get
        { return _carRunning; }
    }

    [SerializeField]
    private Player _player;
    public Player Player
    { get { return _player; } }

    public GameObject car;

    #region test function for restarting and stopping the car
    public bool engineToggle = false;
    /// <summary>
    /// Test function for restarting and stopping the car. Run in update.
    /// </summary>
    private void EngineToggle()
    {
        if (engineToggle)
        {
            Debug.Log("toggle");
            engineToggle = false;

            if (carRunning)
            { StopCar(); }
            else
            { RestartCar(); }
        }
    }
    #endregion

    /// <summary>
    /// Sets up a singleton instance for the <typeparamref name="GameManager"/>. Call during Awake().
    /// </summary>
    private void SetupGameManagerInstance()
    {
        if (Instance != null && Instance != this)
        {
            this.enabled = false;
        }
        else
        {
            Instance = this;
        }
    }

    private void Awake()
    {
        SetupGameManagerInstance();
    }

    // Start is called before the first frame update
    void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();

        SetNextBreakdown(); //TODO: Call at some other time?
        EventHandler.current.OnEngineStart += RestartCar;
    }

    // Update is called once per frame
    void Update()
    {
        EngineToggle(); //TODO: Remove or comment out later
        BreakdownTimer();
    }

    /// <summary>
    /// Sets up the timer for the next breakdown.
    /// </summary>
    private void SetNextBreakdown()
    {
        _timeToBreakdown = UnityEngine.Random.Range(minBreakdownTime, maxBreakdownTime);
    }

    /// <summary>
    /// Runs the timer for car breakdowns. Run in Update().
    /// </summary>
    private void BreakdownTimer()
    {
        if (_carRunning && !_carBrokenDown)
        {
            _timeToBreakdown -= Time.deltaTime;
            if (_timeToBreakdown < 0)
            {
                _carBrokenDown = true;
                _carRunning = false;
                CarBreakdown();
            }
        }
    }

    /// <summary>
    /// Stops the car.
    /// </summary>
    public void StopCar()
    {
        _carRunning = false;
        //should pause breakdown timer if this gets relevant
    }

    /// <summary>
    /// Causes the car to break down.
    /// </summary>
    private void CarBreakdown()
    {
        _carBrokenDown = true;
        _carRunning = false;
        car.GetComponent<SimpleCarController>().enabled = false;
        car.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePosition;
        EventHandler.current.EngineDeath();
    }

    /// <summary>
    /// Restarts the car, and sets up the timer for the next breakdown.
    /// </summary>
    public void RestartCar()
    {
        //EventHandler.current.EngineStart();
        //TODO: make car run again
        _carRunning = true;
        _carBrokenDown = false;
        car.GetComponent<SimpleCarController>().enabled = true;
        car.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
        SetNextBreakdown();
    }

    private void OnDestroy()
    {
        EventHandler.current.OnEngineStart -= RestartCar;
    }
}
