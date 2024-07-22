using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum EquipmentSlot
{
    Helmet,
    Overall,
    Glove,
    Boot,
    Weapon,
    Accessory
}
public class Equipment : Item
{
    protected EquipmentSlot 
        slotType;

    protected int
        itemDamage,
        itemDefence,
        projectileSize,
        criticalChance,
        attackSpeed;

    public EquipmentSlot _slotType
    { get { return slotType; } }

    public int _itemDamage
    { get { return itemDamage; } }

    public int _itemDefence
    { get { return itemDefence; } }

    public int _projectileSize
    { get { return projectileSize; } }

    public int _criticalChance
    { get { return criticalChance; } }

    public int _attackSpeed
    { get { return attackSpeed; } }

    public Equipment(string itemName, string itemDescription, string itemSprite, int costPrice, EquipmentSlot slotType, int itemDamage, int itemDefence, int projectileSize, int criticalChance, int attackSpeed)
        : base(itemName, itemDescription, itemSprite, costPrice)
    {
        this.slotType = slotType;
        this.itemDamage = itemDamage;
        this.itemDefence = itemDefence;
        this.projectileSize = projectileSize;
        this.criticalChance = criticalChance;
        this.attackSpeed = attackSpeed;
    }
}
