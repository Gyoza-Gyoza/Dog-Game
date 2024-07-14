using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Player : Entity
{
    [SerializeField]
    private int
        attackSpeed;

    [SerializeField]
    private string
        weaponType;

    public int _attackSpeed
    { get { return attackSpeed; } }

    public string _weaponType
    { get { return weaponType; } }

    public Player(string entityId, string entityName, int hp, int attack, int magicAttack, int movementSpeed, int armor, int magicResist, string entitySprite, int attackSpeed, string weaponType) 
        : base (entityId, entityName, hp, attack, magicAttack, movementSpeed,armor, magicResist, entitySprite)
    {
        this.attackSpeed = attackSpeed;
        this.weaponType = weaponType;
    }
}
