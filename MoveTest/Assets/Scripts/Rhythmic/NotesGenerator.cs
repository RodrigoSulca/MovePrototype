using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using DG.Tweening;
using System.Collections.Generic;
using FMOD.Studio;

public class NotesGenerator : MonoBehaviour
{
    public enum Instrument { I1, I2 }
    public Instrument instrument;
    public TextAsset[] charts;
    public Transform[] lines;
    public GameObject[] notePrefabs;

    public NotesList notesList;
    private HashSet<int> notasGeneradas = new();
    public float tiempoActual;
    public Image beatImg;
    public float beatInterval;
    public HitNotes hitNotes;
    private EventInstance musicEventInstance;

    void Start()
    {
        CargarCancion();
        StartCoroutine(Beat());
        musicEventInstance = AudioManager.instance.GetMusicEventInstance();
    }

    void Update()
    {
        Debug.Log("Handle: " + musicEventInstance.handle);
        if (Input.GetKeyDown(KeyCode.Space))
        {
            ChangeInstrument();
        }

        if (notesList == null || notesList.notes == null)
        {
            return;
        }

        int timelinePosition;
        musicEventInstance.getTimelinePosition(out timelinePosition);
        tiempoActual = timelinePosition / 1000f;

        for (int i = 0; i < notesList.notes.Length; i++)
        {
            if (!notasGeneradas.Contains(i) && notesList.notes[i].spawnTime <= tiempoActual)
            {
                GenerarNota(notesList.notes[i]);
                notasGeneradas.Add(i);
            }
        }
    }

    void CargarCancion()
    {
        notesList = JsonUtility.FromJson<NotesList>(charts[(int)instrument].text);
        notasGeneradas.Clear();
        if (notesList != null && notesList.notes != null)
        {
            Debug.Log("Notas cargadas: " + notesList.notes.Length);
        }
    }

    void GenerarNota(Note nota)
    {
        if (nota.line < 1 || nota.line > lines.Length)
        {
            Debug.LogWarning("Línea inválida: " + nota.line);
            return;
        }

        Transform posicionline = lines[nota.line - 1];
        Instantiate(notePrefabs[(int)instrument], posicionline.position, Quaternion.identity);
    }

    void ChangeInstrument()
    {
        instrument = (Instrument)(((int)instrument + 1) % System.Enum.GetValues(typeof(Instrument)).Length);
        hitNotes.defaultMaterial = hitNotes.materials[(int)instrument];
        hitNotes.mRenderer.material = hitNotes.defaultMaterial;
        CargarCancion();
        Debug.Log("Instrumento actual: " + instrument);
    }

    private IEnumerator Beat()
    {
        beatImg.DOFade(0, 0.3f);
        yield return new WaitForSeconds(beatInterval);
        beatImg.color = Color.white;
        StartCoroutine(Beat());
    }
}