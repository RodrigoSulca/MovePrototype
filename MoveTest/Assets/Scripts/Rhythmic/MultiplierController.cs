using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class MultiplierController : MonoBehaviour
{
    public int actualMult;
    public int cantNotes;
    public int totalPoints;
    public Slider multSlider;
    public Slider hpSlider;
    private int initCantNotes;
    public TMP_Text multiplierTxt;
    public TMP_Text pointsTxt;
    private AudioSource audioSource;
    private ComboRewards comboRewards;
    public NoteFeedback feedback;
    public BeatFlash beatFlash;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        comboRewards = GetComponent<ComboRewards>();
        initCantNotes = cantNotes;
        multSlider.maxValue = initCantNotes;
    }

    // Update is called once per frame
    void Update()
    {
        multSlider.value = comboRewards.actualCombo;
        CheckMult();
        multiplierTxt.text = $"x{actualMult}";
        pointsTxt.text = totalPoints.ToString();
        multSlider.maxValue = cantNotes;
    }

    void CheckMult()
    {
        if (comboRewards.actualCombo >= cantNotes && actualMult < 4)
        {
            multSlider.minValue = cantNotes;
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

        if (feedback != null)
                feedback.ShowFeedback(false);

        if (beatFlash != null)
                beatFlash.Flash(Color.red);
    }
}
