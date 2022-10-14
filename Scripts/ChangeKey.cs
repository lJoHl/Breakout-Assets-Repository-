using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ChangeKey : MonoBehaviour
{
    private Button button;

    private bool buttonSelected;


    private void Start()
    {
        button = GetComponent<Button>();

        button.onClick.AddListener(Selected);
    }


    public void Selected()
    {
        buttonSelected = true;

        button.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = ""; 
    }


    public void OnGUI()
    {
        Event e = Event.current;

        if (buttonSelected)
            if (e.isKey)
            {
                button.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = e.keyCode.ToString();

                buttonSelected = false;
            }
    }
}
