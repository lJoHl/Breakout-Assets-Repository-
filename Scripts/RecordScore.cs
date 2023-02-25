using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RecordScore : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI newScore;
    [SerializeField] private TMP_InputField newName;
    [SerializeField] private Button confirmRecord;

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

        confirmRecord.interactable = newName.text.Length == newName.characterLimit;
    }


    public void ConfirmRecord()
    {
        highScoresBehaviour.UpdateHighScores(newName.text, int.Parse(newScore.text));
        GameObject.Find(name).SetActive(false);
    }
}
