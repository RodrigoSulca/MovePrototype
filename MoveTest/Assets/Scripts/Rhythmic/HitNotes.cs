using UnityEngine;

public class HitNotes : MonoBehaviour
{
    public KeyCode inputKey;
    public Material activeMaterial;
    private bool active;
    private Renderer mRenderer;
    private Material defaultMaterial;
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
            Debug.Log("Destroy Note");
            Destroy(other.gameObject);
        }
    }
}
