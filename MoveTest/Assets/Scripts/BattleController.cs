using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class BattleController : MonoBehaviour
{
    public Slider battleSlider;
    public GameObject gameOverPanel;
    public float enemyDmg;
    public NotesGenerator notesGenerator;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter(Collider collision)
    {
        EnemyController enemy = collision.gameObject.GetComponent<EnemyController>();
        Debug.Log("Colision");
        if (collision.gameObject.CompareTag("Enemy") && (int)enemy.type != (int)notesGenerator.instrument)
        {
            Debug.Log("Daño");
            battleSlider.value -= enemyDmg;
        }
    }

}
