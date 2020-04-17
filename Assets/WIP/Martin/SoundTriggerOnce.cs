using UnityEngine;
using System.Collections;

public class SoundTriggerOnce : MonoBehaviour
{

    public AudioSource sound;
    public GameObject player;
    private bool collision = false;


    void Start()
    {

    }
    void OnTriggerEnter(Collider other)
    {
        if (!collision)
        {
            sound.Play();
            collision = true;
        }
    }

}