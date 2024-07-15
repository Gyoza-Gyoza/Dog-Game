using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SlotScript : MonoBehaviour
{
    protected Item
        itemHeld = null;

    public Item _itemHeld
    { get { return itemHeld; } }

    private void Start()
    {
        GetComponent<Button>().onClick.AddListener(() =>
        {
            Game._inventoryManager.SelectItem(gameObject);
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
