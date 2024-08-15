using System;
using System.Collections;
using System.IO;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

//DONE BY DYLAN NG SHAO WEI & WANG JIA LE 
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

    private StreamWriter
        sw;

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

    //private List<Augment>
    //    augmentList = new List<Augment>();

    private List<Item> 
        itemList = new List<Item>();

    private Dictionary<string, Skill>
        skillDB = new Dictionary<string, Skill>();

    private Dictionary<string, Dialogue>
        dialogueDB = new Dictionary<string, Dialogue>();

    public Dictionary<string, Entity> _enemyList 
    { get { return enemyDB; } }

    public Dictionary<string, Entity> _playerDB
    { get { return playerDB; } }

    public Dictionary<string, Wave> _waveDB
    { get { return waveDB; } }

    //public List<Augment> _augmentList
    //{ get { return augmentList; } }

    public List<Item> _itemList
    { get { return itemList; } }

    public Dictionary<string, Skill> _skillDB
    { get { return skillDB; } }

    public Dictionary<string, Equipment> _equipmentDB
    { get { return equipmentDB; } }

    public Dictionary<string, Dialogue> _dialogueDB
    { get { return dialogueDB; } }


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
        }

        List<string> dialogues = ParseCSV("/Databases/DialogueList.csv");
        foreach (string dialogue in dialogues)
        {
            string[] dialogueEntry = dialogue.Split(",");
            dialogueDB.Add(dialogueEntry[0], new Dialogue(
                dialogueEntry[1],
                dialogueEntry[2],
                dialogueEntry[3],
                dialogueEntry[4],
                dialogueEntry[5],
                dialogueEntry[6],
                dialogueEntry[7],
                dialogueEntry[8],
                dialogueEntry[9]
                ));

        }
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
    private void OnApplicationQuit()
    {
        AnalyticsManager._timePlayed = Time.realtimeSinceStartup;
        string filePath = Application.streamingAssetsPath + "/Databases/AnalyticsList.csv";

        string achievements = "";

        if(AnalyticsManager._monsterHunter)
        {
            achievements += "monsterHunter#"; 
        }
        if (AnalyticsManager._drowningInGold)
        {
            achievements += "drowningInGold#";
        }
        if (AnalyticsManager._tonsOfDamage)
        {
            achievements += "tonsOfDamage#";
        }

        if (File.Exists(filePath))
        {
            using (sw = new StreamWriter(filePath, true)) //If file exists, add on to the file 
            {
                sw.WriteLine($"{AnalyticsManager._goldEarned},{AnalyticsManager._monstersKilled},{AnalyticsManager._damageDealt},{AnalyticsManager._timePlayed},{achievements}");
            }
        }
        else
        {
            using (sw = new StreamWriter(filePath)) //If it doesn't, create a new file and add on to it 
            {
                sw.WriteLine("goldEarned,monstersKilled,damageDealt,timePlayed,achievements");
                sw.WriteLine($"{AnalyticsManager._goldEarned},{AnalyticsManager._monstersKilled},{AnalyticsManager._damageDealt},{AnalyticsManager._timePlayed},{achievements}");
            }
        }
    }
}
