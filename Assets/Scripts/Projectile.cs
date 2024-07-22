using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Contains the values of the projectile skills
public class Projectile : Skill
{
    private int
        projectilePierce,
        projectileSpeed;

    private float
        projectileDamage,
        projectileSize; 

    public float _projectileDamage
    { get { return projectileDamage; } }

    public int _projectilePierce
    { get { return projectilePierce; } }

    public int _projectileSpeed
    { get { return projectileSpeed; } }

    public float _projectileSize
    { get { return projectileSize; } }

    public Projectile(string skillName, string skillDescription, string skillIcon, int skillCooldown, string skillSprite, float projectileDamage, int projectilePierce, float projectileSize, int projectileSpeed) 
        : base (skillName, skillDescription, skillIcon, skillSprite, skillCooldown)
    {
        this.projectileDamage = projectileDamage;
        this.projectilePierce = projectilePierce;
        this.projectileSpeed = projectileSpeed;
        this.projectileSize = projectileSize;
    }
}
