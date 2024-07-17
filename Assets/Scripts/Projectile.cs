using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    private int
        damage;

    private float
        projectileSpeed = 30f, 
        timer = 0f;

    public int _damage
    { get { return damage; } set { damage = value; } }

    private void Update()
    {
        transform.localPosition += transform.TransformDirection(0f, projectileSpeed * Time.deltaTime, 0f);
        
        if(timer < 2f)
        {
            timer += Time.deltaTime; 

            if (timer >= 2f)
            {
                Game._skillManager.DestroyProjectile(this.gameObject);
                timer = 0f;
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        IDamageable dmg = collision.GetComponent<IDamageable>();

        if (dmg != null)
        {
            dmg.TakeDamage(damage);
            Game._skillManager.DestroyProjectile(this.gameObject);
        }
    }
}
