using TMPro;
using UnityEngine;

public class FloatingText : MonoBehaviour
{
    public float duration = 0.6f;
    public float floatSpeed = 1f;
    public float scaleAmount = 1.3f;
    public float fadeSpeed = 2f;
    public float rotationSpeed = 30f;

    private TMP_Text text;
    private Color originalColor;
    private Vector3 originalScale;

    void Start()
    {
        text = GetComponent<TMP_Text>();
        originalColor = text.color;
        originalScale = transform.localScale;
        StartCoroutine(AnimateText());
    }

    System.Collections.IEnumerator AnimateText()
    {
        float t = 0f;
        while (t < duration)
        {
            // Flotar hacia arriba
            transform.position += Vector3.up * floatSpeed * Time.deltaTime;

            // Rotar suavemente
            transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime);

            // Efecto rebote suave (con Mathf.Sin)
            float scale = Mathf.Lerp(1f, scaleAmount, Mathf.Sin(t * Mathf.PI));
            transform.localScale = originalScale * scale;

            // Desvanecer
            Color c = text.color;
            c.a = Mathf.Lerp(originalColor.a, 0, t / duration);
            text.color = c;

            t += Time.deltaTime;
            yield return null;
        }

        Destroy(gameObject);
    }
}
