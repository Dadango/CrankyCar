using UnityEngine;
using System.Collections;

public class soundTrigger : MonoBehaviour
{

    public AudioSource sound;
    public GameObject player;
    void Start()
    {

    }
    void OnTriggerStay(Collider other)
    {
        sound.Play();
    }

}