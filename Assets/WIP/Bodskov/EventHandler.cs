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

    public event Action OnEngineStart;
    public void EngineStart()
    {
        OnEngineStart?.Invoke();
    }

    public event Action OnEngineDeath;
    public void EngineDeath()
    {
        OnEngineDeath?.Invoke();
    }

    public static event Action UserEnterInteraction;
    public static void PlayerEnter()
    {
        Debug.Log("User Enter");
        UserEnterInteraction?.Invoke();
    }
}
