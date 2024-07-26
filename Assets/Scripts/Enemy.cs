using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : Entity
{
    private int
        goldDrop;

    private string
        extraSkill; 

    public int _goldDrop
    { get { return goldDrop; } }

    public string _extraSkill
    { get { return extraSkill; } }

    public Enemy(string entityName, int hp, int attack, int movementSpeed, int defence, string entitySprite, int goldDrop, string extraSkill) 
        : base(entityName, hp, attack, movementSpeed, defence, entitySprite)
    {
        this.goldDrop = goldDrop;
        this.extraSkill = extraSkill;
    }
}
