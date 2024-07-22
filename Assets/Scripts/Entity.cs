using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Entity
{
    [SerializeField]
    protected string
        entityName;

    [SerializeField]
    protected int
        hp,
        attack,
        movementSpeed,
        defence;

    [SerializeField]
    protected string
        entitySprite;

    public string _name
    { get { return entityName; } }

    public int _hp
    { get { return hp; } }

    public int _attack
    { get { return attack; } }

    public int _movementSpeed
    { get { return movementSpeed; } }

    public int _defence
    { get { return defence; } }

    public string _entitySprite
    { get { return entitySprite; } }

    public Entity(string entityName, int hp, int attack, int movementSpeed, int defence, string entitySprite)
    {
        this.entityName = entityName;
        this.hp = hp; 
        this.attack = attack;
        this.movementSpeed = movementSpeed;
        this.defence = defence;
        this.entitySprite = entitySprite;
    }
}
