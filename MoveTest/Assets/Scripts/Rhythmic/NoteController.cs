using UnityEngine;
using DG.Tweening;
public class NoteController : MonoBehaviour
{
    public float speed;
    public int points;
    private GameObject endPoint;
    private MultiplierController multiplierController;
    private Rigidbody rb;
    void Start()
    {
        endPoint = GameObject.FindWithTag("EndPoint");
        multiplierController = GameObject.FindWithTag("MultiplierM").GetComponent<MultiplierController>();
        MoveDown();
    }

    void MoveDown()
    {
        rb = GetComponent<Rigidbody>();
        rb.DOMoveZ(endPoint.transform.position.z, speed).SetEase(Ease.Linear).OnComplete(() =>
        {
            if (gameObject.CompareTag("Note"))
            {
                multiplierController.FailNote();
            }
            Destroy(gameObject);
        });
    }

    public void PlayNote()
    {
        int finalPoints = points * multiplierController.actualMult;
        multiplierController.totalPoints += finalPoints;
        rb.DOKill();
        Destroy(gameObject);
    }
}
