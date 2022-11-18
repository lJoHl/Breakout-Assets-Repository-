using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameState : MonoBehaviour
{
    [SerializeField] private MenuManager menuManager;
    [SerializeField] private GameObject pauseMenu;

    private bool inGame;
    private bool gameOver;


    private void Update()
    {
        // set game state and time scale, based on the existence of the pauseMenu
        inGame = !GameObject.Find($"{pauseMenu.name}(Clone)");
        Time.timeScale = inGame ? 1 : 0;


        // handles the pauseMenu state, based on the game state. When "esc" is pressed
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (inGame)
                menuManager.OpenMenu(pauseMenu);
            else
                menuManager.CloseMenu();
        }
    }
}
