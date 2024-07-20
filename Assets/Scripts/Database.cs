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

    [SerializeField]
    private List<Entity> 
        enemyList = new List<Entity>(), 
        playerClassList = new List<Entity>();
    
    //Dylan
    [SerializeField]
    private List<Equipment>
    equipmentList = new List<Equipment>();

    public List<Equipment> _equipmentList
    { get { return _equipmentList; } }

    //For testing
    [SerializeField]
    private List<Player> playerList = new List<Player>();

    private List<EnemyWaves>
        enemyWaves = new List<EnemyWaves>();

    private List<Augment>
        augmentList = new List<Augment>();

    private List<Item> 
        itemList = new List<Item>();

    private Dictionary<string, Skill>
        skillDB = new Dictionary<string, Skill>();

    public List<Entity> _enemyList 
    { get { return enemyList; } }

    public List<Entity> _playerClassList
    { get { return playerClassList; } }

    public List<EnemyWaves> _enemyWaves
    { get { return enemyWaves; } }

    public List<Augment> _augmentList
    { get { return augmentList; } }

    public List<Item> _itemList
    { get { return itemList; } }

    public Dictionary<string, Skill> _skillDB
    { get { return skillDB; } }

    private void Awake()
    {
        if(Game._database == null)
        {
            Game._database = this;
        }

        List<string> enemies = ParseCSV("Assets/Databases/EnemyList.csv");
        foreach (string enemy in enemies)
        {
            string[] enemyEntry = enemy.Split(',');
            enemyList.Add(new Enemy(
                enemyEntry[0],
                enemyEntry[1],
                int.Parse(enemyEntry[2]),
                int.Parse(enemyEntry[3]),
                int.Parse(enemyEntry[4]),
                int.Parse(enemyEntry[5]),
                int.Parse(enemyEntry[6]),
                int.Parse(enemyEntry[7]),
                enemyEntry[8]));
            Debug.Log($"{enemyEntry[1]} created and added into database");
        }

        List<string> players = ParseCSV("Assets/Databases/ClassList.csv"); 
        foreach (string player in players)
        {
            string[] playerEntry = player.Split(',');
            playerClassList.Add(new Player(
                playerEntry[0],
                playerEntry[1],
                int.Parse(playerEntry[2]),
                int.Parse(playerEntry[3]),
                int.Parse(playerEntry[4]),
                int.Parse(playerEntry[5]),
                int.Parse(playerEntry[6]),
                int.Parse(playerEntry[7]),
                playerEntry[8],
                int.Parse(playerEntry[9]),
                playerEntry[10]));
        }
        Game._chosenPlayer = playerClassList[3] as Player;

        //For testing 
        foreach (Entity ent in playerClassList)
        {
            playerList.Add(ent as Player);
        }
        //enemyList.Add(new Enemy("Slime", 1, 1, 1, 1, (EnemyTypes)0, "Assets/Images/SadHamster.png"));
        //enemyList.Add(new Enemy("Fat Slime", 2, 2, 2, 2, (EnemyTypes)1, "Assets/Images/SadHamster.png"));
        //enemyList.Add(new Enemy("Boss Slime", 5, 5, 5, 5, (EnemyTypes)2, "Assets/Images/SadHamster.png"));

        //Change to read from player tables
        //playerClassList.Add(new Player("Warrior", 1, 1, 1, 10));
        //playerClassList.Add(new Player("Archer", 1, 1, 1, 10));

        //Change to read from enemywaves table
        //The code will be structured so that the first number will be the entry
        //in the enum and the second will be the number of enemies of that category
        enemyWaves.Add(new EnemyWaves("1", "0@2#1@3#2@1"));

        augmentList.Add(new Augment("Weak augment", "This is a weak augment", 0)); 
        augmentList.Add(new Augment("Strong augment", "This is a strong augment", 1));
        augmentList.Add(new Augment("Legendary augment", "This is a legendary augment", 2));

        itemList.Add(new Item("Sword1", "1", "A short sword, not used for much", "Assets/Art Assets/Shikashi's Fantasy Icons Pack v2/Shikashi's Fantasy Icons Pack v2/Sword1.png", 1, 1, EquipmentSlot.Weapon));
        itemList.Add(new Item("Sword2", "1", "A short sword, not used for much", "Assets/Art Assets/Shikashi's Fantasy Icons Pack v2/Shikashi's Fantasy Icons Pack v2/Sword2.png", 1, 1, EquipmentSlot.Weapon));
        itemList.Add(new Item("Sword3", "1", "A short sword, not used for much", "Assets/Art Assets/Shikashi's Fantasy Icons Pack v2/Shikashi's Fantasy Icons Pack v2/Sword3.png", 1, 1, EquipmentSlot.Weapon));
        itemList.Add(new Item("Sword4", "1", "A short sword, not used for much", "Assets/Art Assets/Shikashi's Fantasy Icons Pack v2/Shikashi's Fantasy Icons Pack v2/Sword4.png", 1, 1, EquipmentSlot.Weapon));
        itemList.Add(new Item("Helmet1", "1", "A short sword, not used for much", "Assets/Art Assets/Shikashi's Fantasy Icons Pack v2/Shikashi's Fantasy Icons Pack v2/Helmet1.png", 1, 1, EquipmentSlot.Helmet));
        itemList.Add(new Item("Overall1", "1", "A short sword, not used for much", "Assets/Art Assets/Shikashi's Fantasy Icons Pack v2/Shikashi's Fantasy Icons Pack v2/Overall1.png", 1, 1, EquipmentSlot.Overall));
        itemList.Add(new Item("Boot1", "1", "A short sword, not used for much", "Assets/Art Assets/Shikashi's Fantasy Icons Pack v2/Shikashi's Fantasy Icons Pack v2/Boot1.png", 1, 1, EquipmentSlot.Boot));
        itemList.Add(new Item("Glove1", "1", "A short sword, not used for much", "Assets/Art Assets/Shikashi's Fantasy Icons Pack v2/Shikashi's Fantasy Icons Pack v2/Glove1.png", 1, 1, EquipmentSlot.Glove));

        //Dylan try
        /*
                List<string> equipments = ParseCSV("Assets/Databases/EquipmentList.csv");
        foreach (string equipment in equipments)
        {
            string[] equipmentEntry = equipment.Split(',');
            equipmentList.Add(new Equipment(
                equipmentEntry[0],
                equipmentEntry[1],
                equipmentEntry[2],
                equipmentEntry[3],
                equipmentEntry[4],
                int.Parse(equipmentEntry[5]),
                int.Parse(equipmentEntry[6]),
                int.Parse(equipmentEntry[7]),
                int.Parse(equipmentEntry[8]),
                int.Parse(equipmentEntry[9]),
                int.Parse(equipmentEntry[10]),
                int.Parse(equipmentEntry[11])));
            Debug.Log($"{equipmentEntry[1]} created and added into database");

        } */
    }
    private List<string> ParseCSV(string filePath)
    {
        List<string> result = new List<string>();

        sr = File.OpenText(filePath); 

        sr.ReadLine();
        while (!sr.EndOfStream)
        {
            result.Add(sr.ReadLine());
        }

        return result;
    }
}
