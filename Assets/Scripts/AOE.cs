using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Contains the values of the AOE skills
public class AOE : Skill
{
    private float
        aOEsize;

    private int
        aOEDamage; 

    public float _aOESize
    { get {  return aOEsize; } }

    public int _aOEDamage
    { get { return aOEDamage; } }

    public AOE(string skillName, string skillDescription, string skillIcon, string skillSprite, int skillCooldown, float size, int damage)
        : base (skillName, skillDescription, skillIcon, skillSprite, skillCooldown)
    {
        this.aOEsize = size;
        this.aOEDamage = damage;
    }
}
