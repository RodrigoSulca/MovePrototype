using UnityEngine;

public class NotesGenerator : MonoBehaviour
{
    public enum Instrument { I1, I2, I3, I4 }
    public Instrument instrument;
    public TextAsset[] charts;
    public Transform[] lines;
    public GameObject[] notePrefabs;

    public NotesList notesList;
    public float moveTime;
    public int indiceNotaActual = 0;
    private float tiempoInicio;

    void Start()
    {
        CargarCancion();
        tiempoInicio = Time.time;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            ChangeInstrument();
        }

        if (notesList == null || indiceNotaActual >= notesList.notes.Length)
        {
            return;
        }

        float tiempoActual = Time.time - tiempoInicio;

        while (indiceNotaActual < notesList.notes.Length && notesList.notes[indiceNotaActual].spawnTime <= tiempoActual)
        {
            GenerarNota(notesList.notes[indiceNotaActual]);
            indiceNotaActual++;
        }

        if (indiceNotaActual >= notesList.notes.Length)
        {
            indiceNotaActual = 0;
            tiempoInicio = Time.time;
            Debug.Log("Reiniciando chart...");
        }
    }

    void CargarCancion()
    {
        notesList = JsonUtility.FromJson<NotesList>(charts[(int)instrument].text);
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
        Instantiate(notePrefabs[nota.line - 1], posicionline.position, Quaternion.identity);
    }
    
    void ChangeInstrument()
    {
        instrument = (Instrument)(((int)instrument + 1) % System.Enum.GetValues(typeof(Instrument)).Length);
        CargarCancion();
        Debug.Log("Instrumento actual: " + instrument);
    }

}