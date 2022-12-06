using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class RecordScore : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI newScore;
    [SerializeField] private TMP_InputField newName;

    private MainManager mainManager;
    [SerializeField] private HighScoresBehaviour highScoresBehaviour;


    private void Start()
    {
        mainManager = GameObject.Find("MainManager").GetComponent<MainManager>();

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
