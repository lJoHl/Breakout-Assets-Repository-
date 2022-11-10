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
        if (Input.GetKeyDown(KeyCode.Escape))
            PauseGame();
    }


    public void InGame(bool value)
    {
        Time.timeScale = value ? 1 : 0;
    }


    public void PauseGame()
    {
        InGame(false);
        menuManager.OpenMenu(pauseMenu);
    }
}
