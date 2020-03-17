using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager2 : MonoBehaviour
{
    /// <summary>
    /// Reference to active <typeparamref name="GameManager"/> instance.
    /// </summary>
    public static GameManager2 Instance { get; private set; }

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
        SetNextBreakdown(); //TODO: Call at some other time?
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
        Debug.Log(_timeToBreakdown);
    }

    /// <summary>
    /// Runs the timer for car breakdowns. Run in Update().
    /// </summary>
    private void BreakdownTimer()
    {
        if (_carRunning && !_carBrokenDown)
        {
            Debug.Log(_timeToBreakdown);
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
    }

    /// <summary>
    /// Causes the car to break down.
    /// </summary>
    private void CarBreakdown()
    {
        _carBrokenDown = true;
        _carRunning = false;
        Debug.Log("Car has broken down");
        throw new NotImplementedException();
    }

    /// <summary>
    /// Restarts the car, and sets up the timer for the next breakdown.
    /// </summary>
    public void RestartCar()
    {
        //TODO: make car run again
        _carRunning = true;
        _carBrokenDown = false;
        SetNextBreakdown();
    }
}
