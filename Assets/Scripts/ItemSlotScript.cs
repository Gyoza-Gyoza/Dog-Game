using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSlotScript : SlotScript
{
    private int
        itemId;

    public int _itemId
    { get { return itemId; } set { itemId = value; } }

    public void AddItem(Item itemToEquip, int id)
    {
        itemHeld = itemToEquip;
        this.itemId = id;
    }
}
