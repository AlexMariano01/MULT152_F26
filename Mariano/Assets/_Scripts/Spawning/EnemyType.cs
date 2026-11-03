using UnityEngine;

[CreateAssetMenu(fileName = "EnemyType", menuName = "Scriptable Objects/EnemyType")]
public class EnemyType : ScriptableObject
{
    public string enemyName;
    public GameObject enemyPrefab;
    [Min(1)]
    public int difficultyValue = 1;
}
