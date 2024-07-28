using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item 
{
    protected string
        itemName,
        itemDescription,
        itemSprite;

    protected int
        costPrice,
        sellPrice;

    public string _itemName
    { get { return itemName; } }

    public string _itemDescription
    { get { return itemDescription; } }

    public string _itemSprite
    { get { return itemSprite; } }

    public int _costPrice
    { get { return costPrice; } }

    public int _sellPrice
    { get { return sellPrice;}}

    public Item(string itemName, string itemDescription, string itemSprite, int costPrice, int sellPrice)
    {
        this.itemName = itemName;
        this.itemDescription = itemDescription.Replace('#', ',');
        this.itemSprite = itemSprite;
        this.costPrice = costPrice;
        this.sellPrice = sellPrice;
    }
}