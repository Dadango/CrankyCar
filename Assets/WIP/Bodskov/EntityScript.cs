using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityScript : MonoBehaviour //move me into Eventhandler and gamemanager later!
{
    public GameObject player;
    public Transform goal;
    public float rotationSpeed;
    public float moveSpeed;
    public float severity;
    public float difficultyMod;
    private int diffStep = 800;


    private void Start()
    {
        InvokeRepeating("DiffRamp", 10.0f, 2.0f);
        Invoke("eyes", 1.0f);
    }

    private void LateUpdate()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(player.transform.position - transform.position), rotationSpeed * Time.deltaTime);//rotation
        transform.position += transform.forward * moveSpeed * Time.deltaTime;//movement
        severity = difficultyMod + (500 - distanceToPlayer); //severity level of nme interactions, based off distance and difficulty
        moveSpeed = severity <= 150 ? 8 : 3;
    }

    void DiffRamp() {
        if (Vector3.Distance(player.transform.position, goal.position)-200 < diffStep) { //the closer you get the more difficulty ramps up
            diffStep -= 80;
            difficultyMod += 10; //difficulty escalation as player approaches the goal. Increase or change to multiplication?
        };
    }

    public List<GameObject> eventPrefabs;
    //private List<GameObject> runningSounds;

    void decideAction()
    {
        if (severity < 100)
        {
            print("ambience + occasional owls");
        }
        else if (100 < severity && severity < 150)
        {
            print("ambience + leaves + owls");
        }
        else if (150 < severity && severity < 200)
        {
            print("ambience + leaves + owls + footsteps");
        }
        else if (200 < severity && severity < 250)
        {
            print("ambience + leaves + owls + footsteps + eyes");
        }
        else if (250 < severity && severity < 300) {
            print("ambience + leaves + owls + footsteps + eyes + growling");
        }
        else if (300 < severity && severity < 350)
        {
            print("leaves + footsteps");
        }
        else if (350 < severity) {
            print("many eyes + possibly shadowy figure");
        }

        //foreach (GameObject sound in runningSounds) {

        //}
        GameObject owl = Instantiate(eventPrefabs[0], new Vector3(player.transform.position.x-10, player.transform.position.y, player.transform.position.z+10), new Quaternion(0, 0, 0, 0));
        //wait for length of audio clip then
        GameObject.Destroy(owl);
    }

    void eyes() {
        int lifeSpan = Random.Range(10, 31);
        Vector3 direction = player.transform.position - transform.position;
        Vector3 position = new Vector3(player.transform.position.x - direction.x/3, player.transform.position.y, player.transform.position.z - direction.z/3);
        GameObject eyes = Instantiate(eventPrefabs[3], position, Quaternion.LookRotation(player.transform.position - position));
        GameObject.Destroy(eyes, lifeSpan);
    }
}
