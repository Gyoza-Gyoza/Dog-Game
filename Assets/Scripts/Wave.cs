using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wave
{
    private string
        enemyToSpawn;

    private int
        dungeon,
        waveNumber,
        enemyKillCount,
        spawnFrequency;

    public string _enemyToSpawn
    { get {  return enemyToSpawn; } }

    public int _dungeon
    { get { return dungeon; } }

    public int _waveNumber
    { get { return waveNumber; } }

    public int _enemyKillCount
    { get { return enemyKillCount; } }
    
    public int _spawnFrequency
    { get { return spawnFrequency; } }

    public Wave(string enemyToSpawn, int dungeon, int waveNumber, int enemyKillCount, int spawnFrequency)
    {
        this.enemyToSpawn = enemyToSpawn;
        this.dungeon = dungeon;
        this.waveNumber = waveNumber;
        this.enemyKillCount = enemyKillCount;
        this.spawnFrequency = spawnFrequency;
    }
}
