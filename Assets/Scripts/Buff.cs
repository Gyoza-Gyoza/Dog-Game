using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Buff : Skill
{
    public float
        buffDuration;

    public BuffType
        buffType; 

    public Buff(string skillName, string skillDescription, float buffDuration, BuffType buffType)
        : base(skillName, skillDescription)
    {
        this.buffDuration = buffDuration;
        this.buffType = buffType;
    }
}

public enum BuffType
{
    MovementSpeed, 
    AttackSpeed, 
    AttackDamage, 
    ProjectileSize
}