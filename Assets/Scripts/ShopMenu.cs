using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This script exists to give the game script a reference to the shop menus along with their types as well as initializing the shop menu items
public class ShopMenu : MonoBehaviour
{
    [SerializeField]
    private EquipmentSlot
        menuType;

    private List<ShopUIBox>
        shopUIBoxes = new List<ShopUIBox>();

    [SerializeField]
    private GameObject
        shopItemBoxPrefab,
        menuTab;

    public EquipmentSlot _menuType
    { get { return menuType; } }

    public List<ShopUIBox> _shopUIBoxes 
    { get { return shopUIBoxes; } }

    private void Awake()
    {
        Game.AddToShopMenus(this);
    }
    private void Start()
    {
        //Creates a item entry for each item in the database 
        foreach(KeyValuePair<string, Equipment> keyValuePair in Game._database._equipmentDB)
        {
            if (keyValuePair.Value._slotType == menuType)
            {
                ShopUIBox box = Instantiate(shopItemBoxPrefab, transform).GetComponent<ShopUIBox>();
                box.PlaceItem(keyValuePair.Key, keyValuePair.Value);
                shopUIBoxes.Add(box);
            }
        }

        //Check if all tabs are active before turning them off 
        //This is used to preload all the shop items upon game start 
        if(menuType != 0)
        {
            menuTab.SetActive(false);
        }
        Game._tabManager.CountInitializedTabs();
    }
}
