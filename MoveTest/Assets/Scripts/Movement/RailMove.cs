using DG.Tweening;
using UnityEngine;
public class RailMove : MonoBehaviour
{
    public GameObject[] rails;
    public float moveTime;
    public bool onHorse;
    public GameObject dmgPanel;
    private int railIndex;
    private bool isMoving;
    private Rigidbody rb;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody>();
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
            rb.DOMoveX(rails[railIndex].transform.position.x, moveTime).SetEase(Ease.Linear).OnComplete(() =>
            {
                isMoving = false;
            });
        }
        else if (Input.GetKeyDown(KeyCode.D) && !isMoving && railIndex < rails.Length - 1)
        {
            railIndex++;
            isMoving = true;
            rb.DOMoveX(rails[railIndex].transform.position.x, moveTime).SetEase(Ease.Linear).OnComplete(() =>
            {
                isMoving = false;
            });
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy") && onHorse)
        {
            moveTime = 0.5f;
            dmgPanel.SetActive(true);
            onHorse = false;
            transform.DOKill();
            Destroy(other.gameObject);
        }
        else if (other.CompareTag("Enemy") && !onHorse)
        {
            Destroy(other.gameObject);
            Time.timeScale = 0;
        }
    }
}
