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

        button.onClick.AddListener(ModifierAction);
    }

    private void ModifierAction()
    {
        adjustableParameters.ModifyParameter(parameter, isAnIncrease);
    }
}