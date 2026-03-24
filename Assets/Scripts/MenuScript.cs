using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class MenuScript : MonoBehaviour
{

    //public Text text;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //text = GetComponent<Text>();
    }

    public void Tutorial()
    {
        SceneManager.LoadScene("Level 1");
    }

    public void PlayGame()
    {
        SceneManager.LoadScene("Play");
    }
    public void Controls()
    {
        SceneManager.LoadScene("Controls");
    }

    public void Menu()
    {
        SceneManager.LoadScene("Title");
    }

    public void QuitGame()
    {
        Application.Quit();
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.ExitPlaymode();
        #endif
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
