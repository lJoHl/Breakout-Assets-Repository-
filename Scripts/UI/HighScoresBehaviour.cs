using System.IO;
using TMPro;
using UnityEngine;

public class HighScoresBehaviour : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI[] namesText;
    [SerializeField] private TextMeshProUGUI[] scoresText;

    private static string[] names = { "bee", "fox", "cat", "pig", "dog" };
    private static int[] scores = { 1476, 1208, 909, 803, 617 };

    private static readonly int highScoresLength = 5;

    private static string dataPath;


    [System.Serializable] class HighScoresData
    {
        public string[] names = new string[highScoresLength];
        public int[] scores = new int[highScoresLength];
    }


    private void Start()
    {
        dataPath = Application.persistentDataPath + "/savehighscoresfile.json";

        LoadHighScores();
    }


    public void UpdateHighScores(string newName, int newScore)
    { 
        for (int i = 0; i < highScoresLength; i++)
        {
            if (newScore <= scores[i])
                continue;

            UpdateHighScores(names[i], scores[i]);

            scores[i] = newScore;
            names[i] = newName;
            break;
        }
    }


    public void SaveHighScores()
    {
        HighScoresData highScoresData = new();

        DataExchange(scores, highScoresData.scores);
        DataExchange(names, highScoresData.names);

        string json = JsonUtility.ToJson(highScoresData);

        File.WriteAllText(dataPath, json);
    }

    public void LoadHighScores()
    {
        if (File.Exists(dataPath))
        {
            string json = File.ReadAllText(dataPath);
            HighScoresData highScoresData = JsonUtility.FromJson<HighScoresData>(json);

            DataExchange(highScoresData.scores, scores);
            DataExchange(highScoresData.names, names);
        }

        UpdateHighScoresText();
    }
    private void UpdateHighScoresText()
    {
        for (int i = 0; i < highScoresLength; i++)
        {
            namesText[i].text = names[i];
            scoresText[i].text = scores[i].ToString();
        }
    }


    private void DataExchange(string[] dataSender, string[] dataReceiver)
    {
        for (int i = 0; i < highScoresLength; i++)
            dataReceiver[i] = dataSender[i];
    }
    private void DataExchange(int[] dataSender, int[] dataReceiver)
    {
        for (int i = 0; i < highScoresLength; i++)
            dataReceiver[i] = dataSender[i];
    }


    public static bool IsItANewHighScore(int currentPoints)
    {
       return currentPoints > scores[^1];
    }
}