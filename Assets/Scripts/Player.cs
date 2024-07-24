using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Player : Entity
{
    private int
        attackSpeed,
        attackRange,
        critChance,
        dashSpeed;

    private float
        dashDuration;

    private string
        projectileType,
        classHurtSprite;

    public int _attackSpeed
    { get { return attackSpeed; } }

    public int _attackRange
    { get { return attackRange; } }

    public int _critChance
    { get { return critChance; } }

    public int _dashSpeed
    { get { return dashSpeed; } }

    public float _dashDuration
    { get { return dashDuration; } }

    public string _projectileType
    { get { return projectileType; } }

    public string _classHurtSprite
    { get { return classHurtSprite; } }

    public Player(string entityName, int hp, int attack, int movementSpeed, int defence, string entitySprite, int attackSpeed, int attackRange, int critChance, string projectileType, string classHurtSprite, int dashSpeed, float dashDuration) 
        : base(entityName, hp, attack, movementSpeed, defence, entitySprite)
    {
        this.attackSpeed = attackSpeed;
        this.attackRange = attackRange;
        this.critChance = critChance;
        this.projectileType = projectileType;
        this.classHurtSprite = classHurtSprite;
        this.dashSpeed = dashSpeed;
        this.dashDuration = dashDuration;
    }
}
