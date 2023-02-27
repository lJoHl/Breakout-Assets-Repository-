using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PauseGame : MonoBehaviour
{
    [SerializeField] private MenuManager menuManager;
    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private GameObject pressKeyText;

    private bool paused;
    private bool waitingKey;


    private void Update()
    {
        // set game state and time scale, based on the existence of the pauseMenu
        paused = GameObject.Find($"{pauseMenu.name}(Clone)");
        Time.timeScale = paused | waitingKey ? 0 : 1;

        // pressKeyText is activated when closing a previously open pauseMenu
        if (!paused & waitingKey)
            pressKeyText.SetActive(true);

        // handles the pauseMenu state, based on the game state. When "esc" is pressed
        if (Input.GetKeyDown(ControlsSettings.pauseKey) & !GameObject.Find("ControlsMenu" + "(Clone)"))
        {
            if (paused)
            {
                menuManager.CloseMenu();
            }
            else if (!GetComponent<MainManager>().m_GameOver)
            {
                menuManager.OpenMenu(pauseMenu);
                waitingKey = true;
            }
        }


        // update the pressKeyText
        if (pressKeyText.activeInHierarchy)
        {
            string keyName = KeyCodesDictionary.AssignKeyName(ControlsSettings.throwBallKey);

            if (keyName.Length == 1) keyName = $"\"{keyName}\"";
            pressKeyText.GetComponent<TextMeshProUGUI>().text = $"Press {keyName}";
        }

        // disables the pressKeyText
        if (Input.GetKeyDown(ControlsSettings.throwBallKey) & !paused)
        {
            pressKeyText.SetActive(false);
            waitingKey = false;
        }
    }
}
