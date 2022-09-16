using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    private GameObject canvas;

    [SerializeField] private GameObject screenLock;


    private void AssingCanvas()
    {
        canvas = GameObject.Find("Canvas");
    }


    public void ChangeScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }


    public void OpenMenu(GameObject menu)
    {
        AssingCanvas();

        Instantiate(screenLock, canvas.transform);
        Instantiate(menu, canvas.transform);
    }


    public void CloseMenu()
    {
        AssingCanvas();

        foreach (Transform child in canvas.transform)
            if(child.gameObject.tag == "MenuElement")
                Destroy(child.gameObject);
    }
}
