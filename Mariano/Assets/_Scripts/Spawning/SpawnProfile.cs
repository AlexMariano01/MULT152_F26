using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "SpawnProfile", menuName = "Scriptable Objects/SpawnProfile")]
public class SpawnProfile : ScriptableObject
{
    public string profileName;
    [Header("Possible Enemies")]
    public List<EnemyType> enemyTypes;
    public int minEnemies = 1;
    public int maxEnemies = 5;
    [Header("Timing")]
    public float cooldownAfterClear = 10f;
}
