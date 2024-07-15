using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class ItemSlotScript : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private Item
        itemHeld; 

    public void OnPointerEnter(PointerEventData eventData)
    {
        Debug.Log($"{gameObject.name} entered");
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        Debug.Log($"{gameObject.name} exited");
    }
    private void OnMouseDown()
    {
        //Game._cursor.GetComponent<SpriteRenderer>().sprite = 
    }
    private void EquipItem(Item itemToEquip)
    {
        itemHeld = itemToEquip;
    }
    public void UpdateItemSlot()
    {
        if(itemHeld != null)
        {
            AssetManager.SetSprite(itemHeld._itemSprite, GetComponentInChildren<GameObject>());
        }
        else
        {
            GetComponentInChildren<GameObject>().GetComponent<SpriteRenderer>().sprite = null;
            Debug.Log("No item is equipped");
        }
    }
}
