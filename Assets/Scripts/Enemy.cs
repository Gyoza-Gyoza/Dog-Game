using Microsoft.Unity.VisualStudio.Editor;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : Entity
{
    private int
        goldDrop; 

    public int _goldDrop
    { get { return goldDrop; } }

    public Enemy(string entityName, int hp, int attack, int movementSpeed, int defence, string entitySprite, int goldDrop) 
        : base(entityName, hp, attack, movementSpeed, defence, entitySprite)
    {
        this.goldDrop = goldDrop;
    }
}
