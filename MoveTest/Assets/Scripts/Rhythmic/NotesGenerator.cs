using UnityEngine;

public class NotesGenerator : MonoBehaviour
{
    public TextAsset chart;
    public Transform[] lines;
    public GameObject[] notePrefabs;

    public NotesList notesList;
    public float moveTime;
    private int indiceNotaActual = 0;
    private float tiempoInicio;

    void Start()
    {
        CargarCancion();
        tiempoInicio = Time.time;
    }

    void Update()
    {
        if (notesList == null || indiceNotaActual >= notesList.notes.Length){
            return;
        }

        float tiempoActual = Time.time - tiempoInicio;

        while (indiceNotaActual < notesList.notes.Length && notesList.notes[indiceNotaActual].spawnTime <= tiempoActual)
        {
            GenerarNota(notesList.notes[indiceNotaActual]);
            indiceNotaActual++;
        }

         if (indiceNotaActual >= notesList.notes.Length){
        indiceNotaActual = 0;
        tiempoInicio = Time.time;
        Debug.Log("Reiniciando chart...");
    }
    }

    void CargarCancion()
    {
        if (chart != null)
        {
            notesList = JsonUtility.FromJson<NotesList>(chart.text);
            if (notesList != null && notesList.notes != null)
            {
                Debug.Log("Notas cargadas: " + notesList.notes.Length);
            }
        }
        else
        {
            Debug.LogError("No se encontró el archivo JSON: ");
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
}
