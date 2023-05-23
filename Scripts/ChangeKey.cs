using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ChangeKey : MonoBehaviour
{
    private enum Controls { MoveLeft, MoveRight, ThrowBall, Pause }
    [SerializeField] private Controls controls;
        
    private Button controlButton;

    private bool buttonSelected;
    private static bool interactionLock;


    private void Awake()
    {
        controlButton = GetComponent<Button>();
        controlButton.onClick.AddListener(Selected);
    }

    private void Start()
    {
        interactionLock = false;
        buttonSelected = false;
    }

    // Prevents more than one button from being selected at the same time
    private void Update()
    {
        if (interactionLock)
        {
            if (!buttonSelected)
                controlButton.interactable = false;
        }
        else controlButton.interactable = true;
    }


    public void Selected()
    {
        buttonSelected = true;
        interactionLock = true;

        SetControlButtonText("");
    }

    // Assigns the pressed key to the selected button
    private void OnGUI()
    {
        Event e = Event.current;

        if (buttonSelected)
            if (e.isKey)
            {
                buttonSelected = false;
                interactionLock = false;

                SetControlKey(e.keyCode);
            }
    }

    private void SetControlKey(KeyCode controlKey)
    {
        switch (controls)
        {
            case Controls.MoveLeft:
                ControlsSettings.ReassingKey(ControlsSettings.moveLeftKey, controlKey);
                break;

            case Controls.MoveRight:
                ControlsSettings.ReassingKey(ControlsSettings.moveRightKey, controlKey);
                break;

            case Controls.ThrowBall:
                ControlsSettings.ReassingKey(ControlsSettings.throwBallKey, controlKey);
                break;

            case Controls.Pause:
                ControlsSettings.ReassingKey(ControlsSettings.pauseKey, controlKey);
                break;
        }

        ControlsSettings.SaveControls();
    }

    public void SetControlButtonText(string keyString)
    {
        controlButton.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = keyString;
    }


    public void DeselectAll()
    {
        interactionLock = false;

        foreach (ChangeKey controlButton in FindObjectsOfType<ChangeKey>())
            controlButton.buttonSelected = false;
    }
}