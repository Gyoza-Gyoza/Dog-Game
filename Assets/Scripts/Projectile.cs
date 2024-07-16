using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    private int
        damage; 

    public int _damage
    { get { return damage; } set { damage = value; } }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        IDamageable dmg = collision.GetComponent<IDamageable>();

        if (dmg != null)
        {
            dmg.DealDamage(damage);
            Destroy(this.gameObject);
        }
    }
}
