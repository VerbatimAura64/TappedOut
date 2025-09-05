using UnityEngine;

public class ClientSpawner : MonoBehaviour
{
    [SerializeField] private GameObject[] clients;
    [SerializeField] private bool MainSpawner;
    [SerializeField] private bool SideSpawner;
    //public int rand;
    public float clientSpawnTime = 2f;
    private bool drinking;
    private bool leaving;
    private BoxCollider2D boxCollider;
    private int count;
    private float timeUntilClientSpawn;
    private float timeUntilDespawn;
    [SerializeField]private float clientSpeed;
    
    private Animator[] anim;

    // Update is called once per frame
    void Update()
    {
        
        SpawnLoop();
        
    }

    private void SpawnLoop()
    {
        clientSpawnTime = Random.Range(1, 30);
        timeUntilClientSpawn += Time.deltaTime;

        if(timeUntilClientSpawn >= clientSpawnTime)
        {
            Spawn();
            timeUntilClientSpawn = 0;
        }
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
        if (MainSpawner)
        {
            GameObject clientToSpawn = clients[Random.Range(0, clients.Length)];

            GameObject spawnedClient = Instantiate(clientToSpawn, transform.position, Quaternion.identity);
            Rigidbody2D clientRB = spawnedClient.GetComponent<Rigidbody2D>();
        }

    }
}
