using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EncounterDirector : MonoBehaviour
{
    [Header("Profiles")]
    [SerializeField] private List<SpawnProfile> spawnProfiles = new();

    [Header("Spawn Points")]
    [SerializeField] private List<EnemySpawnPoint> spawnPoints = new();

    [Header("Difficulty Setting")]
    [SerializeField] private float fastKillThreshold = 10f;
    [SerializeField] private float slowKillThreshold = 30f;
    [SerializeField] private int minDifficultyBonus = -3;
    [SerializeField] private int maxDifficultyBonus = 10;

    private readonly List<GameObject> activeEnemies = new();
    private float encounterStartTime;
    private int difficultyModifier;
    public int CurrentEncounter {get; private set;}
    public int AliveEnemies => activeEnemies.Count;

    private void Start()
    {
        StartCoroutine(EncounterRoutine());
    }

    private IEnumerator EncounterRoutine()
    {
        while (true)
        {
            CurrentEncounter++;
            SpawnProfile profile = GetRandomProfile();
            SpawnEncounter(profile);
            encounterStartTime = Time.time;

            yield return new WaitUntil(() => activeEnemies.Count == 0);
            EvaluatePerformance();
            yield return new WaitForSeconds(profile.cooldownAfterClear);
        }
    }

    private void SpawnEncounter(SpawnProfile profile)
    {
        if (profile == null)
        {
            return;
        }

        int enemyCount = Random.Range(profile.minEnemies, profile.maxEnemies +1);
        enemyCount += difficultyModifier;
        enemyCount = Mathf.Max(1, enemyCount);
        Debug.Log($"Encounter: {CurrentEncounter}" + $"Enemies: {enemyCount}" + $"Difficulty Bolus: {difficultyModifier}");

        for (int i = 0; i < enemyCount; i++)
        {
            SpawnEnemy(profile);
        }
        RefreshTargeting();
    }

    private void SpawnEnemy(SpawnProfile profile)
    {
        if (profile.enemyTypes.Count == 0)
        {
            return;
        }

        EnemyType enemyType = profile.enemyTypes[Random.Range(0, profile.enemyTypes.Count)];
        EnemySpawnPoint spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Count)];
        GameObject enemy = Instantiate(enemyType.enemyPrefab, spawnPoint.transform.position, spawnPoint.transform.rotation);
        activeEnemies.Add(enemy);
        ScaleEnemy(enemy);
        StartCoroutine(TrackEnemy(enemy));
    }

    private IEnumerator TrackEnemy( GameObject enemy )
    {
        yield return new WaitUntil( () => enemy == null);
        activeEnemies.Remove(enemy);
    }

    private void ScaleEnemy(GameObject enemy)
    {
        EnemyHealth health = enemy.GetComponent<EnemyHealth>();
        if (health != null)
        {
            int bonus = Mathf.Max(0, difficultyModifier * 10);
            health.maxHealth += bonus;
            health.health = health.maxHealth;

            if (health.healthSlider != null)
            {
                health.healthSlider.maxValue = health.maxHealth;
                health.healthSlider.value = health.health;
            }
        }
    }

    private void EvaluatePerformance()
    {
        float clearTime = Time.time - encounterStartTime;
        Debug.Log($"Encounter Clear Time: {clearTime}");

        if (clearTime < fastKillThreshold)
        {
            difficultyModifier++;
        }
        else if (clearTime > slowKillThreshold)
        {
            difficultyModifier--;
        }

        difficultyModifier = Mathf.Clamp(difficultyModifier, minDifficultyBonus, maxDifficultyBonus);
        Debug.Log($"Difficulty Modifier: {difficultyModifier}");
    }

    private SpawnProfile GetRandomProfile()
    {
        return spawnProfiles[Random.Range(0, spawnProfiles.Count)];
    }

    private void RefreshTargeting()
    {
        PlayerTargeting targeting = FindAnyObjectByType<PlayerTargeting>();
        if (targeting != null)
        {
            targeting.RefreshTargets();
        }
    }
}