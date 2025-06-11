using UnityEngine;

public class EnemiesGenerator : MonoBehaviour
{
    public TextAsset chart;
    public Transform[] rows;
    public GameObject[] enemyPrefabs;

    public EnemiesList enemiesList;
    public int enemyTypeId;
    private int enemyIndex = 0;
    private float tiempoInicio;

    void Start()
    {
        CargarCancion();
        tiempoInicio = Time.time;
    }

    void Update()
    {
        if (enemiesList == null || enemyIndex >= enemiesList.enemies.Length){
            return;
        }

        float tiempoActual = Time.time - tiempoInicio;

        while (enemyIndex < enemiesList.enemies.Length && enemiesList.enemies[enemyIndex].spawnTime <= tiempoActual)
        {
            GenerarEnemigo(enemiesList.enemies[enemyIndex]);
            enemyIndex++;
        }

         if (enemyIndex >= enemiesList.enemies.Length){
        enemyIndex = 0;
        tiempoInicio = Time.time;
        Debug.Log("Reiniciando chart...");
    }
    }

    void CargarCancion()
    {
        if (chart != null)
        {
            enemiesList = JsonUtility.FromJson<EnemiesList>(chart.text);
            if (enemiesList != null && enemiesList.enemies != null)
            {
                Debug.Log("Enemigos cargados: " + enemiesList.enemies.Length);
            }
        }
        else
        {
            Debug.LogError("No se encontró el archivo JSON: ");
        }
    }

    void GenerarEnemigo(Enemy enemy)
    {
        if (enemy.spawnRow < 1 || enemy.spawnRow > rows.Length)
        {
            Debug.LogWarning("Línea inválida: " + enemy.spawnRow);
            return;
        }

        Transform posicionline = rows[enemy.spawnRow - 1];
        Instantiate(enemyPrefabs[enemyTypeId], posicionline.position, Quaternion.identity);
    }
}
