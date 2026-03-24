using UnityEngine;

public class Client : MonoBehaviour
{
    [SerializeField] private float speed;
    public bool moving;
    [SerializeField] private Vector3 positions;
    private Rigidbody2D body;
    private Animator anim;
    private TimeToDrink drink;
    [SerializeField] private ClientSpawner spawner;
    private GM gm;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        body = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        drink = GetComponent<TimeToDrink>();
        gm = GameObject.FindGameObjectWithTag("GameController").GetComponent<GM>();
        spawner = FindAnyObjectByType<ClientSpawner>();
        this.enabled = true;
    }


    // Update is called once per frame
    void Update()
    {
        this.enabled = true;
        anim.SetBool("run", moving);

        if (gm.levelFail)
        {
            anim.SetBool("run", false);
            body.linearVelocity = Vector2.down *0;
            spawner.canSpawn = false;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        if(collision.gameObject.CompareTag("Spawn") )
        {
            this.enabled = true;
            moving = true;
            spawner = collision.gameObject.GetComponent<ClientSpawner>();
            
        }

        if (collision.gameObject.CompareTag("Taken"))
        {
           moving = false;
            
        }
        if (collision.gameObject.CompareTag("Open"))
        {
            moving = false;

        }

        if (collision.gameObject.CompareTag("Exit"))
        {
            Destroy(this.gameObject);
            spawner.count--;
            gm.currCustomers++;
            
        }
        if (collision.gameObject.CompareTag("Beer"))
        {
            moving = false;

        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Taken"))
        {
            moving = true;
            //if (drink.leaveSeat)
            {
                //collision.tag = "Open";
            }
            
        }
        if (collision.gameObject.CompareTag("Open"))
        {
            moving = true;

        }
        if (collision.gameObject.CompareTag("Spawn"))
        {
            this.enabled = true;
            //moving = true;
            spawner = collision.gameObject.GetComponent<ClientSpawner>();

        }
    }
    
}
