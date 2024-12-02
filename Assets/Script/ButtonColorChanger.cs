using UnityEngine;
using UnityEngine.UI;

public class ButtonColorChanger : MonoBehaviour
{
    public Button button; // Il bottone che cambierà colore
    public float duration = 2.0f; // La durata del ciclo completo del cambio colore in secondi

    private Image buttonImage;
    private Color startColor = Color.grey;
    private Color endColor = Color.white;

    void Start()
    {
        if (button == null)
        {
            button = GetComponent<Button>();
        }

        if (button != null)
        {
            buttonImage = button.GetComponent<Image>();
            buttonImage.color = startColor;
        }
    }

    void Update()
    {
        if (buttonImage != null)
        {
            float t = Mathf.PingPong(Time.time / duration, 1.0f);
            buttonImage.color = Color.Lerp(startColor, endColor, t);
        }
    }
}