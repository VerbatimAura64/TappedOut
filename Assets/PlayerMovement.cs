using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    //Serialize variables to edit them in editor
    [SerializeField]
    private float speed;
    private Rigidbody2D body;
    private Animator anim;
    //private bool grounded;
    //public GameOverScreen GameOverScreen;
    public int score;
    //public Text pointsText;


    void Awake()
    {
        //Gets this objects rigidbody to allow for movement
        body = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }
    // Start is called before the first frame update


    // Update is called once per frame
    void Update()
    {
        //
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        //This is how we move left and right, up and down
        body.linearVelocity = new Vector3(horizontalInput*speed,verticalInput * speed, body.linearVelocity.x);

        //This will flip the sprite based on the input direction we get
        if (horizontalInput > 0.01f)
            transform.localScale = new Vector3(2, 3, 2);
        if (horizontalInput < -0.01f)
            transform.localScale = new Vector3(-2, 3, -2);
        if (verticalInput > 0.01f)
            transform.localScale = new Vector3(-2, 3, 2);
        if (verticalInput < -0.01f)
            transform.localScale = new Vector3(-2, 3, -2);

        anim.SetBool("run", verticalInput != 0 || horizontalInput != 0);
        //anim.SetBool("run", horizontalInput != 0);
    }
}
