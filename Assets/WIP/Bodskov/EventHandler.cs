using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EventHandler : MonoBehaviour
{
    public static EventHandler current; //singleton reference

    private void Awake()
    {
        current = this;
    }

    public static event Action OnEngineStart;
    public static void EngineStart()
    {
        OnEngineStart?.Invoke();
    }

    public static event Action OnEngineStop;
    public static void EngineStop()
    {
        OnEngineStop?.Invoke();
    }

    public static event Action UserEnterInteraction;
    public static void PlayerEnter()
    {
        Debug.Log("User Enter");
        UserEnterInteraction?.Invoke();
    }
}
