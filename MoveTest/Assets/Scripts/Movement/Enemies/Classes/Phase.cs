using UnityEngine;

[System.Serializable]
public class Phase
{
    public int phaseId;
    public int enemyId;
    public float start;
    public float finish;
    public int[] spawnRow;
    public float[] spawnTime;
}
