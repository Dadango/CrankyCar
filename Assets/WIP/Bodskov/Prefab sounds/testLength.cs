using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testLength : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        print(gameObject.GetComponent<AudioClip>().length);
    }

}
