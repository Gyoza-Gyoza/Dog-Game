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

    private List<Entity> 
        enemyList = new List<Entity>(), 
        playerClassList = new List<Entity>();

    private List<EnemyWaves>
        enemyWaves = new List<EnemyWaves>();

    private List<Augment>
        augmentList = new List<Augment>();

    private List<Item> 
        itemList = new List<Item>();

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

    public List<Augment>
        _augmentList
    {
        get { return augmentList; }
    }

    public List<Item>
        _itemList
    {
        get { return itemList; }
    }

    private void Awake()
    {
        if(Game._database == null)
        {
            Game._database = this;
        }

        Debug.Log(ParseCSV("Assets/Databases/Equipment List.csv"));

        enemyList.Add(new Enemy("Slime", 1, 1, 1, 1, (EnemyTypes)0, "Assets/Images/SadHamster.png"));
        enemyList.Add(new Enemy("Fat Slime", 2, 2, 2, 2, (EnemyTypes)1, "Assets/Images/SadHamster.png"));
        enemyList.Add(new Enemy("Boss Slime", 5, 5, 5, 5, (EnemyTypes)2, "Assets/Images/SadHamster.png"));

        //Change to read from player tables
        playerClassList.Add(new Player("Warrior", 1, 1, 1, 10));
        playerClassList.Add(new Player("Archer", 1, 1, 1, 10));

        //Change to read from enemywaves table
        //The code will be structured so that the first number will be the entry
        //in the enum and the second will be the number of enemies of that category
        enemyWaves.Add(new EnemyWaves("1", "0@2#1@3#2@1"));

        augmentList.Add(new Augment("Weak augment", "This is a weak augment", 0)); 
        augmentList.Add(new Augment("Strong augment", "This is a strong augment", 1));
        augmentList.Add(new Augment("Legendary augment", "This is a legendary augment", 2));

        itemList.Add(new Item("Sword", "1", "A short sword, not used for much", 1, 1, EquipmentSlot.Weapon));
        itemList.Add(new Equipment("Helmet", "2", "A basic helmet, offers little protection", 1, 1, EquipmentSlot.Helmet, EquipmentEffect.HealthRegen, 10, 10, 30));
    }
    private string ParseCSV(string filePath)
    {
        string result = "";

        sr = File.OpenText(filePath); 

        sr.ReadLine();
        result = sr.ReadToEnd();

        return result;
    }
}
