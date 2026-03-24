using UnityEngine;
using System.Collections;
using UnityEngine.Device;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class GM : MonoBehaviour
{
    public Camera cam;
    public int brokenCount = 10;
    public int currScore;
    public int currCustomers;
    public int goalScore;
    public int goalCustomers;
    public Score score;
    public AudioSource jukebox;
    public GameObject bottles;
    public bool levelComplete;
    public bool levelFail;
    public GameObject Cntrl1, Cntrl2, Cntrl3, RetryButton, QuitButton, prompt;

    public GameObject[] spawners;


    [SerializeField] private UnityEngine.SceneManagement.Scene scene;
    private int sceneNumber;
    private int nextSceneNumber;
    public GameObject loseScreen;
    [SerializeField] private GameObject loadScreen;
    [SerializeField] private PlayerMovement pm;
    [SerializeField] private DrinkPour dp;
    [SerializeField] private ClientSpawner cs;

    public void SetInputMode()//string mode)
    {
        if(UnityEngine.Application.isMobilePlatform)//mode == "Mobile")
        {
            //Enable touch controls
            prompt.SetActive(false);
            Cntrl1.SetActive(true);
            Cntrl2.SetActive(true);
            Cntrl3.SetActive(true);
            RetryButton.SetActive(true);
            QuitButton.SetActive(true);

        } else
        {
            //Disable touch screen add in's
            cam.orthographicSize = 6.5f;
            Cntrl1.SetActive(false);
            Cntrl2.SetActive(false);
            Cntrl3.SetActive(false);
            RetryButton.SetActive(false);
            QuitButton.SetActive(false);

        }
    }

    

    private void Awake()
    {
        
        
        SetInputMode();
        pm = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
        dp = GameObject.FindGameObjectWithTag("Player").GetComponent <DrinkPour>();
        cs = GameObject.FindGameObjectWithTag("Spawn").GetComponent<ClientSpawner>();
        loadScreen = GameObject.FindGameObjectWithTag("Load");
        loadScreen.SetActive(false);
        loseScreen = GameObject.FindGameObjectWithTag("Lose");
        loseScreen.SetActive(false);
        spawners = GameObject.FindGameObjectsWithTag("Spawn");
        loadScreen.SetActive(false);
        


        scene = SceneManager.GetActiveScene();
        sceneNumber = SceneManager.GetActiveScene().buildIndex;
        nextSceneNumber = sceneNumber + 1;
        string sceneName = scene.name;
        switch (sceneName)
        {
            case "Level 1":
                Time.timeScale = 1;
                goalScore = 30;
                goalCustomers = 1;
                break;
            case "Level 2":
                goalScore = 75;
                goalCustomers = 3;
                break;
            case "Level 3":
                goalScore = 150;
                goalCustomers = 20;
                break;
            case "Level 4":
                goalScore = 325;
                goalCustomers = 50;
                break;
            case "Level 5":
                goalScore = 500;
                goalCustomers = 100;
                break;
            case "Level 6":
                goalScore = 750;
                goalCustomers = 150;
                break;
            case "Play":
                Time.timeScale = 1;
                goalScore = 99999999;
                goalCustomers = 999999;
                break;
            default:
                break;

        }
        ///SceneManager.GetSceneByName(this.scene.name);
        //score = new Score();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        loadScreen.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        currScore = score.cash;
        bottles.GetComponent<TextMeshProUGUI>().text = brokenCount.ToString();
        if (currCustomers >= goalCustomers || currScore >= goalScore)
        {
            levelComplete = true;
            cs.canSpawn = false;
            if (cs.count == 0)
            {
                LoadNextLevel();
            }
        }
        else if (brokenCount <= 0 || levelFail)
        {
            if (brokenCount <= 0)
            {
                Time.timeScale = 0;
            }
            
            jukebox.Pause();
            pm.enabled = false;
            //for (int i = 0; i < spawners.Length; i++) 
            //{
               // spawners[i].GetComponent<ClientSpawner>().enabled = false;
            //}
           //dp.enabled = false;
            loseScreen.SetActive(true);

            if (Input.GetKeyDown(KeyCode.Escape))
            {
                SceneManager.LoadScene("Title");
            } else if (Input.GetKeyDown(KeyCode.Return))
            {
                if (!scene.name.Equals("Play"))
                {
                    SceneManager.LoadScene("Level 1");
                } else
                {
                    Time.timeScale = 1;
                    SceneManager.LoadScene("Play");
                }
                    
            }
        }
    }

    public void LoadNextLevel()
    {
            if (nextSceneNumber != 9)
            {
                LoadScene(nextSceneNumber);
            }
            else
            {
                SceneManager.LoadScene("Title");
            }
        
    }

    public void LoadScene(int sceneId)
    {
        StartCoroutine(LoadSceneAsync(sceneId));
    }

    IEnumerator LoadSceneAsync(int sceneId)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneId);

        loadScreen.SetActive(true);

        while (!operation.isDone)
        {
            yield return null;
        }
    }

    public void Retry()
    {
        SceneManager.LoadScene(scene.name);
    }

    public void Quit()
    {
        SceneManager.LoadScene("Title");
    }
}
