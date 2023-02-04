using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;

public class HighScoresBehaviour : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI[] names;
    [SerializeField] private TextMeshProUGUI[] scores;

    private static int HighScoresLength = 5;

    private string dataPath;


    [System.Serializable] class HighScoresData
    {
        public string[] names = new string[HighScoresLength];
        public string[] scores = new string[HighScoresLength];
    }



    private void Start()
    {
        dataPath = Application.persistentDataPath + "/savehighscoresfile.json"; //Move it to SaveHighScores, do it statis?

        LoadHighScores();
    }

    

    public void UpdateHighScores(string newName, int newScore)      //inGame Branch
    {
        for (int i = 0; i < 5; i++)
        {
            int score = int.Parse(scores[i].text);

            if (newScore > score) // or newScore < score ?
                continue;

            UpdateHighScores(names[i].text, score);

            names[i].text = newName;
            scores[i].text = newScore.ToString();
        }
    }
        


    public void SaveHighScores()
    {
        HighScoresData highScoresData = new HighScoresData();

        DataExchange(scores, highScoresData.scores, true);
        DataExchange(names, highScoresData.names, true);

        string json = JsonUtility.ToJson(highScoresData);

        File.WriteAllText(dataPath, json);
    }


    public void LoadHighScores()
    {
        if (File.Exists(dataPath))
        {
            string json = File.ReadAllText(dataPath);
            HighScoresData highScoresData = JsonUtility.FromJson<HighScoresData>(json);

            DataExchange(scores, highScoresData.scores, false);
            DataExchange(names, highScoresData.names, false);
        }
    }


    private void DataExchange(TextMeshProUGUI[] behaviourArray, string[] dataArray, bool saving)
    {
        for (int i = 0; i < HighScoresLength; i++)
        {
            if (saving)
                dataArray[i] = behaviourArray[i].text;
            else
                behaviourArray[i].text = dataArray[i];
        }
    }
}
