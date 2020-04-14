using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EngineController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        EventHandler.OnEngineStart += OnEngineOnline;
        EventHandler.OnEngineStop += OnEngineOffline;
    }

    private void OnEngineOnline() {
        Debug.Log("Colour is now green");
        gameObject.GetComponent<Renderer>().material.SetColor("_Color", Color.green);
    }

    private void OnEngineOffline() {
        Debug.Log("Colour is now red");
        gameObject.GetComponent<Renderer>().material.SetColor("_Color", Color.red);
    }

    private void OnDestroy()
    {
        EventHandler.OnEngineStart -= OnEngineOnline;
        EventHandler.OnEngineStop -= OnEngineOffline;
    }
}
