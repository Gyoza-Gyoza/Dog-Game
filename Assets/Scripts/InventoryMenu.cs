using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This script exists to give the game script a reference to the inventory menu
public class InventoryMenu : MonoBehaviour
{
    private void Awake()
    {
        Game._inventoryMenu = gameObject;
        gameObject.SetActive(false);
    }
}
