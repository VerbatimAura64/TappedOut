using UnityEngine;

public class BeerSlide : MonoBehaviour
{
    [SerializeField] private float speed;
    private float direction;
    public bool empty;
    public bool owned;
    private Rigidbody2D beerRB;
    [SerializeField] private float drinkTime;
    public BoxCollider2D boxCollider;
    private float drank;
    private TimeToDrink client;
    [SerializeField] private Score score;
    private GM gm;
    [SerializeField]private float timeToDelete;
    private AudioSource audioS;


    //private Animator anim;

    private void Awake()
    {
        this.enabled = true;
        gm = GameObject.FindGameObjectWithTag("GameController").GetComponent<GM>();
        score = GameObject.FindGameObjectWithTag("Score").GetComponent<Score>();
        empty = false;
        //anim = GetComponent<Animator>();
        boxCollider = GetComponent<BoxCollider2D>();
        beerRB = this.GetComponent<Rigidbody2D>();
        beerRB.linearVelocity = Vector2.left * speed;
        timeToDelete = 5f;
        audioS = GetComponent<AudioSource>();
        //transform.Translate(speed, 0, 0);
    }
    private void Start()
    {
        this.enabled = true;  
        score = GameObject.FindGameObjectWithTag("Score").GetComponent<Score>();
    }

    // Update is called once per frame
    void Update()
    {
        //if (hit) return;
        timeToDelete = timeToDelete - Time.deltaTime;
        if (timeToDelete <= 0 && (!empty || !owned))
        {
            score.LoseCash(10);
            gm.brokenCount--;
            audioS.Play();
            Destroy(gameObject);
        }
        this.enabled = true;
        ReturnBeer();
        if (empty)
            transform.localScale = new Vector3(.38f, .38f, .38f);

    }
    
    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.gameObject.CompareTag("Deleter"))
        {
            
            score.LoseCash(10);
            gm.brokenCount--;
            collision.GetComponent<AudioSource>().Play();
            Destroy(gameObject);
            
        }
        if (collision.gameObject.CompareTag("Bar"))
        {

            score.LoseCash(10);
            gm.brokenCount--;
            collision.GetComponent<AudioSource>().Play();
            Destroy(gameObject);

        }
        if (collision.gameObject.CompareTag("Player"))
        {
            score.EarnCash(3);
            beerRB.linearVelocity = (Vector2.right * 0);
            Destroy(gameObject);
        }
        if (collision.gameObject.CompareTag("Waiting"))
        {
            timeToDelete = 12;
            boxCollider.enabled = false;
            if (!empty)
            {
                //owned = true;
                client = collision.GetComponent<TimeToDrink>();
                beerRB.linearVelocity = (Vector2.left * 0);
            }   
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Client"))
        {
            boxCollider.enabled = true;
        }
        if (collision.gameObject.CompareTag("Waiting"))
        {
            boxCollider.enabled = true;
            //timeToDelete = 4;
        }
        }

    private void ReturnBeer()
    {
        if (client != null)
        {
            if (client.drinkTime <= 0)
            {

                empty = true;
                this.boxCollider.enabled = true;
                beerRB.linearVelocity = Vector2.right * speed;

            }
        }
    }
    private void SetDirection(float _direction)
    {
        direction = _direction;

        float localScaleX = transform.localScale.x;
        if (Mathf.Sign(localScaleX) != _direction)
            localScaleX = -localScaleX;

        transform.localScale = new Vector2(localScaleX, transform.localScale.y);
    }

    
}
