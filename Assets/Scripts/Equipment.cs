using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Equipment : Item
{

    [SerializeField]
    private int
        defence,
        damage,
        projectileSize,
        criticalChance,
        attackSpeed; 

    public int
        _defence
    { get { return defence; } }

    public int _damage
    { get { return damage; } }

    public int _projectileSize
    { get { return projectileSize; } }

    public int _criticalChance
    { get { return criticalChance; } }

    public int _attackSpeed
    { get { return attackSpeed; } }

    public Equipment(string itemId, string itemName, string itemDescription, string itemSprite, EquipmentSlot slotType, int costPrice, int sellPrice, int defence, int damage, int projectileSize, int criticalChance, int attackSpeed)
        : base (itemName, itemId, itemDescription, itemSprite, costPrice, sellPrice, slotType)
    {
        this.defence = defence;
        this.damage = damage;
        this.projectileSize = projectileSize;
        this.criticalChance = criticalChance;
        this.attackSpeed = attackSpeed;

        Debug.Log("Equipment Created!");
    }
}
