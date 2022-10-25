using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class ControlsSettings : MonoBehaviour
{
    public static KeyCode MoveLeftKey { get; set; }
    public static KeyCode MoveRightKey { get; set; }
    public static KeyCode ThrowBallKey { get; set; }
    public static KeyCode PauseKey { get; set; }

    private const KeyCode MoveLeftDefaultKey = KeyCode.A;
    private const KeyCode MoveRightDefaultKey = KeyCode.D;
    private const KeyCode ThrowBallDefaultKey = KeyCode.Space;
    private const KeyCode PauseDefaultKey = KeyCode.Escape;

    private string dataPath;


    [System.Serializable] class ControlsData
    {
        public KeyCode moveLeftKey;
        public KeyCode moveRightKey;
        public KeyCode throwBallKey;
        public KeyCode pauseKey;
    }



    private void Start()
    {
        dataPath = Application.persistentDataPath + "/savecontrolsfile.json";

        LoadControls();
    }


    public void SaveControls()
    {
        ControlsData controlsData = new ControlsData();

        controlsData.moveLeftKey = MoveLeftKey;
        controlsData.moveRightKey = MoveRightKey;
        controlsData.throwBallKey = ThrowBallKey;
        controlsData.pauseKey = PauseKey;

        string json = JsonUtility.ToJson(controlsData);

        File.WriteAllText(dataPath, json);
    }

    public void LoadControls()
    {
        if (File.Exists(dataPath))
        {
            string json = File.ReadAllText(dataPath);
            ControlsData controlsData = JsonUtility.FromJson<ControlsData>(json);

            MoveLeftKey = controlsData.moveLeftKey;
            MoveRightKey = controlsData.moveRightKey;
            ThrowBallKey = controlsData.throwBallKey;
            PauseKey = controlsData.pauseKey;
        }
        else
        {
            SetDefaultKeys();
        }

        MatchButtonsToKeys();
    }


    public void SetDefaultKeys()
    {
        MoveLeftKey = MoveLeftDefaultKey;
        MoveRightKey = MoveRightDefaultKey;
        ThrowBallKey = ThrowBallDefaultKey;
        PauseKey = PauseDefaultKey;
    }

    
    public void MatchButtonsToKeys()
    {
        ChangeKey[] foundChangeKeyObjects = FindObjectsOfType<ChangeKey>();

        foreach (ChangeKey changeKeyObject in foundChangeKeyObjects)
            changeKeyObject.SetControlButtonText();
    }
}
