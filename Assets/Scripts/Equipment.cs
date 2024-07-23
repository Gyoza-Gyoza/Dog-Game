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
        itemDefence;

    
    protected float
        criticalChance,
        projectileSize,
        attackSpeed;

    public EquipmentSlot _slotType
    { get { return slotType; } }

    public int _itemDamage
    { get { return itemDamage; } }

    public int _itemDefence
    { get { return itemDefence; } }

    public float _projectileSize
    { get { return projectileSize; } }

    public float _criticalChance
    { get { return criticalChance; } }

    public float _attackSpeed
    { get { return attackSpeed; } }

    public Equipment(string itemName, string itemDescription, string itemSprite, int costPrice, int sellPrice, string slotType, int itemDamage, int itemDefence, float projectileSize, float criticalChance, float attackSpeed)
        : base(itemName, itemDescription, itemSprite, costPrice, sellPrice)
    {
        switch (slotType)
        {
            case "HELMET":
                this.slotType = EquipmentSlot.Helmet;
                break;
            case "OVERALL":
                this.slotType = EquipmentSlot.Overall;
                break;
            case "GLOVE":
                this.slotType= EquipmentSlot.Glove;
                break;
            case "BOOT":
                this.slotType = EquipmentSlot.Boot;
                break;
            case "WEAPON":
                this.slotType = EquipmentSlot.Weapon;
                break;
            case "ACCESSORY":
                this.slotType = EquipmentSlot.Accessory;
                break;
        }

        this.itemDamage = itemDamage;
        this.itemDefence = itemDefence;
        this.projectileSize = projectileSize;
        this.criticalChance = criticalChance;
        this.attackSpeed = attackSpeed;
    }
}
