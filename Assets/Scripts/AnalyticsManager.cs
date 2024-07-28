using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class AnalyticsManager 
{
    private static int
        goldEarned,
        monstersKilled,
        damageDealt;

    private static float
        timePlayed;

    public static int _goldEarned
    {  get { return goldEarned; } 
        set 
        {
            Game._analyticsUIManager.UpdateGoldUI();
            goldEarned = value; 
        } 
    } 

    public static int _monstersKilled 
    { get { return monstersKilled; } 
        set 
        { 
            Game._analyticsUIManager.UpdateKillsUI();
            monstersKilled = value; 
        } 
    }

    public static int _damageDealt
    { get { return damageDealt; } 
        set 
        { 
            Game._analyticsUIManager.UpdateDamageUI();
            damageDealt = value; 
        } 
    }

    public static float _timePlayed 
    { get { return timePlayed; } set { timePlayed = value; } }
}
