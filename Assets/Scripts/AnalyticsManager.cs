using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//DONE BY WANG JIA LE
public static class AnalyticsManager 
{
    private static int
        goldEarned,
        monstersKilled,
        damageDealt;

    private static float
        timePlayed;

    private static int
        monsterHunterAchievement = 1000,
        drowningInGoldAchievement = 20000,
        tonsOfDamageAchievement = 1000000;

    private static bool
        monsterHunter = false,
        drowningInGold = false,
        tonsOfDamage = false;

    public static int _goldEarned
    {  get { return goldEarned; } 
        set 
        {
            goldEarned = value; 
            Game._analyticsUIManager.UpdateGoldUI();
            if(goldEarned >= drowningInGoldAchievement)
            {
                drowningInGold = true;
            }
        } 
    } 

    public static int _monstersKilled 
    { get { return monstersKilled; } 
        set 
        { 
            monstersKilled = value; 
            //Game._analyticsUIManager.UpdateKillsUI();
            if(monstersKilled >= monsterHunterAchievement)
            {
                monsterHunter = true;
            }
        } 
    }

    public static int _damageDealt
    { get { return damageDealt; } 
        set 
        { 
            damageDealt = value; 
            //Game._analyticsUIManager.UpdateDamageUI();
            if(damageDealt >= tonsOfDamageAchievement)
            {
                tonsOfDamage = true;
            }
        } 
    }

    public static float _timePlayed 
    { get { return timePlayed; } set { timePlayed = value; } }

    public static bool _monsterHunter
    {  get { return monsterHunter; } }

    public static bool _drowningInGold
    {  get { return drowningInGold; } }

    public static bool _tonsOfDamage
    { get { return tonsOfDamage; } }
}
