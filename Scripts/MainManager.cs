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

    [SerializeField] private GameObject ballPrefab;
    [SerializeField] private GameObject paddle;
    private Vector3 paddleStartingPosition;

    [SerializeField] public TextMeshProUGUI scoreText;     //inGame branch
    [SerializeField] private GameObject GameOverMenu;      //inGame Branch

    private bool m_Started;     //inGame Branch
    private int currentPoints;      //inGame Branch
    
    private bool m_GameOver;    //inGame Branch

    [SerializeField] private GameObject newHighScoreMenu;   //inGame Branch
    [SerializeField] private MenuManager menuManager;   //inGame Branch

    [SerializeField] private TextMeshProUGUI levelText;     //inGame Branch
    private int currentLevel = 1;       //inGame Branch

    public float lives = 1; //inGame Branch
    [SerializeField] private GameObject life;   //inGame Branch
    [SerializeField] private float xDefaultIncrement; //inGame Branch

    private ComboBehaviour comboBehaviour;  //inGame Branch


    private void Awake() //inGame Branch
    {
        comboBehaviour = gameObject.GetComponent<ComboBehaviour>();

        paddleStartingPosition = paddle.transform.position;
    }


    // Start is called before the first frame update
    void Start()
    {
        NewBall();  //inGame Branch

        const float step = 0.6f;
        int perLine = Mathf.FloorToInt(4.0f / step);
        
        int[] pointCountArray = new [] {1,1,2,2,5,5};
        for (int i = 0; i < LineCount; ++i)
        {
            for (int x = 0; x < perLine; ++x)
            {
                bool instantiateBrick = currentLevel > 1 ? Random.value < .5f : true;

                if (instantiateBrick)
                {
                    Vector3 position = new Vector3(-1.5f + step * x, 2.5f + i * 0.3f, 0);
                    var brick = Instantiate(BrickPrefab, position, Quaternion.identity);
                    brick.PointValue = pointCountArray[i];
                    brick.onDestroyed.AddListener(AddPoints);
                    brick.onAllDestroyed.AddListener(LevelCompleted);
                }
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


    public void NewBall()  //inGame Branch
    {
        m_Started = false;
        comboBehaviour.breakCombo();

        UpdateLives();
        StartCoroutine(ResetPaddle());
    }

    private IEnumerator ResetPaddle()   //inGame Branch
    {
        paddle.transform.position = paddleStartingPosition;
        yield return new WaitForSeconds(0);

        Instantiate(ballPrefab, paddle.transform);
        Ball = GameObject.Find("Ball(Clone)").GetComponent<Rigidbody>();   
    }


    private void AddPoints(int points)      //inGame Branch
    {
        if (comboBehaviour.hasMultiplierReached2) points *= comboBehaviour.getMultiplier(); //inGame Branch

        currentPoints += points;        //inGame Branch
        scoreText.text = currentPoints.ToString();     //inGame Branch

        comboBehaviour.increaseMultiplier();
    }


    private void UpdateLives()  //inGame Branch
    {
        foreach (GameObject life in GameObject.FindGameObjectsWithTag("Life"))
            Destroy(life);

        for (float i = 1; i <= lives; i++)
        {
            Vector3 lifePosition = life.transform.position;
            float xIncrement;


            if (lives % 2 == 0)
                xIncrement = (Mathf.Ceil(i / 2) * 2 - 1) * xDefaultIncrement / 2;
            else
                xIncrement = Mathf.Floor(i / 2) * xDefaultIncrement;

            xIncrement *= i % 2 == 0 ? 1 : -1;
            lifePosition.x += xIncrement;

            Instantiate(life, lifePosition, life.transform.rotation);
        }
    }


    public void LevelCompleted()   //inGame Branch
    {
        // Change this
        lives++;    
        currentLevel++;
        levelText.text = currentLevel.ToString();

        Destroy(GameObject.Find("Ball(Clone)"));
        Start();
    }


    public void GameOver()
    {
        m_GameOver = true;

        menuManager.OpenMenu(GameOverMenu);   //inGame Branch
        UpdateLives();  //inGame Branch
    }
}
