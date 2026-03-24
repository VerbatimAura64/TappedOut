using UnityEngine;
using UnityEngine.Timeline;

public class DrinkPour : MonoBehaviour
{
    [SerializeField] private Transform[] points;
    [SerializeField] private int ppcount;
    //[SerializeField] private Transform[] pourPoints;
    [SerializeField] private GameObject beer;
    private Animator anim;
    [SerializeField] private string pPointName;
    private bool pourPoint = false;
    public float speed;
    [SerializeField] private Transform bars;
    public AudioSource clip;
    private PlayerMovement playerMovement;
    //public GameObject chatbox;

    //private PlayerMovement playerMovement;

    private void Awake()
    {
        //chatbox = GameObject.FindGameObjectWithTag("Tutorial");
        playerMovement = GetComponent<PlayerMovement>();
        //points = new Transform[bars.childCount-1];
        for (int i=0; i < bars.childCount; i++)
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
        //{

        //
        //{
        //points[i] = transform.GetChild(i).GetComponent<Transform>();
        //}
        //}

        anim = GetComponent<Animator>();

        //playerMovement = GetComponent<PlayerMovement>();
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Pourpoint")
        {
            pourPoint = true;
            pPointName = collision.gameObject.name;
        }
    }

    /*private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Pourpoint")
        {
            pourPoint = false;
        }
    }*/

    // Update is called once per frame
    void Update()
    {
        if (pourPoint == true && (Input.GetKeyDown(KeyCode.Space)) && !playerMovement.paused)
        {
           // if (chatbox != null)
            //{
            //    chatbox.SetActive(false);
            //}
           PourBeer();
        }

    }

    public void PressBeer()
    {
        if (pourPoint == true && !playerMovement.paused)
        {
            PourBeer();
        }

    }
    private void PourBeer()
    {
        for (int i = 0; i < points.Length; i++)
        {
            if (points[i].gameObject.name == pPointName)
            {
                //Debug.Log(i);
                clip.Play();
                Instantiate(beer, 
                            new Vector3(points[i].position.x -.3f, 
                             points[i].position.y,
                             points[i].position.z), 
                                Quaternion.identity);
                beer.GetComponent<BeerSlide>().enabled = true;
                Rigidbody2D beerRB = beer.GetComponent<Rigidbody2D>();
                beerRB.linearVelocity = Vector2.left * speed;
                
            }
            
        }

    }

    private int FindBeer()
    {
        //for (int i = 0; i < beers.Length; i++)
        //{
          //  if (!beers[i].activeInHierarchy)
            //    return i;
        //}
        return 0;
    }
}
