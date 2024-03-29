using UnityEngine;
using TMPro;

public class ComboBehaviour : MonoBehaviour
{
    [SerializeField] private GameObject combo;
    [SerializeField] private TextMeshProUGUI multiplierText;

    private int multiplier;
    public bool hasMultiplierReached2;


    private void UpdateCombo()
    {
        hasMultiplierReached2 = multiplier >= 2;

        combo.SetActive(hasMultiplierReached2);
        if (hasMultiplierReached2) multiplierText.text = $"x{multiplier}";
    }

    public void IncreaseMultiplier()
    {
        multiplier++;
        UpdateCombo();
    }

    public void BreakCombo()
    {
        multiplier = 0;
        UpdateCombo();
    }


    public int getMultiplier()
    {
        return multiplier;
    }
}