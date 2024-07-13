using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity
{
    [SerializeField]
    protected string
        entityName;
    [SerializeField]
    protected int
        hp,
        attack,
        attackSpeed,
        movementSpeed; 

    public string
        _name
    {
        get { return entityName; }
    }

    public int 
        _hp
    {
        get { return hp; }
    } 

    public int 
        _attack
    {
        get { return attack; }
    }

    public int 
        _attackSpeed
    {
        get { return attackSpeed; } 
    }

    public int
        _movementSpeed
    {
        get { return movementSpeed; }
    }
    public Entity(string entityName, int hp, int attack, int attackSpeed, int movementSpeed)
    {
        this.entityName = entityName;
        this.hp = hp;
        this.attack = attack;
        this.attackSpeed = attackSpeed;
        this.movementSpeed = movementSpeed;
    }
}
