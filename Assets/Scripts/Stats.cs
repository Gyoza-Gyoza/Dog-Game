using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stats
{
    public int
        health,
        damage,
        defence;

    public float
        critChance,
        projectileSize,
        attackSpeed;

    public Stats()
    {
        this.health = 0;
        this.damage = 0;
        this.defence = 0;
        this.projectileSize = 0;
        this.attackSpeed = 0;
        this.critChance = 0;
    }
    public Stats(int health, int damage, int defence, float critChance, float projectileSize, float attackSpeed)
    {
        this.health = health;
        this.damage = damage;
        this.defence = defence;
        this.critChance = critChance;
        this.projectileSize = projectileSize;
        this.attackSpeed = attackSpeed;
    }
    public static Stats operator+(Stats s1, Stats s2)
    {
        return new Stats(
            s1.health + s2.health,
            s1.damage + s2.damage,
            s1.defence + s2.defence,
            s1.critChance + s2.critChance,
            s1.projectileSize + s2.projectileSize,
            s1.attackSpeed + s2.attackSpeed);
    }
    public static Stats operator-(Stats s1, Stats s2)
    {
        return new Stats(
            s1.health - s2.health,
            s1.damage - s2.damage,
            s1.defence - s2.defence,
            s1.critChance - s2.critChance,
            s1.projectileSize - s2.projectileSize,
            s1.attackSpeed - s2.attackSpeed);
    }
}