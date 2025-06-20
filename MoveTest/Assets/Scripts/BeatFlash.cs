using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class BeatFlash : MonoBehaviour
{
    private Image img;

    void Awake()
    {
        img = GetComponent<Image>();
    }

    public void Flash(Color color)
    {
        img.color = color;
        img.DOFade(0, 0.3f).From(1).SetEase(Ease.OutQuad);
    }
}
