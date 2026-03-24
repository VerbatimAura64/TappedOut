using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ClientSpawner : MonoBehaviour
{
    [SerializeField] private GameObject[] clients;
    public bool MainSpawner;
    public bool SideSpawner;
    //public int rand;
    public bool canSpawn;
    public float clientSpawnTime = 2f;
    private bool drinking;
    private bool leaving;
    private BoxCollider2D boxCollider;
    public int count;
    private float timeUntilClientSpawn;
    private float timeUntilDespawn;
    private Seat check;
    [SerializeField]private float clientSpeed;
    [SerializeField] private GameObject[] chairs;
    [SerializeField] private bool[] seats;
    private List<GameObject> foundObjects;
    private GM gm;
    
    private Animator[] anim;
    private string[] tags;

    private void Awake()
    {
        gm = GameObject.FindGameObjectWithTag("GameController").GetComponent<GM>();
        canSpawn = true;
        SeatsFilled();
        
    }

    private void SeatsFilled()
    {
        if (MainSpawner)
        {
            
            tags = new string[2];
            tags[0] = "Taken";
            tags[1] = "Open";
            foundObjects = new List<GameObject>();

            foreach (string tag in tags)
            {
                GameObject[] objects = GameObject.FindGameObjectsWithTag(tag);
                foundObjects.AddRange(objects);
            }
            chairs = foundObjects.ToArray();
            /*for(int i = 0; i < chairs.Length; i++)
            {
                if (!chairs[i].name.Equals("DrinkSpot " + i))
                {
                    chairs[i] = GameObject.Find("DrinkSpot " + i);
                }
            }*/
            seats = new bool[chairs.Length];
            
        }
    }

    private void SeatChecker()
    {
        for (int i = 0; i < seats.Length; i++)
            {
                check = chairs[i].GetComponent<Seat>();
                seats[i] = check.seatTaken;

            }
        
    }
    private bool CanSpawn()
    {
        if (count != seats.Length)  { 
            if (!gm.levelComplete && !gm.levelFail)
            {
                canSpawn = true;
            }
    }
        else
        {
            canSpawn = false;
        }
        return canSpawn;
    }
    // Update is called once per frame
    void Update()
    {
        SeatChecker();
        CanSpawn();
        SpawnLoop();
        
    }

    private void SpawnLoop()
    {
        //if (canSpawn)
        //{
            //clientSpawnTime = Random.Range(1, 30);
            timeUntilClientSpawn += Time.deltaTime;

            if (timeUntilClientSpawn >= clientSpawnTime)
            {
                Spawn();
                timeUntilClientSpawn = 0;
            }
        //}
    }

    private void Spawn()
    {

        if (SideSpawner)
        {
            GameObject clientToSpawn = clients[Random.Range(0, clients.Length)];

            GameObject spawnedClient = Instantiate(clientToSpawn, transform.position, Quaternion.identity);
            Rigidbody2D clientRB = spawnedClient.GetComponent<Rigidbody2D>();
            clientSpeed = Random.Range(2,3);
            clientRB.linearVelocity = Vector2.down * clientSpeed;
        }
        if (MainSpawner && canSpawn)
        {
            count++;
            GameObject clientToSpawn = clients[Random.Range(0, clients.Length)];
            GameObject spawnedClient = Instantiate(clientToSpawn, transform.position, Quaternion.identity);
            Rigidbody2D clientRB = spawnedClient.GetComponent<Rigidbody2D>();
            
        }

    }
}
