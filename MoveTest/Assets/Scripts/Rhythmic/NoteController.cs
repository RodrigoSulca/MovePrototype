using UnityEngine;
using DG.Tweening;
public class NoteController : MonoBehaviour
{
    public float speed;
    private GameObject endPoint;
    private Rigidbody rb;
    void Start()
    {
        endPoint = GameObject.FindWithTag("EndPoint");
        rb = GetComponent<Rigidbody>();
        rb.DOMoveZ(endPoint.transform.position.z, speed).SetEase(Ease.Linear).OnComplete(() =>
        {
            Destroy(gameObject);
        });
    }
}
