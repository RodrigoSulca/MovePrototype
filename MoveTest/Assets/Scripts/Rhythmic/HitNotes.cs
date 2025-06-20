using TMPro;
using UnityEngine;

public class HitNotes : MonoBehaviour
{
    public KeyCode inputKey;
    public Material activeMaterial;
    public ComboRewards comboRewards;
    public NoteFeedback feedback;
    public BeatFlash beatFlash;

    public GameObject textPrefab;      // Texto 3D opcional
    public Transform textSpawner;      // Punto para texto 3D
    public GameObject textPrefabUI;    // Prefab UI con FloatingUIText
    public Canvas uiCanvas;            // Canvas principal

    private bool active;
    [HideInInspector] public Renderer mRenderer;
    [HideInInspector] public Material defaultMaterial;

    void Start()
    {
        mRenderer = GetComponent<Renderer>();
        defaultMaterial = mRenderer.material;
        ShowUIText("Test visible", Color.red);
    }

    void Update()
    {
        if (Input.GetKeyDown(inputKey))
        {
            mRenderer.material = activeMaterial;
            active = true;

            // Asumir fallo por defecto (Miss)
            ShowUIText("Miss!", Color.red);

            if (feedback != null)
                feedback.ShowFeedback(false);

            if (beatFlash != null)
                beatFlash.Flash(Color.red);
        }
        else if (Input.GetKeyUp(inputKey))
        {
            mRenderer.material = defaultMaterial;
            active = false;
        }
    }

    void OnTriggerStay(Collider other)
    {
        if (active && other.CompareTag("Note"))
        {
            comboRewards.actualCombo++;
            other.GetComponent<NoteController>().PlayNote();

            ShowUIText("Perfect!", Color.cyan);

            if (feedback != null)
                feedback.ShowFeedback(true);

            if (beatFlash != null)
                beatFlash.Flash(Color.cyan);

            mRenderer.material = defaultMaterial;
            active = false;
        }
    }

    // Texto flotante (UI Canvas)
    void ShowUIText(string content, Color color)
    {
        GameObject uiText = Instantiate(textPrefabUI, uiCanvas.transform);
        
        // Posicionarlo en el centro del Canvas
        RectTransform rect = uiText.GetComponent<RectTransform>();
        rect.anchoredPosition = Vector2.zero;

        // Cambiar el texto y color
        var textComponent = uiText.GetComponent<TMPro.TextMeshProUGUI>();
        if (textComponent != null)
        {
            textComponent.text = content;
            textComponent.color = color;
        }
        else
        {
            Debug.LogError("El prefab no tiene TextMeshProUGUI");
        }

        // Destruir después de 1 segundo (opcional)
        Destroy(uiText, 1f);
    }



    // Texto 3D opcional (si usas uno)
    public void NoteText(string text)
    {
        if (textPrefab != null && textSpawner != null)
        {
            GameObject noteTxt = Instantiate(textPrefab, textSpawner.position, textPrefab.transform.rotation, transform);
            noteTxt.GetComponent<TMP_Text>().text = text;
            Destroy(noteTxt, 0.5f);
        }
    }
}
