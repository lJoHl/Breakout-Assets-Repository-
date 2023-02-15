using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class ControlsSettings : MonoBehaviour
{
    public static KeyCode[] controls = new KeyCode[4];
    private static readonly KeyCode[] defaultControls = { KeyCode.A, KeyCode.D, KeyCode.Space, KeyCode.Escape };

    public static KeyCode moveLeftKey;
    public static KeyCode moveRightKey;
    public static KeyCode throwBallKey;
    public static KeyCode pauseKey;

    private static string dataPath;


    [System.Serializable] class ControlsData
    {
        public KeyCode moveLeftKey;
        public KeyCode moveRightKey;
        public KeyCode throwBallKey;
        public KeyCode pauseKey;
    }



    private void Start()
    {
        LoadControls();
    }


    public static void SaveControls()
    {
        dataPath = Application.persistentDataPath + "/savecontrolsfile.json";

        ControlsData controlsData = new();

        controlsData.moveLeftKey = moveLeftKey;
        controlsData.moveRightKey = moveRightKey;
        controlsData.throwBallKey = throwBallKey;
        controlsData.pauseKey = pauseKey;

        string json = JsonUtility.ToJson(controlsData);
        File.WriteAllText(dataPath, json);
    }

    public static void LoadControls()
    {
        if (File.Exists(dataPath))
        {
            string json = File.ReadAllText(dataPath);
            ControlsData controlsData = JsonUtility.FromJson<ControlsData>(json);

            moveLeftKey = controlsData.moveLeftKey;
            moveRightKey = controlsData.moveRightKey;
            throwBallKey = controlsData.throwBallKey;
            pauseKey = controlsData.pauseKey;

            SetControls();
        }
        else SetDefaultKeys();

        MatchButtonsToKeys();
    }


    public static void SetDefaultKeys()
    {
        for (int i = 0; i < controls.Length; i++)
            controls[i] = defaultControls[i];

        SetKeys();
    }

    private static void SetKeys() //put this code in SetDefaultKeys?
    {
        moveLeftKey = controls[0];
        moveRightKey = controls[1];
        throwBallKey = controls[2];
        pauseKey = controls[3];
    }

    private static void SetControls()
    {
        controls[0] = moveLeftKey;
        controls[1] = moveRightKey;
        controls[2] = throwBallKey;
        controls[3] = pauseKey;
    }

    
    public static void MatchButtonsToKeys()
    {
        ChangeKey[] changeKeyObjects = FindObjectsOfType<ChangeKey>();

        for (int i = changeKeyObjects.Length - 1, j = 0; i >= 0; i--, j++)
            changeKeyObjects[i].SetControlButtonText(KeyCodesDictionary.AssignKeyName(controls[j]));
    }



    public static void ReassingKey(KeyCode control, KeyCode controlKey)
    {
        for(int i = 0; i < controls.Length; i++)
        {
            if (control == controls[i])
            {
                controls[i] = controlKey;
            }
            else if (controlKey == controls[i])
            {
                controls[i] = control;
            }
        }

        SetKeys();
        MatchButtonsToKeys();
    }
}
