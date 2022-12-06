using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class RecordScore : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI newScore;

    [SerializeField] private TMP_InputField newName;

    [SerializeField] private HighScoresBehaviour highScoresBehaviour;
    [SerializeField] private MainManager mainManager;


    private void Start()
    {
        newScore.text = mainManager.scoreText.text;
    }

    private void Update()
    {
        newName.Select();
    }


    public void ConfirmRecord()
    {
        highScoresBehaviour.UpdateHighScores(newName.text, int.Parse(newScore.text));
    }
}
