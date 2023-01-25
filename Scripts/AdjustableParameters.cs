using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class AdjustableParameters : MonoBehaviour
{
    private int maxLive;
    private int startLevel;

    private bool atLowLimit;
    private bool atUpLimit;

    private int[] maxLives = { 1, 2, 3, 4, 5 };
    private int[] startLevels = { 1, 5, 10, 15, 20 };

    private TextMeshProUGUI livesCounter;
    private TextMeshProUGUI levelsCounter;


    private void Start()
    {
        maxLive = 5;
        startLevel = 1;

        livesCounter = GetFirstChild("MaxLives").GetComponent<TextMeshProUGUI>();
        levelsCounter = GetFirstChild("StartLevel").GetComponent<TextMeshProUGUI>();
    }

    private GameObject GetFirstChild(string parentGameObject)
    {
        return GameObject.Find(parentGameObject).transform.Find("Counter").gameObject;
    }



    public void ChangeParameter(string parameter, bool isAnIncrease)     //cambiar nombre
    {
        switch (parameter)
        {
            case "MaxLives":
                maxLive = UpdateCounter(maxLive, maxLives, isAnIncrease);
                livesCounter.text = maxLive.ToString();
                break;

            case "StartLevels":
                startLevel = UpdateCounter(startLevel, startLevels, isAnIncrease);
                levelsCounter.text = startLevel.ToString();
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
}
