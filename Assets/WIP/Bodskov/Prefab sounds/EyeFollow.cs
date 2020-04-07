using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EyeFollow : MonoBehaviour
{

    GameObject player;
    

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(player.transform.position - transform.position), 3 * Time.deltaTime);
    }
}
