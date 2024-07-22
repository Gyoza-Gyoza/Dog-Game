using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This script exists to give the inventory manager a reference to the equipment UI boxes
public class EquipmentBoxes : MonoBehaviour
{
    public EquipmentSlotScript[]
        equipmentBoxes;

    private void Awake()
    {
        Game._inventoryManager._equipmentBoxList = equipmentBoxes;
    }
}
