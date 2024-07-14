using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public enum EquipmentEffect
{
    HealthRegen, 
    Luck, 
    MovementSpeed, 
    CritChance
}

[System.Serializable]
public class Equipment : Item
{
    [SerializeField]
    private EquipmentEffect
        equipmentEffect;

    [SerializeField]
    private int
        armour,
        magicResist, 
        effectValue; 

    public EquipmentEffect 
        _equipmentEffect
    { get { return equipmentEffect; } }

    public int
        _armour
    { get { return armour; } }

    public int _magicResist
    { get { return magicResist; } }

    public int _effectValue
    { get { return effectValue; } }

    public Equipment(string itemName, string itemId, string itemDescription, int costPrice, int sellPrice, EquipmentSlot slotType, EquipmentEffect equipmentEffect, int armour, int magicResist, int effectValue) : base (itemName, itemId, itemDescription, costPrice, sellPrice, slotType)
    {
        this.equipmentEffect = equipmentEffect;
        this.armour = armour;
        this.magicResist = magicResist;
        this.effectValue = effectValue;
        Debug.Log("Equipment Created!");
    }
}
