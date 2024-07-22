using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Contains the values of the passive skills
public class Passives : Skill
{
    private PassiveEffect
        passiveEffect;

    private float
        passiveValue; 

    public PassiveEffect _passiveEffect
    {  get { return passiveEffect; } }

    public float _passiveValue
    {  get { return passiveValue; } }

    public Passives(string skillName, string skillDescription, string skillIcon, string skillPrefab, int skillCooldown, PassiveEffect passiveEffect, float passiveValue) 
        : base(skillName, skillDescription, skillIcon, skillPrefab, skillCooldown)
    {
        this.passiveEffect = passiveEffect; 
        this.passiveValue = passiveValue;
    }
}

//Categorizes the effects 
public enum PassiveEffect
{
    AttackSpeed, 
    MovementSpeed,
    CritChance, 
    Defence, 
    ProjectileSize
}