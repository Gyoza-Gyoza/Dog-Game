using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//DONE BY WANG JIA LE
[System.Serializable]
public enum EquipmentSlot
{
    Helmet,
    Overall,
    Glove,
    Boot,
    Accessory,
    Weapon
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

    protected Stats 
        stats = new Stats();

    public EquipmentSlot _slotType
    { get { return slotType; } }

    public int _itemDamage
    { get { return stats.damage; } }

    public int _itemDefence
    { get { return stats.defence; } }

    public float _projectileSize
    { get { return stats.projectileSize; } }

    public float _criticalChance
    { get { return stats.critChance; } }

    public float _attackSpeed
    { get { return stats.attackSpeed; } }

    public Stats _stats
    { get { return stats; } }

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

        this.stats.damage = itemDamage;
        this.stats.defence = itemDefence;
        this.stats.projectileSize = projectileSize;
        this.stats.critChance = criticalChance;
        this.stats.attackSpeed = attackSpeed;
    }
}
