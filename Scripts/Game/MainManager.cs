using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainManager : MonoBehaviour
{
    private ComboBehaviour comboBehaviour;
    private AdjustableParameters adjustParams;

    public bool m_Started;
    public bool m_GameOver;

    public Brick brickPrefab;
    private readonly int lineCount = 6;

    [SerializeField] private GameObject ballPrefab;
    private readonly float ballScaleX = .15f;
    private Rigidbody ball;

    [SerializeField] private Transform paddle;
    private Vector3 paddleStartingPosition;
    private Vector3 paddleStartingScale;

    [SerializeField] public TextMeshProUGUI scoreText;
    private int currentPoints;

    [SerializeField] private TextMeshProUGUI levelText;
    private int currentLevel;

    [SerializeField] private Camera mainCamera;
    [SerializeField] private GameObject life;
    [SerializeField] private float lifePosXIncrement;
    private Vector3 livesPosition;
    public float lives = 1;

    [SerializeField] private MenuManager menuManager;
    [SerializeField] private GameObject gameOverMenu;
    [SerializeField] private GameObject newHighScoreMenu;


    private void Awake()
    {
        comboBehaviour = gameObject.GetComponent<ComboBehaviour>();
        adjustParams = GameObject.Find("AdjustableParameters").GetComponent<AdjustableParameters>();

        paddleStartingPosition = paddle.position;
        paddleStartingScale = paddle.localScale;

        livesPosition = mainCamera.ScreenToWorldPoint(GameObject.Find("Lives").transform.position);
        livesPosition.z = 0;

        currentLevel = adjustParams.getStartLevel();
        ControlsSettings.LoadControls();
    }


    // Prepares the level elements
    private void Start()
    {
        NewBall();
        UpdateLevel();
        

        bool firstBrick = false;

        const float step = 0.6f;
        int perLine = Mathf.FloorToInt(4.0f / step);
        
        int[] pointCountArray = {1,1,2,2,5,5};

        // Build the bricks structure
        for (int i = 0; i < lineCount; ++i)
        {
            for (int x = 0; x < perLine; ++x)
            {
                bool noBricks = !firstBrick & x == perLine - 1 & i == lineCount - 1;
                bool instantiateBrick = currentLevel <= adjustParams.getStartLevel() | Random.value < .5f;

                if (instantiateBrick | noBricks)
                {
                    Vector3 position = new(-1.5f + step * x, 2.5f + i * 0.3f, 0);
                    var brick = Instantiate(brickPrefab, position, Quaternion.identity);
                    brick.pointValue = pointCountArray[i];
                    brick.onDestroyed.AddListener(AddPoints);
                    brick.onAllDestroyed.AddListener(LevelCompleted);

                    firstBrick = true;
                }
            }
        }
    }


    private void Update()
    {
        if (Input.GetKeyDown(ControlsSettings.throwBallKey))
        {
            // Throws the ball
            if (!m_Started & ball != null)
            {
                m_Started = true;
                float randomDirection = Random.Range(-1.0f, 1.0f);
                Vector3 forceDir = new(randomDirection, 1, 0);
                forceDir.Normalize();

                ball.transform.SetParent(null);
                ball.AddForce(forceDir * 2.0f, ForceMode.VelocityChange);
            }
            // Starts the game again
            else if (m_GameOver & GameObject.Find(gameOverMenu.name + "(Clone)"))
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
        }
    }


    public void NewBall()
    {
        m_Started = false;
        comboBehaviour.BreakCombo();
        UpdateLives();

        Instantiate(ballPrefab, paddle);
        StartCoroutine(ResetPaddle());
    }
    private IEnumerator ResetPaddle()
    {
        paddle.position = paddleStartingPosition;
        paddle.localScale = paddleStartingScale;

        // Rescales the paddle
        Vector3 paddleScale = new(ChangeDifficultyParameter(paddle.localScale.x, 5, .075f, false), .1f, 1);
        paddle.localScale = paddleScale;

        yield return new WaitForSeconds(0);

        ball = GameObject.Find("Ball(Clone)").GetComponent<Rigidbody>();

        // Rescales the ball
        Vector3 ballScale = new(ballScaleX / paddle.localScale.x, ballScaleX * 10, ballScaleX);
        ball.gameObject.transform.localScale = ballScale;
    }


    private void AddPoints(int points)
    {
        if (comboBehaviour.hasMultiplierReached2) points *= comboBehaviour.getMultiplier();

        currentPoints += points;
        scoreText.text = currentPoints.ToString();

        comboBehaviour.IncreaseMultiplier();
    }


    private void UpdateLives()
    {
        // Delete the previous position of lives
        foreach (GameObject life in GameObject.FindGameObjectsWithTag("Life"))
            Destroy(life);

        // Determines the position in which lives will be instantiated
        for (float i = 1; i <= lives; i++)
        {
            Vector3 lifePosition = livesPosition;
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


    private void UpdateLevel()
    {
        levelText.text = currentLevel.ToString();
    }

    private void LevelCompleted()
    {
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
        UpdateLives();

        if (HighScoresBehaviour.IsItANewHighScore(currentPoints))
            menuManager.OpenMenu(newHighScoreMenu);
        else
            menuManager.OpenMenu(gameOverMenu);
    }
}