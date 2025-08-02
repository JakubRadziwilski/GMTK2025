using TMPro;
using UnityEngine;

public class StatsDisplay : MonoBehaviour
{
    TextMeshProUGUI statText;
    Color32 positiveColor = new Color32(0, 255, 0, 255); // Green for positive changes
    Color32 negativeColor = new Color32(255, 0, 0, 255); // Red for negative changes
    Color32 neutralColor = new Color32(255, 255, 0, 255); // Yellow for neutral

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        statText = GetComponent<TextMeshProUGUI>();
        if (statText == null)
        {
            Debug.LogError("TextMeshProUGUI component not found on" + gameObject.name);
        }
    }

    public void UpdateStatDisplay(int value, int previousValue)
    {
        if (statText == null) return;

        statText.text = value.ToString();
        if (value > previousValue)
        {
            statText.color = positiveColor; // Green for positive changes
        }
        else if (value < previousValue)
        {
            statText.color = negativeColor; // Red for negative changes
        }
        else
        {
            statText.color = neutralColor; // Yellow for neutral changes
        }

        Invoke("GoBackToNeutralColor", 1f); // Reset color after 1 second

    }

    public void GoBackToNeutralColor()
    {
        if (statText != null)
        {
            statText.color = neutralColor; // Reset to neutral color
        }
    }
}
