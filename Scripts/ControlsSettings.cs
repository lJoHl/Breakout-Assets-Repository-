using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ControlsSettings : MonoBehaviour
{
    public TextMeshProUGUI buttontext;

    private bool buttonSelected;



    public void Selected()
    {
        buttonSelected = true;

        buttontext.text = "";
    }


    public void OnGUI()
    {
        Event e = Event.current;

        if (buttonSelected)
            if (e.isKey)
            {
                buttontext.text = e.keyCode.ToString();

                buttonSelected = false;
            }
    }
}
