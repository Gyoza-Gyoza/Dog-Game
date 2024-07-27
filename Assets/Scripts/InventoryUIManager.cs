using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

//This script exists to give the game script a reference to the inventory menu
public class InventoryUIManager : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI
        goldAmount, 
        health,
        attack,
        defence, 
        projectileSize, 
        attackSpeed, 
        critChance;

    [SerializeField]
    private ItemSlotScript[]
        inventoryBoxList;

    [SerializeField]
    private EquipmentSlotScript[] 
        equipmentBoxList;

    [SerializeField]
    private Button
        equipButton,
        dropButton;

    public ItemSlotScript[] _inventoryBoxList
    { get { return inventoryBoxList; } }

    public EquipmentSlotScript[] _equipmentBoxList 
    { get { return equipmentBoxList; } }

    private void Awake()
    {
        Game._inventoryUIManager = gameObject;
        gameObject.SetActive(false);
        Game._inventoryManager._inventoryBoxList = inventoryBoxList;
        Game._inventoryManager._equipmentBoxList = equipmentBoxList;
    }

    private void OnEnable()
    {
        UpdateInventory();
        AssignButtonFunctions();
    }
    private void AssignButtonFunctions()
    {
        equipButton.onClick.AddListener(() =>
        {
            Game._inventoryManager.EquipItem();
        });
        dropButton.onClick.AddListener(() =>
        {
            Game._inventoryManager.DropItem();
        });
    }

    private void UpdateInventory()
    {
        goldAmount.text = $"{Game._inventoryManager._goldCount} gold"; //Update gold amount 

        //Update stats 
        health.text = $"{Game._player._currentHp} / {Game._chosenPlayer._hp} + {Game._inventoryManager._statBoost.health}"; 
        attack.text = $"{Game._chosenPlayer._attack} + ";
        defence.text = $"{Game._chosenPlayer._defence}";
        projectileSize.text = $"{Game._chosenPlayer._projectileType}"; //Get the projectile size of equipment 
        attackSpeed.text = $"{Game._chosenPlayer._attackSpeed}";
        critChance.text = $"{Game._chosenPlayer._critChance}";
    }
}
