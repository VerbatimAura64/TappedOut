using UnityEngine;

public class Seat : MonoBehaviour
{
    public bool seatTaken;
    private Collider2D seatCollider;
    private TimeToDrink client;
    public Vector3 origin; //= transform.position;
    public GameObject whoAreYou;

    private void Awake()
    {
        origin = this.transform.position;
        seatCollider = this.GetComponent<BoxCollider2D>();

    }

    private void Update()
    {
        if (whoAreYou == null) {
            this.tag = "Open";
            this.transform.position = origin;
            seatTaken = false;
            //seatCollider.enabled = true;
        }
        else
        {
            seatTaken = true;
            this.tag = "Taken";
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        if (collision.gameObject.CompareTag("Client"))
        {

                
                seatTaken = true;
                
                this.tag = "Taken";
            
            //seatCollider.enabled = false;
            //seatCollider.enabled = false;
        }
        if (collision.gameObject.CompareTag("Waiting"))
        {
            if (whoAreYou == null && collision.gameObject.GetComponent<TimeToDrink>().chair == this.gameObject)
            {
                whoAreYou = collision.gameObject;
            }
        }
        
    }


    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Drunk"))
        {
            //seatTaken = false;
        }

    }

}
        //if (collision.gameObject.CompareTag("Client"))
        //{
            //client = collision.gameObject.GetComponent<TimeToDrink>();

            //if (!client.leaveSeat)
            //{
          ///      seatTaken = false;
             //   this.tag = "Open";
            //}
            
            //seatCollider.enabled = true;
        //}
       /* if (collision.gameObject.CompareTag("Drunk"))
        {
            //client = collision.gameObject.GetComponent<TimeToDrink>();

            //if (client.leaveSeat)
            {
               //seatTaken = false;
               //this.tag = "Open";
            }

            //seatCollider.enabled = true;
        }
    }
}
    */