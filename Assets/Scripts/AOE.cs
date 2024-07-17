using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AOE : Skill
{
    private float
        size;

    private int
        damage; 

    public AOE(string skillName, string skillDescription, float size, int damage)
        : base (skillName, skillDescription)
    {
        this.size = size;
        this.damage = damage;
    }
}
