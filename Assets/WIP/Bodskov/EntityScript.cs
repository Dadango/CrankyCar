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
    public float entityDelay = 5.0f;

    private void Start()
    {
        InvokeRepeating("decideAction", 1.0f , entityDelay); //call this when leaving gas station instead of on start? or just start script when leaving gas station
    }

    private void LateUpdate()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(player.transform.position - transform.position), rotationSpeed * Time.deltaTime);//rotation
        transform.position += transform.forward * moveSpeed * Time.deltaTime;//movement
        severity = (int)(500 - distanceToPlayer); //severity level of nme interactions, based off distance and difficulty
        moveSpeed = severity <= 150 ? 8 : 3;
        if (severity > 490)
        {
            Debug.Log("Game Over");
            Application.Quit();
        }
    }

    public List<GameObject> eventPrefabs;
    //private List<GameObject> runningSounds;



    void decideAction()
    {
        gameObject.GetComponentInChildren<Light>().enabled = false;
        if (severity < 100)
        {
            print("ambience + occasional owls");
        }
        else if (severity.isWithin(100, 150))
        {
            print("ambience + leaves + owls");
        }
        else if (severity.isWithin(150, 200))
        {
            print("ambience + leaves + owls + footsteps");
        }
        else if (severity.isWithin(200, 250))
        {
            eyes();
            print("ambience + leaves + owls + footsteps + eyes");
        }
        else if (severity.isWithin(250, 300))
        {
            eyes();
            print("ambience + leaves + owls + footsteps + eyes + growling");
        }
        else if (severity.isWithin(300, 350))
        {
            print("leaves + footsteps");
        }
        else if (severity.isWithin(350, 400))
        {
            for (int i = 0; i <= Random.Range(2, 10); i++) { eyes(); Debug.Log("severity > 350, multi-eye #: " + i); }
            print("many eyes + severe growling");
        }
        else if (severity.isWithin(400, 490))
        {
            gameObject.GetComponentInChildren<Light>().enabled = true;
        }
        //foreach (GameObject sound in runningSounds) {

        //}
        //GameObject owl = Instantiate(eventPrefabs[0], new Vector3(player.transform.position.x-10, player.transform.position.y, player.transform.position.z+10), new Quaternion(0, 0, 0, 0));
        //wait for length of audio clip then
        //GameObject.Destroy(owl);
    }

    void owls() {
        int offset = Random.Range(-20, 20);
        int offsetY = Random.Range(5, 20);
        Vector3 position = new Vector3(player.transform.position.x + offset, player.transform.position.y + offsetY, player.transform.position.z);
        GameObject owl = Instantiate(eventPrefabs[0], position, Quaternion.LookRotation(player.transform.position - position));
        float lifeSpan = owl.GetComponent<AudioClip>().length;
        GameObject.Destroy(owl, lifeSpan);
    }
    void eyes() {
        int lifeSpan = Random.Range(10, 31);
        //int distance = severe ? 1 : moveSpeed;
        int distance = moveSpeed;
        int offset = Random.Range(-10, 10);
        int offsetY = Random.Range(0, 5);
        Vector3 direction = player.transform.position - transform.position;
        Vector3 position = new Vector3((player.transform.position.x - direction.x/distance) + offset, player.transform.position.y + offsetY, (player.transform.position.z - direction.z/distance)+offset);
        GameObject eyes = Instantiate(eventPrefabs[4], position, Quaternion.LookRotation(player.transform.position - position));
        GameObject.Destroy(eyes, lifeSpan);
    }
}
