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

    [SerializeField] private GameObject ballPrefab; //inGame branch
    private float ballScaleX = .15f;    //inGame branch
    [SerializeField] private Transform paddle; //inGame branch
    private Vector3 paddleStartingPosition;     //inGame branch
    private Vector3 paddleStartingScale;     //inGame branch

    [SerializeField] public TextMeshProUGUI scoreText;     //inGame branch
    [SerializeField] private GameObject GameOverMenu;      //inGame Branch

    private bool m_Started;     //inGame Branch
    private int currentPoints;      //inGame Branch
    
    public bool m_GameOver;    //inGame Branch

    [SerializeField] private GameObject newHighScoreMenu;   //inGame Branch
    [SerializeField] private MenuManager menuManager;   //inGame Branch

    [SerializeField] private TextMeshProUGUI levelText;     //inGame Branch
    private int currentLevel;       //inGame Branch

    public float lives = 1; //inGame Branch
    [SerializeField] private GameObject life;   //inGame Branch
    [SerializeField] private float lifePosXIncrement; //inGame Branch

    private ComboBehaviour comboBehaviour;  //inGame Branch
    private AdjustableParameters adjustParams;


    private void Awake() //inGame Branch
    {
        comboBehaviour = gameObject.GetComponent<ComboBehaviour>();
        adjustParams = GameObject.Find("AdjustableParameters").GetComponent<AdjustableParameters>();

        paddleStartingPosition = paddle.position;
        paddleStartingScale = paddle.localScale;

        currentLevel = adjustParams.getStartLevel();
        ControlsSettings.LoadControls();
    }


    // Start is called before the first frame update
    void Start()
    {
        NewBall();  //inGame Branch
        UpdateLevel();

        bool firstBrick = false;

        const float step = 0.6f;
        int perLine = Mathf.FloorToInt(4.0f / step);
        
        int[] pointCountArray = new [] {1,1,2,2,5,5};
        for (int i = 0; i < LineCount; ++i)
        {
            for (int x = 0; x < perLine; ++x)
            {
                bool noBricks = !firstBrick & x == perLine - 1 & i == LineCount - 1;
                bool instantiateBrick = currentLevel <= adjustParams.getStartLevel() | Random.value < .5f; //inGame Branch

                if (instantiateBrick | noBricks)   //inGame Branch
                {
                    Vector3 position = new Vector3(-1.5f + step * x, 2.5f + i * 0.3f, 0);
                    var brick = Instantiate(BrickPrefab, position, Quaternion.identity);
                    brick.PointValue = pointCountArray[i];
                    brick.onDestroyed.AddListener(AddPoints);
                    brick.onAllDestroyed.AddListener(LevelCompleted);   //inGame Branch

                    firstBrick = true;
                }
            }
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(ControlsSettings.throwBallKey))
        {
            if (!m_Started & Ball != null)
            {
                m_Started = true;
                float randomDirection = Random.Range(-1.0f, 1.0f);
                Vector3 forceDir = new Vector3(randomDirection, 1, 0);
                forceDir.Normalize();

                Ball.transform.SetParent(null);
                Ball.AddForce(forceDir * 2.0f, ForceMode.VelocityChange);
            }
            else if (m_GameOver & GameObject.Find(GameOverMenu.name + "(Clone)"))
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
        paddle.position = paddleStartingPosition;
        paddle.localScale = paddleStartingScale;

        yield return new WaitForSeconds(0);

        Vector3 paddleScale = new Vector3(ChangeDifficultyParameter(paddle.localScale.x, 5, .075f, false), .1f, 1);
        paddle.localScale = paddleScale;

        yield return new WaitForSeconds(0);

        Instantiate(ballPrefab, paddle);
        Ball = GameObject.Find("Ball(Clone)").GetComponent<Rigidbody>();

        Vector3 ballScale = new Vector3(ballScaleX / paddle.localScale.x, ballScaleX * 10, ballScaleX);
        Ball.gameObject.transform.localScale = ballScale;
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
                xIncrement = (Mathf.Ceil(i / 2) * 2 - 1) * lifePosXIncrement / 2;
            else
                xIncrement = Mathf.Floor(i / 2) * lifePosXIncrement;

            xIncrement *= i % 2 == 0 ? 1 : -1;
            lifePosition.x += xIncrement;

            Instantiate(life, lifePosition, life.transform.rotation);
        }
    }


    private void UpdateLevel()  //delete?
    {
        levelText.text = currentLevel.ToString(); //Update level text
    }

    public void LevelCompleted()   //inGame Branch
    {
        // Change this
        if (lives < adjustParams.getMaxLive()) lives++;
        currentLevel++;

        Destroy(GameObject.Find("Ball(Clone)"));
        Start();
    }


    public float ChangeDifficultyParameter(float parameter, int multiple, float amount, bool isAnIncrease)
    {
        int difficultyLimit = adjustParams.getMaxStartLevel();
        int difficultyLevel = currentLevel < difficultyLimit ? currentLevel : difficultyLimit;

        parameter += Mathf.Floor(difficultyLevel / multiple) * (isAnIncrease ? amount : -amount);

        return parameter;
    }


    public void GameOver()
    {
        m_GameOver = true;

        if (HighScoresBehaviour.NewHighScore(currentPoints))
            menuManager.OpenMenu(newHighScoreMenu);
        else
            menuManager.OpenMenu(GameOverMenu);   //inGame Branch

        UpdateLives();  //inGame Branch
    }
}
