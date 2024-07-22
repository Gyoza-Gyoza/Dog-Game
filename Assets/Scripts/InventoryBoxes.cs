using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//This script exists to give the inventory manager a reference to the inventory UI boxes
public class InventoryBoxes : MonoBehaviour
{
    public ItemSlotScript[]
        inventoryBoxes;

    private void Awake()
    {
        Game._inventoryManager._inventoryBoxList = inventoryBoxes;
    }
}
