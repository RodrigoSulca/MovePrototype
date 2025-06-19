using System.Linq;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemiesGenerator : MonoBehaviour
{
    public TextAsset chart;
    public Transform[] rows;
    public GameObject[] enemyPrefabs;

    public EnemiesList[] enemiesList;
    public PhasesList phasesList;
    private int enemyIndex = 0;
    private int phaseIndex = 0;
    private float tiempoInicio;

    void Start()
    {
        CargarFases();
        CargarEnemigos();
        tiempoInicio = Time.time;
    }

    void Update()
    {
        if (enemiesList[phaseIndex] == null || enemyIndex >= enemiesList[phaseIndex].enemies.Length){
            return;
        }

        float tiempoActual = Time.time - tiempoInicio;

        while (enemyIndex < enemiesList[phaseIndex].enemies.Length && enemiesList[phaseIndex].enemies[enemyIndex].spawnTime <= tiempoActual)
        {
            GenerarEnemigo(enemiesList[phaseIndex].enemies[enemyIndex]);
            enemyIndex++;
        }

        // if (enemyIndex >= enemiesList.enemies.Length){ // Test para reiniciar
        //     enemyIndex = 0;
        //     tiempoInicio = Time.time;
        //     Debug.Log("Reiniciando chart...");
        // }
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
                Debug.Log("Fase:" + phasesList.phases[i].phaseId);
                enemiesList[i] = new EnemiesList();
                enemiesList[i].enemies = new Enemy[phasesList.phases[i].enemyId.Length];
                for (int j = 0; j < phasesList.phases[i].enemyId.Length; j++) // Recorremos las IDs de los enemigos de cada fase
                {
                    Enemy enemy_data = new();
                    // Generamos la data del Enemigo
                    enemy_data.enemyId = phasesList.phases[i].enemyId[j];
                    //Debug.Log("Enemigo:" + enemy_data.enemyId);
                    // Temporal, esto debe ser cargado desde la DB interna de enemigos
                    if (enemy_data.enemyId == 2) enemy_data.speed = 5;
                    else if (enemy_data.enemyId == 3) enemy_data.speed = 1;
                    Debug.Log("Velocidad:" + enemy_data.speed);
                    // =====
                    enemy_data.spawnRow = phasesList.phases[i].spawnRow[j];
                    Debug.Log("Columna:" + enemy_data.spawnRow);
                    enemy_data.spawnTime = phasesList.phases[i].start + phasesList.phases[i].spawnTime[j];
                    Debug.Log("Tiempo:" + enemy_data.spawnTime);
                    Debug.Log("i:" + i + ", j:" + j);
                    // Asignamos el enemigo a la lista
                    enemiesList[i].enemies[j] = enemy_data;
                    Debug.Log("Se añadio 1 enemigo a la lista:" + i);
                }
                Debug.Log("Lista de enemigos " + i + " creada con exito.");
            }
        }
        else
        {
            Debug.LogError("No se cargo la lista de fases.");
        }
    }
    void GenerarEnemigo(Enemy enemy) // añadir datos de la fase
    {
        if (enemy.spawnRow < 1 || enemy.spawnRow > rows.Length)
        {
            Debug.LogWarning("Línea inválida: " + enemy.spawnRow);
            return;
        }
        if (enemy.enemyId < 1 || enemy.enemyId > 4)   // 3 Tipos de enemigos - Red(1), Blue(2) y Green(3)
        {
            Debug.LogWarning("ID de enemigo inválido: " + enemy.enemyId);
            return;
        }
        Transform posicionline = rows[enemy.spawnRow - 1];
        Instantiate(enemyPrefabs[enemy.enemyId - 1], posicionline.position, Quaternion.identity);
    }
}