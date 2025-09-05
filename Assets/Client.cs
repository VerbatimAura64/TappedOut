using UnityEngine;

public class Client : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private bool moving;
    private Rigidbody2D body;
    private Animator anim;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        body = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }


    // Update is called once per frame
    void Update()
    {
        anim.SetBool("run", moving);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Spawn") )
        {
            anim.SetBool("run", moving);
            GoDrink();
        }

        if (collision.gameObject.CompareTag("DrinkPoint"))
        {
            anim.SetBool("run", !moving);
            Drink();
        }

        if(collision.gameObject.tag == "Deleter")
        {
            Destroy(gameObject);
        }
    }
    private void GoDrink()
    {

    }

    private void Drink()
    {

    }
    private void LeaveBar()
    {

    }
}
