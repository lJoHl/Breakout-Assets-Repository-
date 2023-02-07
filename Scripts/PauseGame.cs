using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseGame : MonoBehaviour
{
    [SerializeField] private MenuManager menuManager;
    [SerializeField] private GameObject pauseMenu;

    private bool paused;


    private void Update()
    {
        // set game state and time scale, based on the existence of the pauseMenu
        paused = GameObject.Find($"{pauseMenu.name}(Clone)");
        Time.timeScale = paused ? 0 : 1;


        // handles the pauseMenu state, based on the game state. When "esc" is pressed
        if (Input.GetKeyDown(ControlsSettings.pauseKey) & !GameObject.Find("ControlsMenu" + "(Clone)"))
        {
            if (paused)
                menuManager.CloseMenu();
            else
                menuManager.OpenMenu(pauseMenu);
        }
    }
}
