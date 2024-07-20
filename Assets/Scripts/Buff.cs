using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Buff : Skill
{
    public float
        buffDuration;

    public BuffType
        buffType; 

    public Buff(string skillName, string skillDescription, string skillIcon, string skillSprite, int skillCooldown, float buffDuration, BuffType buffType)
        : base(skillName, skillDescription, skillIcon, skillSprite, skillCooldown)
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