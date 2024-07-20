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
        IDamageable dmg = collision.GetComponent<IDamageable>();

        if (dmg != null)
        {
            dmg.TakeDamage(damage);
            Debug.Log("Boom");
        }
    }
    public void SetStats(int damage, float size)
    {
        this.damage = damage;
        this.transform.localScale = new Vector3(size, size, 1f);
    }
    private void OnDisable()
    {
        Game._skillManager.DestroySpell(this.gameObject);
    }
}
