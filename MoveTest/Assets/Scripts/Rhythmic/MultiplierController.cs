using TMPro;
using UnityEngine;

public class MultiplierController : MonoBehaviour
{
    public int actualMult;
    public int cantNotes;
    public int consecNotes;
    public int totalPoints;
    public TMP_Text multiplierTxt;
    public TMP_Text pointsTxt;
    private AudioSource audioSource;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
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
        if (consecNotes >= cantNotes && actualMult < 4)
        {
            consecNotes = 0;
            actualMult++;
        }
    }

    public void FailNote()
    {
        consecNotes = 0;
        actualMult = 1;
        audioSource.Play();
    }
}
