using UnityEngine;
using DG.Tweening;
public class EnemyController : MonoBehaviour
{
    public float speed;
    private GameObject endPoint;
    private Rigidbody rb;
    void Start()
    {
        endPoint = GameObject.FindWithTag("EndPoint");
        MoveDown();
    }

    void MoveDown()
    {
        rb = GetComponent<Rigidbody>();
        rb.DOMoveZ(endPoint.transform.position.z, speed).SetEase(Ease.Linear).OnComplete(() =>
        {
            if (gameObject.CompareTag("Enemy"))
            Destroy(gameObject);
        });
    }
}
