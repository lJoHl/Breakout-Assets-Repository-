using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainManager : MonoBehaviour
{
    public Brick BrickPrefab;
    public int LineCount = 6;
    public Rigidbody Ball;

    [SerializeField] public TextMeshProUGUI scoreText;     //inGame branch
    [SerializeField] private GameObject GameOverMenu;      //inGame Branch

    private bool m_Started;     //inGame Branch
    private int currentPoints;      //inGame Branch
    
    private bool m_GameOver;    //inGame Branch

    [SerializeField] private GameObject newHighScoreMenu;   //inGame Branch
    [SerializeField] private MenuManager menuManager;   //inGame Branch

    [SerializeField] private TextMeshProUGUI levelText;     //inGame Branch
    public int currentLevel = 1;       //inGame Branch

    private static MainManager instance;    //inGame Branch

    [SerializeField] private GameObject canvas; //inGame Branch
    [SerializeField] private GameObject gameOverCanvas; //inGame Branch


    private void Awake()    //inGame Branch
    {
        if (instance != null)
        {
            Destroy(gameObject);
            Destroy(canvas);

            return;
        }

        instance = this;
        DontDestroyOnLoad(gameObject);
        DontDestroyOnLoad(canvas);
    }


    // Start is called before the first frame update
    void Start()
    {
        m_Started = false;  //inGame Branch
        m_GameOver = false; //inGame Branch

        Ball = GameObject.Find("Ball").GetComponent<Rigidbody>();   //inGame Branch

        const float step = 0.6f;
        int perLine = Mathf.FloorToInt(4.0f / step);
        
        int[] pointCountArray = new [] {1,1,2,2,5,5};
        for (int i = 0; i < LineCount; ++i)
        {
            for (int x = 0; x < perLine; ++x)
            {
                Vector3 position = new Vector3(-1.5f + step * x, 2.5f + i * 0.3f, 0);
                var brick = Instantiate(BrickPrefab, position, Quaternion.identity);
                brick.PointValue = pointCountArray[i];
                brick.onDestroyed.AddListener(AddPoints);
                brick.onAllDestroyed.AddListener(LevelCompleted);
            }
        }

    }

    private void Update()
    {
        if (!m_Started)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                m_Started = true;
                float randomDirection = Random.Range(-1.0f, 1.0f);
                Vector3 forceDir = new Vector3(randomDirection, 1, 0);
                forceDir.Normalize();

                Ball.transform.SetParent(null);
                Ball.AddForce(forceDir * 2.0f, ForceMode.VelocityChange);
            }
        }
        else if (m_GameOver)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
        }
    }

    private void AddPoints(int points)      //inGame Branch
    {
        currentPoints += points;        //inGame Branch
        scoreText.text = currentPoints.ToString();     //inGame Branch
    }


    public void LevelCompleted()   //inGame Branch
    {
        // Change this

        currentLevel++;
        levelText.text = currentLevel.ToString();

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        StartCoroutine(WaitForStart());

        IEnumerator WaitForStart()
        {
            yield return new WaitForSeconds(0.1f);
            Start();
        }
    }


    public void GameOver()
    {
        m_GameOver = true;

        Destroy(canvas);        //inGame Branch
        StartCoroutine(WaitForGameOverMenu());   //inGame Branch
        Destroy(gameObject, .2f);    //inGame Branch


        IEnumerator WaitForGameOverMenu()    //inGame Branch
        {
            yield return new WaitForSeconds(.1f);

            Instantiate(gameOverCanvas);
            menuManager.OpenMenu(GameOverMenu);   //inGame Branch
        }
    }
}
