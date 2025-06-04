using UnityEngine;

public class SpawnNotes : MonoBehaviour
{
    public float spawnInterval;
    public GameObject notePrefab;
    private float time;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        if (time >= spawnInterval)
        {
            time = 0;
            Instantiate(notePrefab, transform.position, Quaternion.identity);
        }

    }
}
