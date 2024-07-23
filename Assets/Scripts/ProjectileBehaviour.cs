using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ProjectileBehaviour : SkillBehaviour
{
    //Stores the stats of the skill
    private int
        damage, 
        projectilePierce = 2, 
        currentPierce = 0;

    private float
        projectileSpeed = 30f, 
        timer = 0f;

    private ParticleSystem
        ps;

    //Getters and setters for interacting with the variables
    public int _damage
    { get { return damage; } set { damage = value; } }

    public int _currentPierce
    { get { return currentPierce; } set { currentPierce = value; } }

    private void Awake()
    {
        ps = GetComponent<ParticleSystem>();
    }
    private void Update()
    {
        transform.localPosition += transform.TransformDirection(0f, projectileSpeed * Time.deltaTime, 0f); //Moves the projectile along its y axis over time
        
        //Places the object in the pool after a set amount of time
        if(timer < 1f)
        {
            timer += Time.deltaTime; 

            if (timer >= 1f)
            {
                Game._skillManager.DestroySpell(this.gameObject);
                timer = 0f;
            }
        }
    }
    //Sets the stats of the skill
    public void SetStats(int damage, int projectilePierce, int projectileSpeed, float projectileSize)
    {
        this.damage = damage;
        this.projectilePierce = projectilePierce;
        this.projectileSpeed = projectileSpeed;
        this.gameObject.transform.localScale = new Vector3(projectileSize, projectileSize, 1f);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag != "Player")
        {
            IDamageable dmg = collision.GetComponent<IDamageable>(); //Stores the interface to reduce getcomponent calls

            if (dmg != null) //Checks if the entity the skill collides with has the interface on it
            {
                dmg.TakeDamage(damage); //Deals damage to the enemy holding on to the interface

                currentPierce++; //Keeps track of how many enemies the projectile pierced 
                if (currentPierce > projectilePierce)
                {
                    Game._skillManager.DestroySpell(this.gameObject); //Places the object in the pool after it pierces enemies based on its defined pierce

                    if (spellEffects != null) //Checks if the delegate is null
                    {
                        spellEffects.Invoke(); //Calls the delegate that contains any extra effects 
                    }
                }
            }
        }
    }
    //Assigns effects to the delegate
    public void SetSpellEffects(UnityAction action)
    {
        spellEffects = new SpellEffects(action);
    }
    //Resets its pierce when the projectile is spawned
    private void OnEnable()
    {
        currentPierce = 0;
        ps.Stop();
        ps.Play();
    }
    //Disabling the particle system when its done and placing it in the pool
    private void OnDisable()
    {
        Game._skillManager.DestroySpell(this.gameObject);
    }
}
