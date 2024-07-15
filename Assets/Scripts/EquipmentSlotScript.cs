using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public enum EquipmentSlot
{
    Helmet,
    Overall,
    Glove,
    Boot,
    Weapon
}
public class EquipmentSlotScript : SlotScript
{
    [SerializeField]
    private EquipmentSlot
        slot;

    public EquipmentSlot _slot
    { get { return slot; } }

    private void Start()
    {
        GetComponent<Button>().onClick.AddListener(() =>
        {
            Game._inventoryManager.SelectSlot(slot);
        });
    }
}

