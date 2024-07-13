using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[System.Serializable]
public enum EquipmentSlot
{
    Helmet,
    Overall,
    Glove,
    Boot,
    Weapon
}
public class PlayerEquipment : MonoBehaviour
{
    private EquipmentSlot 
        selectedSlot;

    private Dictionary<EquipmentSlot, Item>
        equippedList = new Dictionary<EquipmentSlot, Item>();

    private void Start()
    {
        for (int i = 0; i < Enum.GetNames(typeof(EquipmentSlot)).Length; i++)
        {
            equippedList.Add((EquipmentSlot)i, null);
        }
    }
    public void EquipItem(Item itemToEquip)
    {
        if (equippedList[itemToEquip._slotType] == null)
        {
            equippedList[itemToEquip._slotType] = itemToEquip;
            Debug.Log($"Equipped an item with name {itemToEquip._itemName} in {itemToEquip._slotType}");
        }
        else
        {
            Debug.Log($"{itemToEquip._slotType} is full, unable to equip");
        }
    }
    public Item UnequipItem(EquipmentSlot slot)
    {
        if (equippedList[slot] == null)
        {
            Debug.Log("No equipment to unequip");
        }
        else
        {
            Item item = equippedList[slot];
            equippedList[slot] = null;
            Debug.Log($"Unequipped {item._itemName}");
            return item;
        }
        return null;
    }
    public void DropItem()
    {
        UnequipItem(selectedSlot);
    }
    public void SelectSlot(int slot)
    {
        selectedSlot = (EquipmentSlot)slot;
        Debug.Log($"{selectedSlot} selected");
    }
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            UnequipItem(selectedSlot);
        }
        if(Input.GetKeyDown(KeyCode.M))
        { 
            EquipItem(Game._database._itemList.ElementAt(0));
        }
    }
}
