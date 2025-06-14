using UnityEngine;
using System.Collections;

public class ComboRewards : MonoBehaviour
{
    public int comboNeeded;
    public GameObject player;
    public float invulnerableTime; 
    public Material invulnerableM;
    public int actualCombo;
    private int initComboN;
    private Collider playerColl;
    private Renderer playerRenderer;
    private Material normalMaterial;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerColl = player.GetComponent<Collider>();
        playerRenderer = player.GetComponent<Renderer>();
        initComboN = comboNeeded;
        normalMaterial = playerRenderer.material;
    }

    // Update is called once per frame
    void Update()
    {
        if(actualCombo >= comboNeeded){
            Reward();
        }
    }


    private void Reward(){
        comboNeeded +=initComboN;
        int rewardId = Random.Range(0,3);
        switch(rewardId){
            case 0:
                StartCoroutine(Invulnerable());
                break;
            case 1:
                Debug.Log("Reward");
                break;
            case 2:
                HorseArmor();
                break;
            
        }
    }

    private IEnumerator Invulnerable(){
        playerColl.isTrigger = true;
        playerRenderer.material = invulnerableM;
        yield return new WaitForSeconds(invulnerableTime);
        playerColl.isTrigger = false;
        playerRenderer.material = normalMaterial;
    }

    private void HorseArmor(){
        Debug.Log("Horse Armor");
    }
}
