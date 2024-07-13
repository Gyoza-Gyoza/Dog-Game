using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Item 
{
    protected string
        itemName,
        itemId, 
        itemDescription;

    //protected Sprite
    //    itemSprite;

    protected int
        costPrice,
        sellPrice;

    protected EquipmentSlot
        slotType;

    public string
        _itemName
    {
        get { return itemName; }
    }

    public string 
        _itemId
    {
        get { return itemId; }
    }

    public string
        _itemDescription
    {
        get { return itemDescription; }
    }

    public int
        _costPrice
    {
        get { return costPrice; }
    }

    public int 
        _sellPrice
    {
        get { return sellPrice; }
    }

    public EquipmentSlot
        _slotType
    {
        get { return slotType; }
    }
    public Item(string itemName, string itemId, string itemDescription, int costPrice, int sellPrice, EquipmentSlot slotType)
    {
        this.itemName = itemName;
        this.itemId = itemId;
        this.itemDescription = itemDescription;
        this.costPrice = costPrice;
        this.sellPrice = sellPrice;
        this.slotType = slotType;
    }
}