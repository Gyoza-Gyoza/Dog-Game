using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    private GameObject[]
        spawnLocations;

    private List<string>
        enemySpawnPool = new List<string>();

    public Queue<Wave> 
        enemyWaves = new Queue<Wave>();

    private int
        spawnFrequency,
        killCount, 
        waveCompleteCount;

    private float
        timer;

    private bool
        startWave = false;

    public int _killCount
    { get { return killCount; } set { killCount = value; } }

    public List<string> _enemySpawnPool
    { get { return enemySpawnPool; } }

    public GameObject[] _spawnLocations
    { get { return spawnLocations; } }

    public bool _startWave
    { get { return startWave; } set {  startWave = value; } }

    private void Awake()
    {
        if (Game._waveManager == null)
        {
            Game._waveManager = this;
        }
    }


    private void Update()
    {
        if(startWave)
        {
            if (timer < 1f)
            {
                timer += Time.deltaTime * spawnFrequency;

                if (timer >= 1f)
                {
                    SpawnEnemy(enemySpawnPool[Random.Range(0, enemySpawnPool.Count)]); //Spawns a random enemy from the pool
                    timer = 0f;
                }
            }
            if (killCount >= waveCompleteCount)
            {
                if (enemyWaves.Count != 0)
                {
                    NextWave();
                }
            }
        }
    }
    public void InitializeStage(int stageNumber)
    {
        InitializeSpawns();
        InitializeWave(stageNumber);
        NextWave();
        startWave = true;
    }
    private void InitializeSpawns()
    {
        spawnLocations = GameObject.FindGameObjectsWithTag("EnemySpawnLocation");
    }
    private void InitializeWave(int stageNumber)
    {
        Debug.Log("Initializing waves");
        Debug.Log(stageNumber);
        //Initializes the requested wave into a dictionary to sort the waves by its waveNumber 
        Dictionary<int, Wave> waves = new Dictionary<int, Wave>();
        foreach(KeyValuePair<string, Wave> keyValuePair in Game._database._waveDB)
        {
            if(keyValuePair.Value._dungeon == stageNumber)
            {
                waves.Add(keyValuePair.Value._waveNumber, keyValuePair.Value);
            }
        }

        //Adds it into the queue for spawning later on
        for(int i = 1; i < waves.Count + 1; i++)
        {
            enemyWaves.Enqueue(waves[i]);
        }
    }
    private void NextWave()
    {
        Wave wave = enemyWaves.Dequeue();

        if(wave._enemyToSpawn.Substring(0,4) == "ENEM")
        {
            enemySpawnPool.Add(wave._enemyToSpawn);
        }
        else if(wave._enemyToSpawn.Substring(0, 4) == "BOSS")
        {
            SpawnEnemy(wave._enemyToSpawn);
        }

        spawnFrequency = wave._spawnFrequency;
        killCount = 0;
        waveCompleteCount = wave._enemyKillCount;
    }
    private void SpawnEnemy(string enemyId)
    {
        //Picks a random location to spawn the enemy
        Game._enemyFactory.GetEnemy(enemyId, spawnLocations[Random.Range(0, spawnLocations.Length)].transform);
    }
    public void ResetWaves()
    {
        startWave = false;
        spawnLocations = null;
        enemySpawnPool.Clear();
        enemyWaves.Clear();
    }
}
