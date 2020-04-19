using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class endText : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject textEnd;

    void OnTriggerEnter(Collider other)
    {
        /*if (other.CompareTag("Player"))*/ { textEnd.gameObject.GetComponent<MeshRenderer>().enabled = true; }
    }
}