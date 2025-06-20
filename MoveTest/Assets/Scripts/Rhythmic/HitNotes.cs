using TMPro;
using UnityEngine;

public class HitNotes : MonoBehaviour
{
    public KeyCode inputKey;
    public Material activeMaterial;
    public ComboRewards comboRewards;

    public Material[] materials;
    public GameObject textPrefab;
    public Transform textSpawner;
    private bool active;
    [HideInInspector] public Renderer mRenderer;
    [HideInInspector] public Material defaultMaterial;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        mRenderer = GetComponent<Renderer>();
        defaultMaterial = mRenderer.material;
    }

    void Update()
    {
        if (Input.GetKeyDown(inputKey))
        {
            mRenderer.material = activeMaterial;
            active = true;
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
            NoteText("Hit!");
        }
    }

    public void NoteText(string text)
    {
        GameObject noteTxt = Instantiate(textPrefab, textSpawner.position, textPrefab.transform.rotation, gameObject.transform);
        noteTxt.GetComponent<TMP_Text>().text = text;
        Destroy(noteTxt, 0.5f);
    }
}
