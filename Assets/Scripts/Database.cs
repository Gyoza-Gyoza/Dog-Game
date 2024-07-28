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
    private Dictionary<string, Entity> 
        enemyDB = new Dictionary<string, Entity>(), 
        playerDB = new Dictionary<string, Entity>();

    //For testing
    [SerializeField]
    private List<Player> playerList = new List<Player>(); 

    private Dictionary<string, Equipment>
        equipmentDB = new Dictionary<string, Equipment>();

    private Dictionary<string, Wave>
        waveDB = new Dictionary<string, Wave>();

    private List<Augment>
        augmentList = new List<Augment>();

    private List<Item> 
        itemList = new List<Item>();

    private Dictionary<string, Skill>
        skillDB = new Dictionary<string, Skill>();

    public Dictionary<string, Entity> _enemyList 
    { get { return enemyDB; } }

    public Dictionary<string, Entity> _playerDB
    { get { return playerDB; } }

    public Dictionary<string, Wave> _waveDB
    { get { return waveDB; } }

    public List<Augment> _augmentList
    { get { return augmentList; } }

    public List<Item> _itemList
    { get { return itemList; } }

    public Dictionary<string, Skill> _skillDB
    { get { return skillDB; } }

    public Dictionary<string, Equipment> _equipmentDB
    { get { return equipmentDB; } }

    private static Database
        database; 

    private void Awake()
    {
        if (database != null && database != this)
        {
            Destroy(this.gameObject);
            return;
        }
        else
        {
            database = this;
        }

        if(Game._database == null)
        {
            Game._database = this;
        }

        //Reading enemies csv 
        List<string> enemies = ParseCSV("/Databases/EnemyList.csv");
        foreach (string enemy in enemies)
        {
            string[] enemyEntry = enemy.Split(',');
            enemyDB.Add(enemyEntry[0], new Enemy(
                enemyEntry[1],
                int.Parse(enemyEntry[2]),
                int.Parse(enemyEntry[3]),
                int.Parse(enemyEntry[4]),
                int.Parse(enemyEntry[5]),
                enemyEntry[6],
                int.Parse(enemyEntry[7]),
                enemyEntry[8]));
        }

        //Reading player csv
        List<string> players = ParseCSV("/Databases/ClassList.csv"); 
        foreach (string player in players)
        {
            string[] playerEntry = player.Split(',');
            playerDB.Add(playerEntry[0], new Player(
                playerEntry[1],
                int.Parse(playerEntry[2]),
                int.Parse(playerEntry[3]),
                int.Parse(playerEntry[4]),
                int.Parse(playerEntry[5]),
                playerEntry[6],
                int.Parse(playerEntry[7]),
                int.Parse(playerEntry[8]),
                int.Parse(playerEntry[9]),
                playerEntry[10], 
                playerEntry[11],
                int.Parse(playerEntry[12]),
                float.Parse(playerEntry[13])
                ));
        }
        //Game._chosenPlayer = playerDB["CLASS_THIEF"] as Player;

        //Reading wave csv 
        List<string> waves = ParseCSV("/Databases/WaveList.csv");
        foreach (string wave in waves)
        {
            string[] waveEntry = wave.Split(",");
            waveDB.Add(waveEntry[0], new Wave(
                waveEntry[1],
                int.Parse(waveEntry[2]),
                int.Parse(waveEntry[3]),
                int.Parse(waveEntry[4]),
                int.Parse(waveEntry[5])
                ));
        }

        List<string> equipments = ParseCSV("/Databases/EquipmentList.csv");
        foreach (string equipment in equipments)
        {
            string[] equipmentEntry = equipment.Split(",");
            equipmentDB.Add(equipmentEntry[0], new Equipment(
                equipmentEntry[1],
                equipmentEntry[2],
                equipmentEntry[3],
                int.Parse(equipmentEntry[4]),
                int.Parse(equipmentEntry[5]),
                equipmentEntry[6],
                int.Parse(equipmentEntry[7]),
                int.Parse(equipmentEntry[8]),
                float.Parse(equipmentEntry[9]),
                float.Parse(equipmentEntry[10]),
                float.Parse(equipmentEntry[11])
                ));
            //Debug.Log(equipmentEntry[0] + equipmentEntry[1] + equipmentEntry[2] + equipmentEntry[3] + equipmentEntry[4] + equipmentEntry[5] + equipmentEntry[6] + equipmentEntry[7] + equipmentEntry[8] + equipmentEntry[9] + equipmentEntry[10] + equipmentEntry[11]);
        }

        List<string> projectiles = ParseCSV("/Databases/ProjectileSkillList.csv");
        foreach (string projectile in projectiles)
        {
            string[] projectileEntry = projectile.Split(",");
            skillDB.Add(projectileEntry[0], new Projectile(
                projectileEntry[1],
                projectileEntry[2],
                projectileEntry[3],
                projectileEntry[4],
                int.Parse(projectileEntry[5]),
                float.Parse(projectileEntry[6]),
                int.Parse(projectileEntry[7]),
                float.Parse(projectileEntry[8]),
                int.Parse(projectileEntry[9])));
            Debug.Log($"{projectileEntry[0]} {projectileEntry[1]} {projectileEntry[2]} {projectileEntry[3]} {projectileEntry[4]} {projectileEntry[5]} {projectileEntry[6]} {projectileEntry[7]} {projectileEntry[8]} {projectileEntry[9]}");
        }

        List<string> aoes = ParseCSV("/Databases/AOESkillList.csv");
        foreach(string aoe in aoes)
        {
            string[] aoesEntry = aoe.Split(",");
            skillDB.Add(aoesEntry[0], new AOE(
                aoesEntry[1],
                aoesEntry[2],
                aoesEntry[3],
                aoesEntry[4],
                int.Parse(aoesEntry[5]),
                float.Parse(aoesEntry[6]),
                int.Parse(aoesEntry[7])));
            Debug.Log($"{aoesEntry[0]} {aoesEntry[1]} {aoesEntry[2]} {aoesEntry[3]} {aoesEntry[4]} {aoesEntry[5]} {aoesEntry[6]} {aoesEntry[7]}");
        }

        //For testing 
        //foreach (Entity ent in playerClassList)
        //{
        //    playerList.Add(ent as Player);
        //}


        augmentList.Add(new Augment("Weak augment", "This is a weak augment", 0)); 
        augmentList.Add(new Augment("Strong augment", "This is a strong augment", 1));
        augmentList.Add(new Augment("Legendary augment", "This is a legendary augment", 2));

        //itemList.Add(new Equipment("Sword1", "A short sword, not used for much", "Assets/Art Assets/Shikashi's Fantasy Icons Pack v2/Shikashi's Fantasy Icons Pack v2/Sword1.png", 1, "Weapon", 1, 1, 1, 1, 1));
        //itemList.Add(new Equipment("Sword1", "A short sword, not used for much", "Assets/Art Assets/Shikashi's Fantasy Icons Pack v2/Shikashi's Fantasy Icons Pack v2/Sword2.png", 1, "Weapon", 1, 1, 1, 1, 1));
        //itemList.Add(new Equipment("Sword1", "A short sword, not used for much", "Assets/Art Assets/Shikashi's Fantasy Icons Pack v2/Shikashi's Fantasy Icons Pack v2/Sword3.png", 1, "Weapon", 1, 1, 1, 1, 1));
        //itemList.Add(new Equipment("Sword1", "A short sword, not used for much", "Assets/Art Assets/Shikashi's Fantasy Icons Pack v2/Shikashi's Fantasy Icons Pack v2/Sword4.png", 1, "Weapon", 1, 1, 1, 1, 1));
        //itemList.Add(new Equipment("Helmet1", "A short sword, not used for much", "Assets/Art Assets/Shikashi's Fantasy Icons Pack v2/Shikashi's Fantasy Icons Pack v2/Helmet1.png", 1, "Helmet", 1, 1 ,1, 1, 1));
        //itemList.Add(new Equipment("Overall1", "A short sword, not used for much", "Assets/Art Assets/Shikashi's Fantasy Icons Pack v2/Shikashi's Fantasy Icons Pack v2/Overall1.png", 1, "Overall", 1 ,1, 1, 1, 1));
        //itemList.Add(new Equipment("Boot1", "A short sword, not used for much", "Assets/Art Assets/Shikashi's Fantasy Icons Pack v2/Shikashi's Fantasy Icons Pack v2/Boot1.png", 1, "Boot", 1, 1, 1, 1 ,1));
        //itemList.Add(new Equipment("Glove1", "A short sword, not used for much", "Assets/Art Assets/Shikashi's Fantasy Icons Pack v2/Shikashi's Fantasy Icons Pack v2/Glove1.png", 1, "Glove", 1, 1, 1 ,1, 1));

        //skillDB.Add("PROJ0001", new Projectile("Basic Projectile", "EXAMPLE DESCRIPTION", "Assets/Images/SadHamster.png", 0, "Assets/Art Assets/Shikashi's Fantasy Icons Pack v2/Shikashi's Fantasy Icons Pack v2/BasicProjectile.prefab", 5, 2, 1, 30));
        //skillDB.Add("PROJ0002", new Projectile("Basic Projectile2", "EXAMPLE DESCRIPTION", "Assets/Images/SadHamster.png", 0, "Assets/Art Assets/Shikashi's Fantasy Icons Pack v2/Shikashi's Fantasy Icons Pack v2/BasicProjectile.prefab", 5, 2, 1, 30));
        //skillDB.Add("PROJ0003", new Projectile("Basic Projectile3", "EXAMPLE DESCRIPTION", "Assets/Images/SadHamster.png", 0, "Assets/Art Assets/Shikashi's Fantasy Icons Pack v2/Shikashi's Fantasy Icons Pack v2/BasicProjectile.prefab", 5, 2, 1, 30));
        //skillDB.Add("PROJ0004", new Projectile("Basic Projectile4", "EXAMPLE DESCRIPTION", "Assets/Images/SadHamster.png", 0, "Assets/Art Assets/Shikashi's Fantasy Icons Pack v2/Shikashi's Fantasy Icons Pack v2/BasicProjectile.prefab", 5, 2, 1, 30));
        //skillDB.Add("AREA0001", new AOE("HEAVENLY STRIKE", "Call upon divine power to bring down a massive sword# targeting a specific area. Dealing massive damage to all enemies within the impact zone.", "Assets/Images/SadHamster.png", "Assets/Art Assets/Shikashi's Fantasy Icons Pack v2/Shikashi's Fantasy Icons Pack v2/TextExplosion.prefab", 3, 7, 3));
        //skillDB.Add("AREA0002", new AOE("HEAVENLY STRIKE2", "Call upon divine power to bring down a massive sword# targeting a specific area. Dealing massive damage to all enemies within the impact zone.", "Assets/Images/SadHamster.png", "Assets/Art Assets/Shikashi's Fantasy Icons Pack v2/Shikashi's Fantasy Icons Pack v2/TextExplosion.prefab", 3, 7, 3));
        //skillDB.Add("AREA0003", new AOE("HEAVENLY STRIKE3", "Call upon divine power to bring down a massive sword# targeting a specific area. Dealing massive damage to all enemies within the impact zone.", "Assets/Images/SadHamster.png", "Assets/Art Assets/Shikashi's Fantasy Icons Pack v2/Shikashi's Fantasy Icons Pack v2/TextExplosion.prefab", 3, 7, 3));
        //skillDB.Add("AREA0004", new AOE("HEAVENLY STRIKE4", "Call upon divine power to bring down a massive sword# targeting a specific area. Dealing massive damage to all enemies within the impact zone.", "Assets/Images/SadHamster.png", "Assets/Art Assets/Shikashi's Fantasy Icons Pack v2/Shikashi's Fantasy Icons Pack v2/TextExplosion.prefab", 3, 7, 3));
    }
    private void Start()
    {
        Game._gameSceneManager.OpenScene("MainMenu", true, null);
    }
    private List<string> ParseCSV(string filePath)
    {
        List<string> result = new List<string>();

        sr = File.OpenText(Application.streamingAssetsPath + filePath); 

        sr.ReadLine();
        while (!sr.EndOfStream)
        {
            result.Add(sr.ReadLine());
        }

        return result;
    }
}
