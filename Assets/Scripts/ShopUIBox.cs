using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopUIBox : MonoBehaviour
{
    private Dictionary<string, Item>
        itemHeld;

    [SerializeField]
    private Image
        itemIcon;

    [SerializeField]
    private TextMeshProUGUI
        itemTitle,
        itemCost;

    [SerializeField]
    private Button
        buyButton;

    private int
        itemCostPrice; 

    public int _itemCostPrice
    { get { return itemCostPrice; }  }

    public void PlaceItem(string itemId, Item itemToGive)
    {
        if(itemHeld == null)
        {
            itemHeld = new Dictionary<string, Item>
            { [itemId] = Game._database._equipmentDB[itemId] };
        }
        itemTitle.text = itemToGive._itemName;
        itemCost.text = itemToGive._costPrice.ToString();
        itemCostPrice = itemToGive._costPrice;

        AssetManager.LoadSprites(itemToGive._itemSprite, (Sprite sp) =>
        {
            itemIcon.sprite = sp;
        });

        buyButton.onClick.AddListener(() =>
        {
            Game._inventoryManager.BuyItem(itemToGive);
        });
    }
    public void EnableButton()
    {
        buyButton.interactable = true;
    }
    public void DisableButton()
    {
        buyButton.interactable = false;
    }
}
