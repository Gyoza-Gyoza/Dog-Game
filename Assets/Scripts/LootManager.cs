using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootManager : MonoBehaviour
{
    [SerializeField]
    private GameObject
        itemPrefab; 

    private Stack<GameObject>
        itemStack = new Stack<GameObject>();

    private void Awake()
    {
        if(Game._lootManager == null)
        {
            Game._lootManager = this;
        }
    }
    public void DropLoot(Transform transform, Item itemToDrop)
    {
        if(itemStack.TryPop(out GameObject result))
        {
            result.transform.position = transform.position;
            result.SetActive(true);
            result.name = itemToDrop._itemId;
            //result.GetComponent<SpriteRenderer>().sprite = itemToDrop._itemSprite;
            result.GetComponent<ItemBehaviour>().SetStats(itemToDrop);
        }
        else
        {
            GameObject item = Instantiate(itemPrefab, transform.position, Quaternion.identity); 
            item.name = itemToDrop._itemId;
            //item.GetComponent<SpriteRenderer>().sprite = itemToDrop._itemSprite; 
            item.GetComponent<ItemBehaviour>().SetStats(itemToDrop);
        }
    }
    public void DestroyItem(GameObject itemToDestroy)
    {
        itemToDestroy.SetActive(false);
        itemStack.Push(itemToDestroy);
    }
}
