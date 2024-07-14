using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Entity
{
    [SerializeField]
    protected string
        entityId,
        entityName;

    [SerializeField]
    protected int
        hp,
        attack,
        magicAttack,
        movementSpeed,
        armor,
        magicResist;

    [SerializeField]
    protected string
        entitySprite;

    public string _entityId
    { get { return entityId; } }
    public string _name
    { get { return entityName; } }

    public int _hp
    { get { return hp; } }

    public int _attack
    { get { return attack; } }

    public int _magicAttack
    { get { return magicAttack; } }

    public int _movementSpeed
    { get { return movementSpeed; } }

    public int _armor
    { get { return armor; } }

    public int _magicResist
    { get { return magicResist; } }

    public string _entitySprite
    { get { return entitySprite; } }

    public Entity(string entityId, string entityName, int hp, int attack, int magicAttack, int movementSpeed, int armor, int magicResist, string entitySprite)
    {
        this.entityId = entityId;
        this.entityName = entityName;
        this.hp = hp; 
        this.attack = attack;
        this.magicAttack = magicAttack;
        this.movementSpeed = movementSpeed;
        this.armor = armor; 
        this.magicResist = magicResist;
        this.entitySprite = entitySprite;
    }
}
