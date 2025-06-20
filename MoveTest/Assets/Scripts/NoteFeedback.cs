using UnityEngine;

public class NoteFeedback : MonoBehaviour
{
    public Material successMaterial;
    public Material failMaterial;
    public float duration = 0.3f;

    private Material originalMaterial;
    private MeshRenderer meshRenderer;

    void Start()
    {
        meshRenderer = GetComponent<MeshRenderer>();
        originalMaterial = meshRenderer.material;
    }

    public void ShowFeedback(bool isCorrect)
    {
        StopAllCoroutines();
        StartCoroutine(FlashMaterial(isCorrect ? successMaterial : failMaterial));
    }

    System.Collections.IEnumerator FlashMaterial(Material mat)
    {
        meshRenderer.material = mat;
        yield return new WaitForSeconds(duration);
        meshRenderer.material = originalMaterial;
    }
}
