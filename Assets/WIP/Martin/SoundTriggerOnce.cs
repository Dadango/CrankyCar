using UnityEngine;
using System.Collections;

public class SoundTriggerOnce : MonoBehaviour
{

    public AudioSource audio;
    public GameObject player;
    private bool collision = false;


    void Start()
    {

    }
    void OnTriggerEnter(Collider other)
    {
        if (!collision)
        {
            audio.Play();
            collision = true;
        }
    }

}