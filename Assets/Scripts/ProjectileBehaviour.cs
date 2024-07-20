using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileBehaviour : MonoBehaviour
{
    private int
        damage, 
        projectilePierce = 2, 
        currentPierce = 0;

    private float
        projectileSpeed = 30f, 
        timer = 0f;

    public int _damage
    { get { return damage; } set { damage = value; } }

    public int _currentPierce
    { get { return currentPierce; } set { currentPierce = value; } }

    private void Update()
    {
        transform.localPosition += transform.TransformDirection(0f, projectileSpeed * Time.deltaTime, 0f);
        
        if(timer < 1f)
        {
            timer += Time.deltaTime; 

            if (timer >= 1f)
            {
                Game._skillManager.DestroyProjectile(this.gameObject);
                timer = 0f;
            }
        }
    }
    public void SetStats(string name, int damage, int projectilePierce, int projectileSpeed, float projectileSize, Sprite skillSprite)
    {
        this.name = name;
        this.damage = damage;
        this.projectilePierce = projectilePierce;
        this.projectileSpeed = projectileSpeed;
        this.gameObject.transform.localScale = new Vector3(projectileSize, projectileSize, 1f);
        this.GetComponent<SpriteRenderer>().sprite = skillSprite;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        IDamageable dmg = collision.GetComponent<IDamageable>();

        if (dmg != null)
        {
            dmg.TakeDamage(damage);

            currentPierce++;
            if(currentPierce > projectilePierce)
            {
                Game._skillManager.DestroyProjectile(this.gameObject);
            }
        }
        //Debug.Log($"Pierce is {projectilePierce}, number of targets pierced is {pierced}");
    }
    private void OnEnable()
    {
        currentPierce = 0;
    }
}
