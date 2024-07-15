using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Item 
{
    [SerializeField]
    protected string
        itemName,
        itemId, 
        itemDescription,
        itemSprite;

    [SerializeField]
    protected int
        costPrice,
        sellPrice;

    [SerializeField]
    protected EquipmentSlot
        slotType;

    public string _itemName
    { get { return itemName; } }

    public string _itemId
    { get { return itemId; } }

    public string _itemDescription
    { get { return itemDescription; } }

    public string _itemSprite
    { get { return itemSprite; } }

    public int _costPrice
    { get { return costPrice; } }

    public int _sellPrice
    { get { return sellPrice; } }

    public EquipmentSlot _slotType
    { get { return slotType; } }

    public Item(string itemName, string itemId, string itemDescription, string itemSprite, int costPrice, int sellPrice, EquipmentSlot slotType)
    {
        this.itemName = itemName;
        this.itemId = itemId;
        this.itemDescription = itemDescription;
        this.itemSprite = itemSprite;
        this.costPrice = costPrice;
        this.sellPrice = sellPrice;
        this.slotType = slotType;
    }
}