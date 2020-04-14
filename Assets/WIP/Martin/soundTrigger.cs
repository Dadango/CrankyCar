using UnityEngine;
using System.Collections;

public class soundTrigger : MonoBehaviour
{

    public AudioSource audio;
    public GameObject player;
    void Start()
    {

    }
    void OnTriggerStay(Collider other)
    {
        audio.Play();
    }

}