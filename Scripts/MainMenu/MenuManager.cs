using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    [SerializeField] private GameObject canvas;

    [SerializeField] private GameObject screenLock;


    public void ChangeScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }


    public void OpenMenu(GameObject menu)
    {
        Instantiate(screenLock, canvas.transform);
        Instantiate(menu, canvas.transform);
    }
}
