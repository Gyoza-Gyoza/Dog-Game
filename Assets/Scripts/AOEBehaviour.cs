using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AOEBehaviour : MonoBehaviour
{
    private int
        damage; 

    public int _damage
    {  get { return damage; } set {  damage = value; } }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        
    }
    public void SetStats(string name, int damage, float size, Sprite skillSprite)
    {
        this.gameObject.name = name;
        this.damage = damage;
        this.transform.localScale = new Vector3(size, size, 1f);
        this.GetComponent<SpriteRenderer>().sprite = skillSprite;
    }
}
