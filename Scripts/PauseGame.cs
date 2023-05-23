using TMPro;
using UnityEngine;

public class PauseGame : MonoBehaviour
{
    private MainManager mainManager;
    [SerializeField] private MenuManager menuManager;

    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private GameObject pressKeyText;

    private bool paused;
    private bool waitingKey;


    private void Start()
    {
        mainManager = GetComponent<MainManager>();
    }


    private void Update()
    {
        // Sets game state and time scale, based on the existence of the pauseMenu
        paused = GameObject.Find($"{pauseMenu.name}(Clone)");
        Time.timeScale = paused | (waitingKey & mainManager.m_Started) ? 0 : 1;

        // pressKeyText is activated when closing a previously open pauseMenu
        if (!paused & waitingKey)
            pressKeyText.SetActive(true);

        // Handles the pauseMenu state, based on the game state. When "pauseKey" is pressed
        if (Input.GetKeyDown(ControlsSettings.pauseKey) & !GameObject.Find("ControlsMenu" + "(Clone)"))
        {
            if (paused)
            {
                menuManager.CloseMenu();
            }
            else if (!mainManager.m_GameOver)
            {
                menuManager.OpenMenu(pauseMenu);
                waitingKey = true;
            }
        }


        // Updates the pressKeyText
        if (pressKeyText.activeInHierarchy)
        {
            string keyName = KeyCodesDictionary.AssignKeyName(ControlsSettings.throwBallKey);

            if (keyName.Length == 1) keyName = $"\"{keyName}\"";
            pressKeyText.GetComponent<TextMeshProUGUI>().text = $"Press {keyName}";
        }

        // Disables the pressKeyText
        if (Input.GetKeyDown(ControlsSettings.throwBallKey) & !paused)
        {
            pressKeyText.SetActive(false);
            waitingKey = false;
        }
    }
}