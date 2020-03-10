using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;

    /// <summary>
    /// Reference to active <typeparamref name="GameManager"/> instance
    /// </summary>
    public static GameManager Instance
    {
        get
        {
            return _instance;
        }
    }

    /// <summary>
    /// Sets up a singleton instance for the <typeparamref name="GameManager"/>. Call during Awake().
    /// </summary>
    private void SetupGameManagerInstance()
    {
        if (_instance != null && _instance != this)
        {
            this.enabled = false;
        }
        else
        {
            _instance = this;
        }
    }

    private void Awake()
    {
        SetupGameManagerInstance();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
