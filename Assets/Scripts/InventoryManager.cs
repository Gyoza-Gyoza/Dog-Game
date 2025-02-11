using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

[System.Serializable]

public class InventoryManager : MonoBehaviour
{
    private EquipmentSlot 
        selectedSlot;

    private GameObject
        selectedItem;

    private ItemSlotScript[]
        inventoryBoxList;

    private EquipmentSlotScript[]
        equipmentBoxList;

    [SerializeField]
    //private Dictionary<int, Item>
    //    inventoryList = new Dictionary<int, Item>();

    private List<Item> 
        inventoryList = new List<Item>();

    private Dictionary<EquipmentSlot, Equipment>
        equipmentList = new Dictionary<EquipmentSlot, Equipment>(); 

    public ItemSlotScript[] _inventoryBoxList
    { 
        get { return inventoryBoxList; } 
        set 
        { 
            inventoryBoxList = value; 
            foreach(ItemSlotScript iss in inventoryBoxList)
            {
                inventoryList.Add(null);
            }
        } 
    }
    public EquipmentSlotScript[] _equipmentBoxList
    { get { return equipmentBoxList; } set { equipmentBoxList = value; } }

    private int 
        goldCount;

    private Stats
        statBoost = new Stats(); 

    public int _goldCount
    { get { return goldCount; } }
    public Stats _statBoost
    { 
        get //Calculate stats upon request
        { 
            Stats result = new Stats();
            
            for(int i = 0; i < equipmentList.Count; i++)
            {
                if(equipmentList.ElementAt(i).Value != null)
                {
                    result += equipmentList.ElementAt(i).Value._stats;
                }
            }

            return result;
        } 
    }

    private void Awake()
    {
        if(Game._inventoryManager == null)
        {
            Game._inventoryManager = this;
        }
    }
    private void Start()
    {
        for (int i = 0; i < Enum.GetNames(typeof(EquipmentSlot)).Length; i++)
        {
            equipmentList.Add((EquipmentSlot)i, null);
        }
        //for(int i = 0; i < 15; i++)
        //{
        //    inventoryList.Add(i, null);
        //}
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
        Equipment equip = null;

        if (selectedItem.GetComponent<ItemSlotScript>()._itemHeld is Equipment)
        {
            equip = selectedItem.GetComponent<ItemSlotScript>()._itemHeld as Equipment;
        }
        EquipmentSlot slotToBePlacedIn = equip._slotType;

        if (equipmentList[slotToBePlacedIn] == null)
        {
            equipmentList[slotToBePlacedIn] = selectedItem.GetComponent<ItemSlotScript>()._itemHeld as Equipment;
            inventoryList[selectedItem.GetComponent<ItemSlotScript>()._itemId] = null; //Remove item at the selected position 
        }
        else
        {
            Item equippedItem = equipmentList[slotToBePlacedIn]; //Store equipped item 
            equipmentList[slotToBePlacedIn] = selectedItem.GetComponent<ItemSlotScript>()._itemHeld as Equipment; //Replace equipped item with selected item 
            inventoryList[selectedItem.GetComponent<ItemSlotScript>()._itemId] = null; //Remove item at the selected position 
            AddItem(equippedItem); //Adds unequipped item to inventory 
        }

        ////Get stats of the equipment 
        //for (int i = 0; i < equipmentList.Count; i++)
        //{
        //    if(equipmentList.ElementAt(i).Value != null)
        //    {
        //        statBoost += equipmentList.ElementAt(i).Value._stats;
        //    }
        //}

        //Updates the inventory UI
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
    public void AddItem(Item itemToAdd)
    {
        for (int i = 0; i < inventoryBoxList.Length; i++)
        {
            if (inventoryList[i] == null)
            {
                inventoryList[i] = itemToAdd;
                UpdateInventory();
                Debug.Log($"Obtained a {itemToAdd._itemName}!");
                break;
            }
        }
    }
    public void BuyItem(Item itemToBuy)
    {
        if (goldCount >= itemToBuy._costPrice)
        {
            AddItem(itemToBuy);
            goldCount -= itemToBuy._costPrice;

            UpdateInventory();
            UpdateShopMenu();
        }
    }
    public void DropItem()
    {
        inventoryList[selectedItem.GetComponent<ItemSlotScript>()._itemId] = null;
        UpdateInventory();
    }
    public void SellItem()
    {
        if(inventoryList[selectedItem.GetComponent<ItemSlotScript>()._itemId] != null)
        {
            GainGold(inventoryList[selectedItem.GetComponent<ItemSlotScript>()._itemId]._sellPrice);
            DropItem();

            UpdateShopMenu(); //Updates shop menu here only as its the only way to get gold while in the shop
        }
    }
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.M))
        {
            Debug.Log("Checking Equipment"); 
            foreach(KeyValuePair<EquipmentSlot, Equipment> keyValuePair in equipmentList)
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

            //foreach(KeyValuePair<int, Item> keyValuePair in inventoryList)
            //{
            //    string invResult = "";

            //    if (keyValuePair.Value == null)
            //    {
            //        invResult = "no item found";
            //    }
            //    else
            //    {
            //        invResult = keyValuePair.Value._itemName;
            //    }
            //    Debug.Log($"Slot {keyValuePair.Key}, {invResult}");
            //}
        }
        //if (Input.GetKeyDown(KeyCode.N))
        //{
        //    int rand = UnityEngine.Random.Range(0, Game._database._equipmentDB.Count);
        //    PickupItem(Game._database._equipmentDB.ElementAt(rand).Value);
        //}
    }
    public void GainGold(int goldAmount)
    {
        goldCount += goldAmount;
        AnalyticsManager._goldEarned += goldAmount;
    }
    //private void OnEnable() //Updates all the UIs 
    //{
    //    if(inventoryBoxList != null && equipmentBoxList != null)
    //    {
    //        UpdateInventory();
    //        Game._uIManager.UpdateInventory();
    //    }
    //}
    private bool InventoryHasSpace()
    {
        foreach (Item item in inventoryList)
        {
            if(item == null)
            {
                return true;
            }
        }
        return false;
    }
    public void UpdateInventory()
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
        Game._uIManager.UpdateInventory();
    }
    public void UpdateShopMenu()
    {
        foreach(ShopMenu shop in Game._shopMenus)
        {
            foreach(ShopUIBox box in shop._shopUIBoxes)
            {
                ShopUIBox shopUIBox = box.GetComponent<ShopUIBox>();
                if (goldCount >= box._itemCostPrice && InventoryHasSpace())
                {
                    shopUIBox.EnableButton();
                }
                else
                {
                    shopUIBox.DisableButton();
                }
            }
        }
    }
}
