using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AdjustableParameters : MonoBehaviour
{
    private int maxLive = 5;
    private int startLevel = 1;

    private bool atLowLimit;
    private bool atUpLimit;

    private readonly int[] maxLives = { 1, 2, 3, 4, 5 };
    private readonly int[] startLevels = { 1, 5, 10, 15, 20 };

    private TextMeshProUGUI livesCounter;
    private TextMeshProUGUI levelsCounter;

    private static AdjustableParameters instance;


    // Makes object persist between scenes
    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(gameObject);
    }

    // Updates counters text
    private void Update()
    {
        Scene currentScene = SceneManager.GetActiveScene();

        if (currentScene.name == "MainMenu")
        {
            livesCounter = GetParameterCounter("MaxLives").GetComponent<TextMeshProUGUI>();
            levelsCounter = GetParameterCounter("StartLevel").GetComponent<TextMeshProUGUI>();

            livesCounter.text = maxLive.ToString();
            levelsCounter.text = startLevel.ToString();
        }
    }
    private GameObject GetParameterCounter(string parameter)
    {
        return GameObject.Find(parameter).transform.Find("Counter").gameObject;
    }



    public void ModifyParameter(string parameter, bool isAnIncrease)
    {
        switch (parameter)
        {
            case "MaxLives":
                maxLive = UpdateCounter(maxLive, maxLives, isAnIncrease);
                break;

            case "StartLevels":
                startLevel = UpdateCounter(startLevel, startLevels, isAnIncrease);
                break;
        }
    }
    private int UpdateCounter(int counter, int[] values, bool isAnIncrease)
    {
        atLowLimit = counter == values[0];
        atUpLimit = counter == values[^1];

        for (int i = 0; i < values.Length; i++)
        {
            if (counter == values[i])
            {
                if (isAnIncrease)
                {
                    if (!atUpLimit)
                        counter = values[++i];
                }
                else
                {
                    if (!atLowLimit)
                        counter = values[--i];
                }

                break;
            }
        }

        return counter;
    }



    public int getMaxLive()
    {
        return maxLive;
    }
    public int getStartLevel()
    {
        return startLevel;
    }

    public int getMaxStartLevel()
    {
        return startLevels[^1];
    }
}