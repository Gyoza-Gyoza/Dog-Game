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
            goldEarned = value; 
            Game._analyticsUIManager.UpdateGoldUI();
        } 
    } 

    public static int _monstersKilled 
    { get { return monstersKilled; } 
        set 
        { 
            monstersKilled = value; 
            Game._analyticsUIManager.UpdateKillsUI();
        } 
    }

    public static int _damageDealt
    { get { return damageDealt; } 
        set 
        { 
            damageDealt = value; 
            Game._analyticsUIManager.UpdateDamageUI();
        } 
    }

    public static float _timePlayed 
    { get { return timePlayed; } set { timePlayed = value; } }
}
