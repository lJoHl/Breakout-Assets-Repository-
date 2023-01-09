using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ParameterModifier : MonoBehaviour
{
    private AdjustableParameters adjustableParameters;
    private Button button;

    [SerializeField] private string parameter;
    [SerializeField] private bool isAnIncrease;


    private void Start()
    {
        adjustableParameters = GameObject.Find("AdjustableParameters").GetComponent<AdjustableParameters>();
        button = GetComponent<Button>();

        button.onClick.AddListener(ModifierActions);
    }


    private void ModifierActions()
    {
        adjustableParameters.ChangeParameter(parameter, isAnIncrease);
    }
}
