using UnityEngine;

public class NotesGenerator : MonoBehaviour
{
    public TextAsset chart; // Nombre del archivo JSON en Resources (sin extensión)
    public GameObject prefabNota; // Prefab para generar las notes
    //public GameObject playerInput;
    public Transform[] lines; // Array de posiciones de las líneas

    public NotesList notesList;
    public float moveTime;
    private int indiceNotaActual = 0;
    private float tiempoInicio;

    void Start()
    {
        // Cargar las notes desde el archivo JSON en Resources
        CargarCancion();
        tiempoInicio = Time.time;
    }

    void Update()
    {
        if (notesList == null || indiceNotaActual >= notesList.notes.Length){
            return;
        }
        // Calcular el tiempo transcurrido desde el inicio
        float tiempoActual = Time.time - tiempoInicio;

        // Generar notes en el tiempo correspondiente
        while (indiceNotaActual < notesList.notes.Length && notesList.notes[indiceNotaActual].spawnTime - moveTime <= tiempoActual)
        {
            GenerarNota(notesList.notes[indiceNotaActual]);
            indiceNotaActual++;
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

        // Crear la nota en la posición de la línea correspondiente
        Transform posicionline = lines[nota.line - 1];
        Instantiate(prefabNota, posicionline.position, Quaternion.identity);
    }
}
