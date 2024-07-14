using Microsoft.Unity.VisualStudio.Editor;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : Entity
{
    public Enemy(string entityId, string entityName, int hp, int attack, int magicAttack, int movementSpeed, int armor, int magicResist, string entitySprite) 
        : base (entityId, entityName, hp, attack, magicAttack, movementSpeed, armor, magicResist, entitySprite)
    {
        //this.enemyType = enemyType;
    }
}
