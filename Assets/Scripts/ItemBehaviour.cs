using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ItemBehaviour : MonoBehaviour
{
    [SerializeField]
    private GameObject
        itemUI;

    public void SetStats(Item item)
    {
        TextMeshPro[] text = itemUI.GetComponentsInChildren<TextMeshPro>();

        if (item is Equipment)
        {
            Equipment equipment = (Equipment)item;
            foreach (TextMeshPro tmp in text)
            {
                switch (tmp.gameObject.name)
                {
                    case "Title":
                        tmp.text = equipment._itemName;
                        Debug.Log("Setting title");
                        break;

                    case "Description":
                        tmp.text = equipment._itemDescription;
                        Debug.Log("Setting description");
                        break;
                }
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        itemUI.SetActive(true);
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        itemUI.SetActive(false);
    }
}
