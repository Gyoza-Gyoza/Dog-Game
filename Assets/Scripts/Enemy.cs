using Microsoft.Unity.VisualStudio.Editor;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public enum EnemyTypes
{
    Basic, 
    Elite, 
    Boss
}
[System.Serializable]
public class Enemy : Entity
{
    private EnemyTypes
        enemyType;

    private string
        enemySprite;

    public EnemyTypes
        _enemyType
    {
        get { return enemyType; }
    }

    public string _enemySprite
    { get { return enemySprite; } }

    public Enemy(string name, int hp, int attack, int attackSpeed, int movementSpeed, EnemyTypes enemyType, string enemySprite) : base (name, hp, attack, attackSpeed, movementSpeed)
    {
        this.enemyType = enemyType;
        this.enemySprite = enemySprite;
    }
}
