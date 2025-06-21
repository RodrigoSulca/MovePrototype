using System.Linq;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemiesGenerator : MonoBehaviour
{
    public TextAsset chart;
    public Transform[] rows;
    public GameObject[] enemyPrefabs;
    public EnemyController enemyController;
    public EnemiesList[] enemiesList;
    public PhasesList phasesList;
    private int enemyIndex = 0;
    private int phaseIndex = 0;
    private int controllerIndex = 0;
    private float tiempoInicio;

    void Start()
    {
        CargarFases();
        CargarEnemigos();
        tiempoInicio = Time.time; // 1800
    }

    void Update()
    {
        if (enemiesList[phaseIndex] == null || enemyIndex >= enemiesList[phaseIndex].enemies.Length)
        {
            return;
        }

        float tiempoActual = Time.time - tiempoInicio;

        while (enemyIndex < enemiesList[phaseIndex].enemies.Length && enemiesList[phaseIndex].enemies[enemyIndex].spawnTime <= tiempoActual)
        {
            // Auxiliares
            Enemy n_enemy = enemiesList[phaseIndex].enemies[enemyIndex];
            Phase n_phase = phasesList.phases[phaseIndex];
            // Creamos enemigos siempre que spawnTime este dentro del tiempo de la fase
            if (n_enemy.spawnTime <= n_phase.finish)
            {
                GenerarEnemigo(n_enemy, n_phase.spawnRow[enemyIndex]);
                enemyIndex++;
            }
            else
            {
                // Verificamos que termino la fase anterior correctamente
                // Primero revisamos si no hay mas enemigos a spawnear
                if (enemiesList[phaseIndex].enemies[enemyIndex] != null && phaseIndex < phasesList.phases.Length)
                {
                    phaseIndex++; // Nueva fase
                    enemyIndex = 0; // Indice vuelve a 0
                }
                break;
            }
            // Logica de enemigos
            // Despues de 2 segundos, el Enemigo Azul aumenta su velocidad
            // for (int i = 0; i <= enemyIndex; i++) // Recorrer enemigos existentes
            // {
            //     for (int j = 0; j <= phaseIndex; j++) // Recorrer fases existentes
            //     {
            //         Enemy test = enemiesList[j].enemies[i];
            //         if (tiempoActual <= (test.spawnTime + 2) && test.enemyId == 2) AumentarVelocidad(i, j);
            //     }
            // }
        }
    }

    void CargarFases()
    {
        if (chart != null)
        {
            phasesList = JsonUtility.FromJson<PhasesList>(chart.text);
            if (phasesList != null && phasesList.phases != null)
            {
                Debug.Log("Fases cargados: " + phasesList.phases.Length);
            }
        }
        else
        {
            Debug.LogError("No se encontró el archivo JSON.");
        }
    }
    void CargarEnemigos()
    {
        if (phasesList != null)
        {
            enemiesList = new EnemiesList[phasesList.phases.Length];
            for (int i = 0; i < phasesList.phases.Length; i++) // Recorremos las fases
            {
                enemiesList[i] = new EnemiesList();
                enemiesList[i].enemies = new Enemy[phasesList.phases[i].enemyId.Length];
                for (int j = 0; j < phasesList.phases[i].enemyId.Length; j++) // Recorremos las IDs de los enemigos de cada fase
                {
                    Enemy enemy_data = new();
                    // Generamos la data del Enemigo
                    enemy_data.enemyId = phasesList.phases[i].enemyId[j];
                    // Temporal, esto debe ser cargado desde la DB interna de enemigos
                    if (enemy_data.enemyId == 1) enemy_data.speed = (float)3;
                    else if (enemy_data.enemyId == 2) enemy_data.speed = (float)3.5;
                    else if (enemy_data.enemyId == 3) enemy_data.speed = 2;
                    // =====
                    enemy_data.spawnTime = phasesList.phases[i].start + phasesList.phases[i].spawnTime[j];
                    // Asignamos el enemigo a la lista
                    enemiesList[i].enemies[j] = enemy_data;
                    // Debug.Log("Enemigo:" + enemy_data.enemyId);
                    // Debug.Log("Velocidad:" + enemy_data.speed);
                    // Debug.Log("Columna:" + enemy_data.spawnRow);
                    // Debug.Log("Tiempo:" + enemy_data.spawnTime);
                    // Debug.Log("Se añadio 1 enemigo a la lista:" + i);
                }
                Debug.Log("Lista de enemigos " + i + " creada con exito.");
            }
        }
        else
        {
            Debug.LogError("No se cargo la lista de fases.");
        }
    }
    void GenerarEnemigo(Enemy enemy, int row) // añadir datos de la fase
    {
        if (row < 1 || row > rows.Length)
        {
            Debug.LogWarning("Línea inválida: " + row);
            return;
        }
        if (enemy.enemyId < 1 || enemy.enemyId > 4)   // 3 Tipos de enemigos - Red(1), Blue(2) y Green(3)
        {
            Debug.LogWarning("ID de enemigo inválido: " + enemy.enemyId);
            return;
        }
        Transform posicionline = rows[row - 1];
        Instantiate(enemyPrefabs[enemy.enemyId - 1], posicionline.position, Quaternion.identity);
        enemyController = enemyPrefabs[enemy.enemyId - 1].GetComponent<EnemyController>();
        // Asignamos velocidades iniciales a los enemigos
        if (enemyController != null)
        {
            enemyController.speed = enemy.speed;
        }
    }
    // void AumentarVelocidad(int eId, int pId)
    // {
    //     enemyController[pId][eId].speed = 1;
    // }
}