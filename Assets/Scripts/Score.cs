using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    [SerializeField]private TextMeshProUGUI scoreText;
    public int cash;
    //public GM gm;


    public void EarnCash(int money)
    {
        cash += money;
       // gm.currScore += money;
        scoreText.text = "" + cash; //.ToString();
    }


    public void LoseCash(int money)
    {
        cash -= money;
        //gm.currScore -= money;
        scoreText.text = "" + cash;
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
