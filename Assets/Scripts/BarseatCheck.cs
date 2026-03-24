using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class BarseatCheck : MonoBehaviour
{
    //public GameObject chair;
    public Transform[] chairs;
    public bool[] seatsTaken;
    public GameObject[] peeps;
    private Collider2D seatCollider;

    private void Awake()
    {
        chairs = new Transform[transform.childCount-1];
        peeps = new GameObject[transform.childCount - 1];
        for (int i = 0;i < transform.childCount; i++)
        {
            if (transform.GetChild(i).name.Equals("DrinkSpot"))
            {
                chairs[i]=transform.GetChild(i);
            }
            
        }
        seatsTaken = new bool[chairs.Length];
        
        seatCollider = this.GetComponent<CircleCollider2D>();
        
    }

    private void Update()
    {
        for (int i = 0; i < seatsTaken.Length; i++)
        {
            seatsTaken[i] = SeatCheck(chairs[i].gameObject);

        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        for (int i = 0; i <= seatsTaken.Length; i++)
        {
            if (peeps[i] == null)
            {
                peeps[i] = collision.gameObject;
                break;
            }
        }
    }

    public bool SeatCheck(GameObject seat)
    {
        Seat seatScript = seat.GetComponent<Seat>();
        return seatScript.seatTaken;
    }

    public Transform OpenSeat(GameObject seat)
    {
        Seat seatScript = seat.GetComponent<Seat>();
        if (seatScript.seatTaken)
        {
            return seat.transform;
        }
        else
        {
            return null;
        }
        
    }
}