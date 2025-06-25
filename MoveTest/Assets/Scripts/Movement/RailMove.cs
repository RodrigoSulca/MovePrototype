using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
public class RailMove : MonoBehaviour
{
    public GameObject[] rails;
    public float moveTime;
    public bool onHorse;
    public Image dmgPanel;
    public Color dmgColor;
    public bool armor;
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
        if (other.CompareTag("Enemy") && onHorse && !armor)
        {
            moveTime = 0.2f;
            dmgPanel.color = dmgColor;
            dmgPanel.DOFade(0, 0.3f).From(1).SetEase(Ease.OutQuad);
            onHorse = false;
            transform.DOKill();
            Destroy(other.gameObject);
        }
        else if (other.CompareTag("Enemy") && onHorse && armor)
        {
            Debug.Log("Armadura perdida");
            armor = false;
            dmgPanel.color = dmgColor;
            dmgPanel.DOFade(0, 0.3f).From(1).SetEase(Ease.OutQuad);
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
