using DG.Tweening;
using UnityEngine;
public class RailMove : MonoBehaviour
{
    public GameObject[] rails;
    public float moveTime;
    private int railIndex;
    private bool isMoving;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        railIndex++;
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
    }

    void Movement()
    {
        if (Input.GetKeyDown(KeyCode.A) && !isMoving && railIndex > 0)
        {
            railIndex--;
            isMoving = true;
            transform.DOMoveX(rails[railIndex].transform.position.x, moveTime).SetEase(Ease.Linear).OnComplete(() =>{
            isMoving = false;
            });
        }
        else if (Input.GetKeyDown(KeyCode.D) && !isMoving && railIndex < rails.Length - 1)
        {
            railIndex++;
            isMoving = true;
            transform.DOMoveX(rails[railIndex].transform.position.x, moveTime).SetEase(Ease.Linear).OnComplete(() =>
            {
                isMoving = false;
            });
        }
    }
}
