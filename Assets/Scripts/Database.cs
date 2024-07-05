using System;
using System.Collections;
using System.IO;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public enum DatabaseTypes
{
    Enemies, 
    PlayerClasses, 
    EnemyWaves
}
public class Database : MonoBehaviour
{
    private StreamReader
        sr; 

    private string
        csvToRead; 

    [SerializeField]
    private List<Entity> 
        enemyList = new List<Entity>(), 
        playerClassList = new List<Entity>();

    [SerializeField]
    private List<EnemyWaves>
        enemyWaves = new List<EnemyWaves>();

    public List<Entity>
        _enemyList 
    { 
        get { return enemyList; } 
    }

    public List<Entity>
        _playerClassList
    { 
        get { return playerClassList; } 
    }

    public List<EnemyWaves>
        _enemyWaves
    { 
        get { return enemyWaves; } 
    }

    private void Awake()
    {
        Game._database = this;

        Debug.Log(ParseCSV("Assets/Databases/Equipment List.csv"));

        enemyList.Add(new Enemy("Slime", 1, 1, 1, 1, (EnemyTypes)0));
        enemyList.Add(new Enemy("Fat Slime", 2, 2, 2, 2, (EnemyTypes)1));
        enemyList.Add(new Enemy("Boss Slime", 5, 5, 5, 5, (EnemyTypes)2));

        //Change to read from player tables
        playerClassList.Add(new Player("Warrior", 1, 1, 1, 10));

        //Change to read from enemywaves table
        //The code will be structured so that the first number will be the entry
        //in the enum and the second will be the number of enemies of that category
        enemyWaves.Add(new EnemyWaves("1", "0@2#1@3#2@1"));
    }
    private string ParseCSV(string filePath)
    {
        string trash = "";
        string result = "";

        sr = File.OpenText(filePath); 

        trash = sr.ReadLine();
        result = sr.ReadToEnd();

        return result;
    }
}
