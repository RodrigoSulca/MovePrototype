using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using System.Collections;

public class Timer : MonoBehaviour
{
    public Color flashColor;
    public float interval;
    private Image img;

    void Awake()
    {
        img = GetComponent<Image>();
    }
    void Start()
    {
        StartCoroutine(FlashCoroutime());
    }

    private IEnumerator FlashCoroutime()
    {
        img.color = flashColor;
        img.DOFade(0, 0.3f).From(1).SetEase(Ease.OutQuad);
        yield return new WaitForSeconds(interval);
        StartCoroutine(FlashCoroutime());
    }
}
