using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ifHelp{
    public static bool isWithin(this int value, int min, int max)
    {
        return value >= min && value <= max;
    }

}

public class EntityScript : MonoBehaviour //move me into Eventhandler and gamemanager later!
{
    public GameObject player;
    public Transform goal;
    public float rotationSpeed;
    public int moveSpeed;
    public int severity;
    private int diffStep = 800;
    private bool severe;


    private void Start()
    {
        Invoke("eyes", 1.0f);
    }

    private void LateUpdate()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(player.transform.position - transform.position), rotationSpeed * Time.deltaTime);//rotation
        transform.position += transform.forward * moveSpeed * Time.deltaTime;//movement
        severity = (int)(500 - distanceToPlayer); //severity level of nme interactions, based off distance and difficulty
        moveSpeed = severity <= 150 ? 8 : 3;
    }


    public List<GameObject> eventPrefabs;
    //private List<GameObject> runningSounds;




    void decideAction()
    {
        severe = false;
        if (severity < 100)
        {
            print("ambience + occasional owls");
        }
        else if (severity.isWithin(100,150))
        {
            print("ambience + leaves + owls");
        }
        else if (severity.isWithin(150, 200))
        {
            print("ambience + leaves + owls + footsteps");
        }
        else if (severity.isWithin(200, 250))
        {
            print("ambience + leaves + owls + footsteps + eyes");
        }
        else if (severity.isWithin(250, 300)) {
            print("ambience + leaves + owls + footsteps + eyes + growling");
        }
        else if (severity.isWithin(300, 350))
        {
            print("leaves + footsteps");
        }
        else if (severity.isWithin(350, 400)) {
            severe = true;
            print("many eyes + possibly shadowy figure");
        }
        //490 u die, 400 -> 490 it becomes faintly visible?
        //foreach (GameObject sound in runningSounds) {

        //}
        //GameObject owl = Instantiate(eventPrefabs[0], new Vector3(player.transform.position.x-10, player.transform.position.y, player.transform.position.z+10), new Quaternion(0, 0, 0, 0));
        //wait for length of audio clip then
        //GameObject.Destroy(owl);
    }
    

    void eyes() {
        int lifeSpan = Random.Range(10, 31);
        int distance = severe ? 1 : moveSpeed;
        int offset = Random.Range(-10, 10);
        int offsetY = Random.Range(0, 10);
        Vector3 direction = player.transform.position - transform.position;
        Vector3 position = new Vector3((player.transform.position.x - direction.x/distance) + offset, player.transform.position.y + offsetY, (player.transform.position.z - direction.z/distance)+offset);
        GameObject eyes = Instantiate(eventPrefabs[3], position, Quaternion.LookRotation(player.transform.position - position));
        GameObject.Destroy(eyes, lifeSpan);
    }
}
