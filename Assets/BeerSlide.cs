using UnityEngine;

public class BeerSlide : MonoBehaviour
{
    [SerializeField] private float speed;
    private float direction;
    private bool hit;

    private BoxCollider2D boxCollider;
    //private Animator anim;

    private void Awake()
    {
        //anim = GetComponent<Animator>();
        boxCollider = GetComponent<BoxCollider2D>();
        Rigidbody2D beerRB = this.GetComponent<Rigidbody2D>();
        beerRB.linearVelocity = Vector2.left * speed;
        //transform.Translate(speed, 0, 0);
    }

    // Update is called once per frame
    void Update()
    {
        //if (hit) return;
        float movementSpeed = speed * Time.deltaTime * direction;
        transform.Translate(movementSpeed, 0, 0);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.gameObject.tag == "Wall" || collision.gameObject.tag == "Deleter")
        {
            Destroy(gameObject);
        }
        /*else if (collision.gameObject.tag != "Bullet"
            && collision.gameObject.tag != "Player" 
            && collision.gameObject.tag != "Ground" 
            && collision.gameObject.tag != "Safe"
            && collision.gameObject.tag != "Spawner"
            && collision.gameObject.tag != "Enemy")
        {
            hit = true;
            boxCollider.enabled = false;
            Debug.Log(collision.gameObject.tag + ", " + collision.gameObject.name); 
            Deactivate();
        }*/
    }

    public void SetDirection(float _direction)
    {
        direction = _direction;
        gameObject.SetActive(true);
        hit = false;
        boxCollider.enabled = true;

        float localScaleX = transform.localScale.x;
        if (Mathf.Sign(localScaleX) != _direction)
            localScaleX = -localScaleX;

        transform.localScale = new Vector3(localScaleX, transform.localScale.y, transform.localScale.z);
    }

    private void Deactivate()
    {
        gameObject.SetActive(false);
    }
}
