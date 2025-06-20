using TMPro;
using UnityEngine;
public class MultiplierController : MonoBehaviour
{
    public int actualMult;
    public int cantNotes;
    public int totalPoints;
    private int initCantNotes;
    public TMP_Text multiplierTxt;
    public TMP_Text pointsTxt;
    private AudioSource audioSource;
    private ComboRewards comboRewards;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        comboRewards = GetComponent<ComboRewards>();
        initCantNotes = cantNotes;
    }

    // Update is called once per frame
    void Update()
    {
        CheckMult();
        multiplierTxt.text = $"x{actualMult}";
        pointsTxt.text = totalPoints.ToString();
    }

    void CheckMult()
    {
        if (comboRewards.actualCombo >= cantNotes && actualMult < 4)
        {
            cantNotes += initCantNotes;
            actualMult++;
        }
    }

    public void FailNote()
    {
        comboRewards.ResetCombo();
        actualMult = 1;
        cantNotes = initCantNotes;
        AudioManager.instance.PlayOneShot(FMODEvents.instance.noteFailed, this.transform.position);
    }
}
