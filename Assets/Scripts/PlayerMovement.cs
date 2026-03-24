using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    //Serialize variables to edit them in editor
    [SerializeField]
    private float speed;
    public Rigidbody2D body;
    private Animator anim;
    [SerializeField] private Transform[] points;
    [SerializeField] private int ppcount;
    [SerializeField] private Transform bars;
    public Transform location;
    private string pPointName;
    public bool paused;
    public GameObject screen;
    public AudioSource jukebox;
    public GameObject chatbox;


    //private bool grounded;
    //public GameOverScreen GameOverScreen;
    //public int score;
    //public Text pointsText;


    void Awake()
    {
        screen = GameObject.FindGameObjectWithTag("Pause");
        chatbox = GameObject.FindGameObjectWithTag("Tutorial");
        screen.SetActive(false);
        bars = GameObject.Find("Bars").transform;

        for (int i = 0; i < bars.childCount; i++)
        {
            if (bars.GetChild(i).CompareTag("Pourpoint"))
            {
                ppcount++;
            }
        }
        points = new Transform[ppcount];

        for (int i = 0; i < points.Length; i++)
        {
            if (bars.GetChild(i).CompareTag("Pourpoint"))
            {
                points[i] = bars.GetChild(i).GetComponent<Transform>();
            }
        }

        
        //Gets this objects rigidbody to allow for movement
        //and animator to animate player movement
        body = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
       
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Pourpoint")
        {
            pPointName = collision.gameObject.name;
            location = collision.gameObject.GetComponent<Transform>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        //

        if (chatbox != null)
        {
            if (Input.GetKeyDown(KeyCode.Return))
            {
                chatbox.SetActive(false);
            }
        }
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxisRaw("Vertical");
        //This is how we move left and right, up and down
        //body.linearVelocity = new Vector3(horizontalInput*speed,verticalInput * speed, body.linearVelocity.x);

        //This will flip the sprite based on the input direction we get
        //if (horizontalInput > 0.01f)
        //transform.localScale = new Vector3(2, 3, 2);
        // if (horizontalInput < -0.01f)
        // transform.localScale = new Vector3(-2, 3, -2);
        if ((Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow) )&& !paused)
        {

            for (int i = 0; i < points.Length; i++)
            {
                if (i! < points.Length)
                {

                    if (points[i] == location)
                    {
                        this.transform.position = new Vector3(points[i - 1].transform.position.x,
                                                            points[i - 1].transform.position.y + .361261f,
                                                            points[i - 1].transform.position.z);
                        transform.localScale = new Vector3(-2, 3, 2);
                    }
                }
            }
            //this.transform.position = new Vector3(pPointName)
        }
        if ((Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow)) && !paused)
        {
            //anim.SetBool("run", Input.GetKeyDown(KeyCode.S));
            for (int i = 0; i < points.Length; i++)
            {
                if (i < points.Length + 1)
                {

                    if (points[i] == location)
                    {
                        this.transform.position = new Vector3(points[i + 1].transform.position.x,
                                                            points[i + 1].transform.position.y + .361261f,
                                                            points[i + 1].transform.position.z);
                        transform.localScale = new Vector3(-2, 3, -2);
                    }
                }
                /*if (i > points.Length)
                {
                    i = 0;
                    this.transform.position = new Vector3(points[i].transform.position.x,
                                                            points[i].transform.position.y + .361261f,
                                                            points[i].transform.position.z);
                }
                if (i < 0)
                {
                    i = points.Length;
                    this.transform.position = new Vector3(points[i].transform.position.x,
                                                            points[i].transform.position.y + .361261f,
                                                            points[i].transform.position.z);

                }*/
            }
        }


        anim.SetBool("run", Input.GetKeyDown(KeyCode.W)  
                    || Input.GetKeyDown(KeyCode.S) 
                    || Input.GetKeyDown(KeyCode.DownArrow) 
                    || Input.GetKeyDown(KeyCode.UpArrow));
        //anim.SetBool("run", horizontalInput != 0);


        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!paused)
            {
                paused = true;
                Time.timeScale = 0;
                jukebox.Pause();
                screen.SetActive(true);

            }
            else 
            { 
                paused= false;
                screen.SetActive(false);
                jukebox.UnPause();
                Time.timeScale= 1;

            }
        }
        if (paused && Input.GetKeyDown(KeyCode.Q))
        {
            SceneManager.LoadScene("Title");
        }

    }

    public void MoveUp()
    {
        if (!paused)
        {

            for (int i = 0; i < points.Length; i++)
            {
                if (i! < points.Length)
                {

                    if (points[i] == location)
                    {
                        this.transform.position = new Vector3(points[i - 1].transform.position.x,
                                                            points[i - 1].transform.position.y + .361261f,
                                                            points[i - 1].transform.position.z);
                        transform.localScale = new Vector3(-2, 3, 2);
                        anim.SetBool("run", !pPointName.Equals(location));
                    }
                }
            }
        }
    }

    public void MoveDown()
    {
        if (!paused)
        {
            
            for (int i = 0; i < points.Length; i++)
            {
                if (i < points.Length + 1)
                {

                    if (points[i] == location)
                    {
                        this.transform.position = new Vector3(points[i + 1].transform.position.x,
                                                            points[i + 1].transform.position.y + .361261f,
                                                            points[i + 1].transform.position.z);
                        transform.localScale = new Vector3(-2, 3, -2);
                        anim.SetBool("run", true);

                    }
                }
            }
        }
    }
}
