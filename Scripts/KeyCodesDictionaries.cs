using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyCodesDictionaries : MonoBehaviour
{
    private static Dictionary<KeyCode, string> keyNames = new();

    private static void AddKeyNamesEntries()
    {
        keyNames.Add(KeyCode.Escape, "Esc");
        keyNames.Add(KeyCode.Delete, "Supr");
        keyNames.Add(KeyCode.Return, "Enter");
        
        keyNames.Add(KeyCode.Alpha1, "1");
        keyNames.Add(KeyCode.Alpha2, "2");
        keyNames.Add(KeyCode.Alpha3, "3");
        keyNames.Add(KeyCode.Alpha4, "4");
        keyNames.Add(KeyCode.Alpha5, "5");
        keyNames.Add(KeyCode.Alpha6, "6");
        keyNames.Add(KeyCode.Alpha7, "7");
        keyNames.Add(KeyCode.Alpha8, "8");
        keyNames.Add(KeyCode.Alpha9, "9");
        keyNames.Add(KeyCode.Alpha0, "0");
        
        keyNames.Add(KeyCode.LeftArrow, "Left");
        keyNames.Add(KeyCode.RightArrow, "Right");
        keyNames.Add(KeyCode.UpArrow, "Up");
        keyNames.Add(KeyCode.DownArrow, "Down");
        
        keyNames.Add(KeyCode.LeftBracket, "LBracket");
        keyNames.Add(KeyCode.RightBracket, "RBracket");
        keyNames.Add(KeyCode.LeftShift, "LShift");
        keyNames.Add(KeyCode.RightShift, "RShift");
        keyNames.Add(KeyCode.LeftControl, "LCtrl");
        keyNames.Add(KeyCode.RightControl, "RCtrl");
        keyNames.Add(KeyCode.LeftAlt, "LAlt");
        keyNames.Add(KeyCode.RightAlt, "RAlt");
        keyNames.Add(KeyCode.LeftApple, "LCommand");
        keyNames.Add(KeyCode.RightApple, "RCommand");
        keyNames.Add(KeyCode.LeftWindows, "LStart");
        keyNames.Add(KeyCode.RightWindows, "RStart");

        keyNames.Add(KeyCode.KeypadDivide, "/");
        keyNames.Add(KeyCode.KeypadMultiply, "*");
        keyNames.Add(KeyCode.KeypadMinus, "-");
        keyNames.Add(KeyCode.KeypadPlus, "+");
        keyNames.Add(KeyCode.KeypadEnter, "Intro");
        keyNames.Add(KeyCode.KeypadPeriod, ".");
        keyNames.Add(KeyCode.KeypadEquals, "=");
    }


    public static string AssignKeyName(KeyCode keyCode)
    {
        AddKeyNamesEntries();
        string keyName = keyCode.ToString();

        foreach (KeyCode key in keyNames.Keys)
        {
            if (key == keyCode)
                keyName = keyNames.GetValueOrDefault(key);
        }

        keyNames.Clear();
        return keyName;
    }
}
