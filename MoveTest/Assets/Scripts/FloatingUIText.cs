using TMPro;
using UnityEngine;

public class FloatingUIText : MonoBehaviour
{
    public TMP_Text text;
    public float lifetime = 1.0f;
    public float floatSpeed = 50f;

    private RectTransform rect;

    void Start()
    {
        rect = GetComponent<RectTransform>();
        Destroy(gameObject, lifetime);
    }

    void Update()
    {
        rect.anchoredPosition += Vector2.up * floatSpeed * Time.deltaTime;
    }

    public void SetText(string content, Color color)
    {
        text.text = content;
        text.color = color;
    }
}
