using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ifHelp
{
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
    public bool scared = false;

    private void Start()
    {
        InvokeRepeating("decideAction", 1.0f, entityDelay); //call this when leaving gas station instead of on start? or just start script when leaving gas station
    }

    private void LateUpdate()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(player.transform.position - transform.position), rotationSpeed * Time.deltaTime);//rotation
        if (scared)
        {
            transform.position += (-1 * transform.forward) * moveSpeed * Time.deltaTime;//movement
            StartCoroutine("scaredCooldown");
        }
        else {
            transform.position += transform.forward * moveSpeed * Time.deltaTime;//movement
        }
        severity = (int)(500 - distanceToPlayer); //severity level of nme interactions, based off distance and difficulty
        moveSpeed = severity <= 150 ? 8 : 3;
        if (severity > 490)
        {
            Debug.Log("Game Over");
            Application.Quit();
        }
    }

    IEnumerator scaredCooldown() {
        yield return new WaitForSeconds(5);
        scared = false;
    }

    public List<GameObject> eventPrefabs;



    void decideAction()
    {
        gameObject.GetComponentInChildren<Light>().enabled = false;
        if (severity < 100)
        {
            if (Random.Range(0, 3) < 2)
            {
                owls();
            }
            print("ambience + occasional owls");
        }
        else if (severity.isWithin(100, 150))
        {
            leaves();
            owls();
            print("ambience + leaves + owls");
        }
        else if (severity.isWithin(150, 200))
        {
            leaves();
            owls();
            steps();
            print("ambience + leaves + owls + footsteps");
        }
        else if (severity.isWithin(200, 250))
        {
            leaves();
            owls();
            steps();
            eyes();
            print("ambience + leaves + owls + footsteps + eyes");
        }
        else if (severity.isWithin(250, 300))
        {
            leaves();
            owls();
            steps();
            eyes();
            growl();
            print("ambience + leaves + owls + footsteps + eyes + growling");
        }
        else if (severity.isWithin(300, 350))
        {
            leaves();
            print("leaves + footsteps");
        }
        else if (severity.isWithin(350, 400))
        {
            growl_s();
            for (int i = 0; i <= Random.Range(2, 10); i++) { eyes(); Debug.Log("severity > 350, multi-eye #: " + i); }
            print("many eyes + severe growling");
        }
        else if (severity.isWithin(400, 490))
        {
            gameObject.GetComponentInChildren<Light>().enabled = true;
            growl_s();
        }

    }

    void owls()
    {
        int offset = Random.Range(-20, 21);
        int offsetY = Random.Range(5, 21);
        Vector3 position = new Vector3(player.transform.position.x + offset, player.transform.position.y + offsetY, player.transform.position.z);
        GameObject owl = Instantiate(eventPrefabs[0], position, Quaternion.LookRotation(player.transform.position - position));
        float lifeSpan = owl.GetComponent<AudioSource>().clip.length;
        lifeSpan = owl.GetComponent<AudioSource>().clip.length < entityDelay ? owl.GetComponent<AudioSource>().clip.length : entityDelay; //lifeSpan = smaller of the two (so the sound doesn't surpass the entityDelay (leading to many simultaneously sounds), but the object doesn't stick around doing nothing)
        GameObject.Destroy(owl, lifeSpan);
    }

    void leaves()
    {
        int offset = Random.Range(-30, 31);
        int offsetY = Random.Range(5, 20);
        Vector3 position = new Vector3(player.transform.position.x + offset, player.transform.position.y + offsetY, player.transform.position.z);
        GameObject leaves = Instantiate(eventPrefabs[1], position, Quaternion.LookRotation(player.transform.position - position));
        float lifeSpan = leaves.GetComponent<AudioSource>().clip.length < entityDelay ? leaves.GetComponent<AudioSource>().clip.length : entityDelay;
        GameObject.Destroy(leaves, lifeSpan);
    }

    void steps()
    {
        int distance = moveSpeed;
        int offset = Random.Range(-20, 21);
        int offsetY = Random.Range(0, 6);
        Vector3 direction = player.transform.position - transform.position;
        Vector3 position = new Vector3((player.transform.position.x - direction.x / distance) + offset, player.transform.position.y + offsetY, (player.transform.position.z - direction.z / distance) + offset);
        GameObject steps = Instantiate(eventPrefabs[2], position, Quaternion.LookRotation(player.transform.position - position));
        float lifeSpan = steps.GetComponent<AudioSource>().clip.length < entityDelay ? steps.GetComponent<AudioSource>().clip.length : entityDelay;
        GameObject.Destroy(steps, lifeSpan);
    }


    void eyes()
    {
        int lifeSpan = Random.Range(10, 31);
        int distance = moveSpeed;
        int offset = Random.Range(-10, 11);
        int offsetY = Random.Range(0, 6);
        Vector3 direction = player.transform.position - transform.position;
        Vector3 position = new Vector3((player.transform.position.x - direction.x / distance) + offset, player.transform.position.y + offsetY, (player.transform.position.z - direction.z / distance) + offset);
        GameObject eyes = Instantiate(eventPrefabs[3], position, Quaternion.LookRotation(player.transform.position - position));
        GameObject.Destroy(eyes, lifeSpan);
    }

    void growl_s()
    {
        int distance = moveSpeed;
        int offset = Random.Range(-10, 11);
        int offsetY = Random.Range(0, 6);
        Vector3 direction = player.transform.position - transform.position;
        Vector3 position = new Vector3((player.transform.position.x - direction.x / distance) + offset, player.transform.position.y + offsetY, (player.transform.position.z - direction.z / distance) + offset);
        GameObject growl_s = Instantiate(eventPrefabs[4], position, Quaternion.LookRotation(player.transform.position - position));
        float lifeSpan = growl_s.GetComponent<AudioSource>().clip.length < entityDelay ? growl_s.GetComponent<AudioSource>().clip.length : entityDelay;
        GameObject.Destroy(growl_s, lifeSpan);
    }

    void growl()
    {
        int distance = moveSpeed;
        int offset = Random.Range(-10, 11);
        int offsetY = Random.Range(0, 6);
        Vector3 direction = player.transform.position - transform.position;
        Vector3 position = new Vector3((player.transform.position.x - direction.x / distance) + offset, player.transform.position.y + offsetY, (player.transform.position.z - direction.z / distance) + offset);
        GameObject growl = Instantiate(eventPrefabs[5], position, Quaternion.LookRotation(player.transform.position - position));
        float lifeSpan = growl.GetComponent<AudioSource>().clip.length < entityDelay ? growl.GetComponent<AudioSource>().clip.length : entityDelay;
        GameObject.Destroy(growl, lifeSpan);
    }
}
