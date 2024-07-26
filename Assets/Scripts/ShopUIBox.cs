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

    public void PlaceItem(string itemId, Item itemToGive)
    {
        if(itemHeld == null)
        {
            itemHeld = new Dictionary<string, Item>
            { [itemId] = Game._database._equipmentDB[itemId] };
        }
        itemTitle.text = itemToGive._itemName;
        itemCost.text = itemToGive._costPrice.ToString();

        AssetManager.LoadSprites(itemToGive._itemSprite, (Sprite sp) =>
        {
            itemIcon.sprite = sp;
        });
    }
}
