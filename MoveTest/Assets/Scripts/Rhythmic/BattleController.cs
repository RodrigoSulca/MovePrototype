using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class BattleController : MonoBehaviour
{
    public Slider battleSlider;
    public GameObject gameOverPanel;
    public float battlePoints;
    public float enemyDmg;
    public float dmgInterval;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartCoroutine(EnemyPoints());
    }

    // Update is called once per frame
    void Update()
    {
        battleSlider.value = battlePoints;
        if (battleSlider.value <= battleSlider.minValue)
        {

        }
    }
    private IEnumerator EnemyPoints()
    {
        yield return new WaitForSeconds(dmgInterval);
        battlePoints -= enemyDmg;
        StartCoroutine(EnemyPoints());
    }
}
