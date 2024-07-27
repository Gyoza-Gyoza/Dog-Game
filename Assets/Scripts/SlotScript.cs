using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Assertions.Must;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SlotScript : MonoBehaviour
{
    protected Item
        itemHeld = null;

    protected Button
        button;

    public Item _itemHeld
    { get { return itemHeld; } }

    private void Start()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(() =>
        {
            Game._inventoryManager.SelectItem(gameObject);
            button.Select();
        });
    }

    public virtual void AddItem(Item itemToEquip)
    {
        itemHeld = itemToEquip;
    }

    public void RemoveItem()
    {
        itemHeld = null;
    }

    public void UpdateItemSlot()
    {
        if(itemHeld != null)
        {
            AssetManager.SetImage(itemHeld._itemSprite, transform.GetChild(0).gameObject);
            transform.GetChild(0).gameObject.GetComponent<Image>().enabled = true;
        }
        else
        {
            transform.GetChild(0).gameObject.GetComponent<Image>().enabled = false;
        }
    }
}
