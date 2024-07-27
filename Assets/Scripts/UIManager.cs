using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

//This script exists to give the game script a reference to the inventory menu
public class UIManager : MonoBehaviour
{
    [SerializeField]
    private GameObject
        shopUI,
        inventoryUI,
        equipmentUI, 
        bg, 
        closeMenu; 

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
        dropButton, 
        sellButton;

    public ItemSlotScript[] _inventoryBoxList
    { get { return inventoryBoxList; } }

    public EquipmentSlotScript[] _equipmentBoxList 
    { get { return equipmentBoxList; } }

    private void Awake()
    {
        Game._uIManager = this;
        //gameObject.SetActive(false);
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
        sellButton.onClick.AddListener(() =>
        {
            Game._inventoryManager.SellItem();
        });
    }
    public void ToggleInventory()
    {
        equipmentUI.SetActive(!equipmentUI.activeInHierarchy);
        inventoryUI.SetActive(!inventoryUI.activeInHierarchy);
        bg.SetActive(!bg.activeInHierarchy);
        closeMenu.SetActive(!closeMenu.activeInHierarchy);
    }
    public void ToggleShop()
    {
        shopUI.SetActive(!shopUI.activeInHierarchy);
        inventoryUI.SetActive(!inventoryUI.activeInHierarchy);
        bg.SetActive(!bg.activeInHierarchy);
        closeMenu.SetActive(!closeMenu.activeInHierarchy);
    }
    public void CloseAllMenus()
    {
        Game._inputHandler.ResumeGame();
        shopUI.SetActive(false);
        inventoryUI.SetActive(false);
        equipmentUI.SetActive(false);
        bg.SetActive(false);
        closeMenu.SetActive(false);
    }
    public void UpdateInventory()
    {
        goldAmount.text = $"{Game._inventoryManager._goldCount}"; //Update gold amount 

        //Update stats 
        health.text = $"{Game._chosenPlayer._hp} + {Game._inventoryManager._statBoost.health}"; 
        attack.text = $"{Game._chosenPlayer._attack} + {Game._inventoryManager._statBoost.damage}";
        defence.text = $"{Game._chosenPlayer._defence} + {Game._inventoryManager._statBoost.defence}";
        projectileSize.text = $"{Game._inventoryManager._statBoost.projectileSize}"; //Get the projectile size of equipment 
        attackSpeed.text = $"{Game._chosenPlayer._attackSpeed} + {Game._inventoryManager._statBoost.attackSpeed}";
        critChance.text = $"{Game._chosenPlayer._critChance} + {Game._inventoryManager._statBoost.critChance}";
    }
}
