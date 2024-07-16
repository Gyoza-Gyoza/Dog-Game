using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[System.Serializable]

public class InventoryManager : MonoBehaviour
{
    private EquipmentSlot 
        selectedSlot;

    private GameObject
        selectedItem;

    [SerializeField]
    private ItemSlotScript[]
        inventoryBoxList;

    [SerializeField]
    private EquipmentSlotScript[]
        equipmentBoxList;

    private Dictionary<EquipmentSlot, Item>
        equipmentList = new Dictionary<EquipmentSlot, Item>();

    [SerializeField]
    private Dictionary<int, Item>
        inventoryList = new Dictionary<int, Item>();

    private void Awake()
    {
        if(Game._inventoryManager == null)
        {
            Game._inventoryManager = this;
        }
    }
    private void Start()
    {
        inventoryBoxList = GetComponentsInChildren<ItemSlotScript>(); //Gets references to inventory item UI boxes
        equipmentBoxList = GetComponentsInChildren<EquipmentSlotScript>(); //Gets references to the equipment UI boxes

        for (int i = 0; i < Enum.GetNames(typeof(EquipmentSlot)).Length; i++)
        {
            equipmentList.Add((EquipmentSlot)i, null);
        }
        for(int i = 0; i < 15; i++)
        {
            inventoryList.Add(i, null);
        }
    }
    public void SelectSlot(EquipmentSlot slotToSelect)
    {
        selectedSlot = slotToSelect;
        Debug.Log($"{selectedSlot} selected");
    }
    public void SelectItem(GameObject itemToSelect)
    {
        selectedItem = itemToSelect;
    }
    public void EquipItem()
    {
        EquipmentSlot slotToBePlacedIn = selectedItem.GetComponent<ItemSlotScript>()._itemHeld._slotType;

        if (equipmentList[slotToBePlacedIn] == null)
        {
            equipmentList[slotToBePlacedIn] = selectedItem.GetComponent<ItemSlotScript>()._itemHeld;
            inventoryList[selectedItem.GetComponent<ItemSlotScript>()._itemId] = null; //Remove item at the selected position 
        }
        else
        {
            Item equippedItem = equipmentList[slotToBePlacedIn]; //Store equipped item 
            equipmentList[slotToBePlacedIn] = selectedItem.GetComponent<ItemSlotScript>()._itemHeld; //Replace equipped item with selected item 
            inventoryList[selectedItem.GetComponent<ItemSlotScript>()._itemId] = null; //Remove item at the selected position 
            PickupItem(equippedItem); //Adds unequipped item to inventory 
        }

        UpdateInventory();
    }
    public Item UnequipItem(EquipmentSlot slot)
    {
        if (equipmentList[slot] == null)
        {
            Debug.Log("No equipment to unequip");
        }
        else
        {
            Item item = equipmentList[slot];
            equipmentList[slot] = null;
            Debug.Log($"Unequipped {item._itemName}");
            return item;
        }
        return null;
    }
    public void PickupItem(Item itemToPickup)
    {
        for(int i = 0; i < inventoryList.Count; i++)
        {
            if (inventoryList[i] == null)
            {
                inventoryList[i] = itemToPickup;
                UpdateInventory();
                Debug.Log($"Obtained a {itemToPickup._itemName}!");
                break;
            }
        }
    }
    public void DropItem()
    {
        inventoryList[selectedItem.GetComponent<ItemSlotScript>()._itemId] = null;
        UpdateInventory();
    }
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.M))
        {
            Debug.Log("Checking Equipment"); 
            foreach(KeyValuePair<EquipmentSlot, Item> keyValuePair in equipmentList)
            {
                string result = ""; 

                if (keyValuePair.Value == null)
                {
                    result = "no equipment found";
                }
                else
                {
                    result = keyValuePair.Value._itemName;
                }
                Debug.Log($"{keyValuePair.Key}, {result}");
            }
            Debug.Log("No more equipment");

            Debug.Log("Checking Inventory"); 

            foreach(KeyValuePair<int, Item> keyValuePair in inventoryList)
            {
                string invResult = "";

                if (keyValuePair.Value == null)
                {
                    invResult = "no item found";
                }
                else
                {
                    invResult = keyValuePair.Value._itemName;
                }
                Debug.Log($"Slot {keyValuePair.Key}, {invResult}");
            }
        }
        if (Input.GetKeyDown(KeyCode.N))
        {
            int rand = UnityEngine.Random.Range(0, Game._database._itemList.Count);
            PickupItem(Game._database._itemList[rand]);
        }
    }
    private void UpdateInventory()
    {
        for (int i = 0; i < inventoryBoxList.Length; i++) //Loops through the inventory UI 
        {
            //Copies the items in the inventory into the UI list as it iterates through both lists 
            if (i < inventoryList.Count) 
            {
                inventoryBoxList[i].AddItem(inventoryList[i], i);
                inventoryBoxList[i].UpdateItemSlot();
            }
            else
            {
                inventoryBoxList[i].RemoveItem();
                inventoryBoxList[i].UpdateItemSlot();
            }
        }

        for (int i = 0; i < equipmentBoxList.Length; i++) //Loops through the equipment UI 
        {
            //Copies the items in the equipment list into the UI list as it iterates through both lists 
            if (i < equipmentList.Count)
            {
                equipmentBoxList[i].AddItem(equipmentList[(EquipmentSlot)i]);
                equipmentBoxList[i].UpdateItemSlot();
            }
            else
            {
                equipmentBoxList[i].RemoveItem();
                equipmentBoxList[i].UpdateItemSlot();
            }
        }

        //foreach (ItemSlotScript iss in inventoryBoxList)
        //{
        //    iss.UpdateItemSlot();
        //}
        //foreach (EquipmentSlotScript ess in equipmentBoxList)
        //{
        //    ess.UpdateItemSlot();
        //}
    }
}
