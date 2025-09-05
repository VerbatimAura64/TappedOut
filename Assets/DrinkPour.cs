using UnityEngine;

public class DrinkPour : MonoBehaviour
{
    [SerializeField] private Transform[] pourPoints;
    [SerializeField] private GameObject beer;
    private Animator anim;
    private string pPointName;
    private bool pourPoint = false;
    public float speed;
    //private PlayerMovement playerMovement;

    private void Awake()
    {
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

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Pourpoint")
        {
            pourPoint = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (pourPoint == true && Input.GetMouseButtonDown(0))
        {
            PourBeer();
        }

    }
    private void PourBeer()
    {
        for (int i = 0; i < pourPoints.Length; i++)
        {
            if (pourPoints[i].gameObject.name == pPointName)
            {
                Debug.Log(i);
                Instantiate(beer, pourPoints[i].position, Quaternion.identity);
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
