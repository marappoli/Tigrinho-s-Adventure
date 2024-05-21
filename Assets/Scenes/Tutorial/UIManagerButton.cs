using UnityEngine;
using TMPro; // Importe isso se estiver usando TextMeshPro

public class UIManagerButton : MonoBehaviour
{
    public TextMeshProUGUI displayText; // Para TextMeshPro. Para UI padrão, use public Text displayText;

    public void UpdateText(string text)
    {
        displayText.text = text;
    }
}
