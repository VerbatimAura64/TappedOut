using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class TimeToDrink : MonoBehaviour
{
    public GameObject bar;
    [SerializeField] private GameObject[] bars;
    [SerializeField] private GameObject[] seats;
    [SerializeField] private float speed;
    public float drinkTime;
    public BarseatCheck check;
    //public GameObject bars;
    [SerializeField] private Vector3 pos;
    public Score score;
    private BoxCollider2D boxCollider;
    private BoxCollider2D boxCollider2;
    private Animator anim;
    private Rigidbody2D clientRB;
    [SerializeField] private float waitTime;
    private float totalWaitTime = 3;
    private bool waiting;
    public float thisDrinkTime;
    [SerializeField] private bool drinking;
    [SerializeField] private int drinkCount;
    public GameObject chair;
    public bool leaveSeat;
    public bool leaveBar;
    private Client client;
    [SerializeField]private PlayerMovement player;
    private DrinkPour pour;
    private Transform chokePoint;
    //private bool gameFail;
    [SerializeField] private ClientSpawner cs;
    private GM gm;


    private void Awake()
    {
        FindBar();
        client = GetComponent<Client>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
        pour = GameObject.FindGameObjectWithTag("Player").GetComponent<DrinkPour>();
        cs = GameObject.FindGameObjectWithTag("Spawn").GetComponent<ClientSpawner>();
        gm = GameObject.FindGameObjectWithTag("GameController").GetComponent<GM>();
        anim = GetComponent<Animator>();
        score = FindAnyObjectByType<Score>();
        //check = gameObject.GetComponent<BarseatCheck>();
        boxCollider = GetComponent<BoxCollider2D>();
        boxCollider2 = GetComponent<BoxCollider2D>();
        clientRB = this.GetComponent<Rigidbody2D>();
        //seats = new GameObject[2];
        thisDrinkTime = Random.Range(1, 8);
        drinkCount = Random.Range(1, 5);
        clientRB.linearVelocity = Vector2.down * speed;
        boxCollider.enabled = false;
        //transform.Translate(speed, 0, 0);

    }

    void Update()
    {
        //anim.SetBool("run", moving);
        EndGame();
        IsWaiting();
        IsDrinking();
        FinishedDrinking();
        IsLeaving();
        //IsLeaving();
        //float movementSpeed = speed * Time.deltaTime * direction;
        //GoToBar();
        //CheckBar();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Bar")) {
            check = collision.gameObject.GetComponent<BarseatCheck>();
            bar = collision.gameObject;

            CheckBar();
        }
        if (collision.gameObject.CompareTag("BarExit"))
        {
            if (leaveSeat)
            {
                leaveSeat = false;
                leaveBar = true;
            }
        }

        if (collision.gameObject.CompareTag("Pourpoint"))
        {
            //Time.timeScale = 0;
            chokePoint = collision.transform;
            gm.levelFail = true;
            //EndGame();
        }

        if (collision.gameObject.CompareTag("Open"))
        {
            if (chair == null)
            {
                tag = "Waiting";
                clientRB.linearVelocity = Vector2.right * 0;
                boxCollider.enabled = true;
                collision.gameObject.GetComponent<Seat>().seatTaken = true;
                chair = collision.gameObject;
                collision.gameObject.tag = "Taken";
                StartWaiting();
            }
        }
        if (collision.gameObject.CompareTag("Beer"))
        {
            if (!collision.GetComponent<BeerSlide>().empty
                && !collision.GetComponent<BeerSlide>().owned
                && CompareTag("Waiting"))
            {
                tag = "Drinking";
                drinkTime = thisDrinkTime;
                waitTime = 0;
                collision.GetComponent<BeerSlide>().owned = true;
                collision.GetComponent<Collider2D>().enabled = false;
                boxCollider2.enabled = false;
                score.EarnCash(5);
                drinkCount--;
                StartDrinking();
            }

            if (collision.gameObject.CompareTag("Wall"))
            {
                leaveBar = true;
            }
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Taken"))
        {
            if (collision.gameObject == chair)
            {
                chair.GetComponent<Seat>().seatTaken = false;
            }

        }
    }

    public void EndGame()
    {
        if (gm.levelFail)
        {
            gm.loseScreen.GetComponentInChildren<TextMeshProUGUI>().text = "TOO SLOW!";//GetComponentInChildren<TextMeshPro>().text = "TOO SLOW WITH THE CUSTOMERS!";
            gm.loseScreen.SetActive(true);
            
            cs.canSpawn = false;
            chair.GetComponent<Seat>().seatTaken = true;
            chair.SetActive(false);
            
            waiting = false;
             
            
            if(player.transform.position != chokePoint.position)
            {
                player.transform.position = new Vector3(chokePoint.position.x, 
                                                        chokePoint.position.y + .361261f, 
                                                        chokePoint.position.z); 
                player.enabled = false;
                pour.enabled = false;
                
            }
            //Time.timeScale = 0;
            //while (transform.position != chair.GetComponent<Seat>().origin)
            //{
            clientRB.linearVelocity = Vector2.left * speed;///Time.deltaTime;
            //}

        }
        
    }


    private void FindBar()
    {
        bars = GameObject.FindGameObjectsWithTag("Bar");

    }

    private void StartDrinking()
    {
        waiting = false;
        drinking = true;
    }
    private void StartWaiting()
    {
        waiting = true;
    }
    private void IsWaiting()
    {
        if (waiting)
        {
            waitTime += Time.deltaTime;
            if (waitTime > totalWaitTime)
            {
                waitTime = 0;
                chair.transform.position = new Vector3(chair.transform.position.x + .5f,
                                                      chair.transform.position.y,
                                                      chair.transform.position.z);
                this.transform.position = new Vector3(this.transform.position.x + .5f,
                                                      this.transform.position.y,
                                                      this.transform.position.z);
            }
        }
    }
    private void IsDrinking()
    {
        if (drinking)
        {
            drinkTime -= Time.deltaTime;
            if (drinkTime <= 0)
            {
                drinking = false;
                drinkTime = 0;
                boxCollider2.enabled = true;
                waiting = true;
                tag = "Waiting";
                IsWaiting();

            }
        }
        //else
        //{
          //  waitTime++;
            /*if (waitTime >= Time.deltaTime)
            {
                waitTime = 0;
                this.transform.position = new Vector3(this.transform.position.x + 2f,
                                                  this.transform.position.y,
                                                  this.transform.position.z);
            }*/
        //}

    }
    private void CheckBar()
    {
        for(int j = 0; j < bars.Length; j++) {
            if (!leaveSeat || !leaveBar)
            {
                seats = new GameObject[check.chairs.Length];
                for (int i = 0; i < seats.Length; i++)
                {
                    seats[i] = check.chairs[i].gameObject;

                    if (!check.seatsTaken[i])//(check.SeatCheck(check.chairs[i].gameObject))
                    {
                        clientRB.linearVelocity = Vector2.down * 0;
                        clientRB.linearVelocity = Vector2.up * 0;
                        clientRB.linearVelocity = Vector2.right * speed;
                        //check.seatsTaken[i] = true;
                        //Debug.Log(seats[i].gameObject.name + " " + check.SeatCheck(seats[i]));
                        //bar = bar[];
                    }
                    

                }
            }
            if (j == bars.Length)
            {
                for (int i = 0; i < seats.Length; i++)
                {
                    if (check.seatsTaken[i])
                    {
                        leaveBar = true;
                    }
                }
            }
        }
    }

    void IsLeaving()
    {
        //waiting = false;
        if (leaveSeat)
        {
            client.moving = true;
            clientRB.linearVelocity = Vector2.left * speed;

        }
        if (leaveBar)
        {
            leaveSeat = false;
            clientRB.linearVelocity = Vector2.left * 0;
            clientRB.linearVelocity = Vector2.up * speed;
        }
    }
    void FinishedDrinking()
    {
        
        if (leaveSeat || leaveBar) {
            IsLeaving(); 
        }
        if(drinkCount == 0 && !drinking)
        {
            waiting = false;
            //client.moving = true;
            this.tag = "Drunk";
            leaveSeat = true;
            
            this.boxCollider2.isTrigger = true;
            this.boxCollider.isTrigger = true;
            //Debug.LogWarning("LEAVE!!!");
            
        }
    }

    // Update is called once per frame
    
}
