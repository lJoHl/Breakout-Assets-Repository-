using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ChangeKey : MonoBehaviour
{
    private enum Controls { MoveLeft, MoveRight, ThrowBall, Pause }
    [SerializeField] private Controls controls;
        
    private Button controlButton;

    private bool buttonSelected;


    private void Awake()
    {
        controlButton = GetComponent<Button>();

        controlButton.onClick.AddListener(Selected);
    }


    public void Selected()
    {
        buttonSelected = true;

        SetControlButtonText("");
    }


    private void OnGUI()
    {
        Event e = Event.current;

        if (buttonSelected)
            if (e.isKey)
            {
                buttonSelected = false;

                SetControlKey(e.keyCode);
            }
    }


    private void SetControlKey(KeyCode controlKey)
    {
        switch (controls)
        {
            case Controls.MoveLeft:
                ControlsSettings.MoveLeftKey = controlKey;
                break;

            case Controls.MoveRight:
                ControlsSettings.MoveRightKey = controlKey;
                break;

            case Controls.ThrowBall:
                ControlsSettings.ThrowBallKey = controlKey;
                break;

            case Controls.Pause:
                ControlsSettings.PauseKey = controlKey;
                break;
        }

        SetControlButtonText(KeyCodesDictionaries.AssignKeyName(controlKey));
    }

    public void SetControlButtonText()
    {
        KeyCode controlButtonKeyCode = KeyCode.None;

        switch (controls)
        {
            case Controls.MoveLeft:
                controlButtonKeyCode = ControlsSettings.MoveLeftKey;
                break;

            case Controls.MoveRight:
                controlButtonKeyCode = ControlsSettings.MoveRightKey;
                break;

            case Controls.ThrowBall:
                controlButtonKeyCode = ControlsSettings.ThrowBallKey;
                break;

            case Controls.Pause:
                controlButtonKeyCode = ControlsSettings.PauseKey;
                break;
        }

        SetControlButtonText(KeyCodesDictionaries.AssignKeyName(controlButtonKeyCode));
    }

    private void SetControlButtonText(string keyString)
    {
        controlButton.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = keyString;
    }
}
