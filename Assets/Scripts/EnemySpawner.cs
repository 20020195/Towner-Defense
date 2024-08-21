using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnemySpawner : MonoBehaviour
{
    [Header("Reference")]
    [SerializeField] private GameObject[] enemyPrefabs;

    [Header("Attributes")]
    [SerializeField] private int baseEnemies = 8;
    [SerializeField] private float enemiesPerSecond = 0.5f;
    [SerializeField] private float timeBetweenWave = 5;
    [SerializeField] private float difficultScalingFactor = 0.75f;

    [Header("Event")]
    public static UnityEvent onEnemyDestroy = new UnityEvent();

    public int currentWave = 1;
    private float timeSinceLastSpawn;
    public int enemiesAlive;
    [SerializeField] private int enemiesLeftToSpawn;
    private bool isSpawning;

    public static EnemySpawner Instance;

    private void Awake()
    {

        if (Instance == null)
        {
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }


        onEnemyDestroy.AddListener(EnemyDestroyed);
    }

    private void Start()
    {
         StartCoroutine(StartWave());
    }

    private void Update()
    {
        if (!isSpawning) { return; }
        timeSinceLastSpawn += Time.deltaTime;

        if (timeSinceLastSpawn >= (1f / enemiesPerSecond) && enemiesLeftToSpawn > 0) {
            SpawnEnemies();
            enemiesLeftToSpawn--;
            enemiesAlive++;
            timeSinceLastSpawn = 0f;
        }

        if (enemiesAlive == 0 && enemiesLeftToSpawn == 0)
        {
            EndWave();
        }
    }

    private void EnemyDestroyed()
    {
        enemiesAlive --;
    }

    void SpawnEnemies()
    {
        int index = Mathf.Clamp(Random.Range(0, currentWave - 1), 0, enemyPrefabs.Length-1);
        GameObject prefabToSpawn = Instantiate(enemyPrefabs[index], LevelManager.Instance.startPos);
        prefabToSpawn.GetComponent<EnemyController>().enemyHP *= Mathf.Pow(currentWave, (difficultScalingFactor/2));
        prefabToSpawn.GetComponent<EnemyController>().enemyHP = Mathf.RoundToInt(prefabToSpawn.GetComponent<EnemyController>().enemyHP);
    }

    private IEnumerator StartWave()
    {
        yield return new WaitForSeconds(timeBetweenWave);

        isSpawning = true;
        enemiesLeftToSpawn = EnemiesPerWave();
    }

    private void EndWave()
    {
        isSpawning = false;
        timeSinceLastSpawn = 0f;
        currentWave++;

        StartCoroutine(StartWave());
    }

    private int EnemiesPerWave()
    {
        return Mathf.RoundToInt(baseEnemies * Mathf.Pow(currentWave, difficultScalingFactor));
    }
}
