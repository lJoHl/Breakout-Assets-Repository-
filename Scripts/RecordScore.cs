using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RecordScore : MonoBehaviour
{
    private MainManager mainManager;
    [SerializeField] private HighScoresBehaviour highScoresBehaviour;

    [SerializeField] private TextMeshProUGUI newScore;
    [SerializeField] private TMP_InputField newName;
    [SerializeField] private Button confirmRecord;


    private void Start()
    {
        mainManager = GameObject.Find("MainManager").GetComponent<MainManager>();
        newScore.text = mainManager.scoreText.text;
    }

    // Controls access to interactive elements
    private void Update()
    {
        newName.Select();

        confirmRecord.interactable = newName.text.Length == newName.characterLimit;
    }


    public void ConfirmRecord()
    {
        highScoresBehaviour.UpdateHighScores(newName.text, int.Parse(newScore.text));

        gameObject.SetActive(false);
    }
}