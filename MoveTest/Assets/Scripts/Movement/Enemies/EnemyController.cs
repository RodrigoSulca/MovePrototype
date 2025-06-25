using UnityEngine;
using DG.Tweening;
using Unity.Mathematics;
public class EnemyController : MonoBehaviour
{
    public enum Type { Red, Blue, Green }
    public Type type;
    public float speed;
    [field: Header("BlueMove")]
    public float initMoveDistance;
    public float initSpeed;
    public float finalSpeed;
    private GameObject endPoint;
    private Rigidbody rb;
    void Start()
    {
        endPoint = GameObject.FindWithTag("EndPoint");
        rb = GetComponent<Rigidbody>();
        MoveDown();
    }

    void MoveDown()
    {
        switch (type)
        {
            case Type.Red:
                RedMove();
                break;
            case Type.Blue:
                BlueMove();
                break;
            case Type.Green:
                GreenMove();
                break;
        }

    }


    private void RedMove()
    {
        rb.DOMoveZ(endPoint.transform.position.z, speed).SetEase(Ease.Linear).OnComplete(() =>
        {
            Destroy(gameObject);
        });
    }
    private void BlueMove()
    {
        rb.DOMoveZ(transform.position.z+initMoveDistance, initSpeed).SetEase(Ease.Linear).OnComplete(() =>
        {
            rb.DOMoveZ(endPoint.transform.position.z, finalSpeed).SetEase(Ease.Linear).OnComplete(() =>
            {
                Destroy(gameObject);
            });
        });
    }

    private void GreenMove()
    {
        
    }
    

}
