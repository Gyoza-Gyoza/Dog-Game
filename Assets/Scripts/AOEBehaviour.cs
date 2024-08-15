using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

//DONE BY WANG JIA LE
public class AOEBehaviour : SkillBehaviour
{
    private int
        damage; //Stores the damage of the skill 

    //Getters and setters for interacting with the variables
    public int _damage
    {  get { return damage; } set { damage = value; } } 
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag != "Player")
        {
            IDamageable dmg = collision.GetComponent<IDamageable>(); //Stores the interface to reduce getcomponent calls

            if (dmg != null) //Checks if the entity the skill collides with has the interface on it
            {
                dmg.TakeDamage(damage); //Deals damage to the enemy holding on to the interface

                if (spellEffects != null) //Checks if the delegate is null
                {
                    spellEffects.Invoke(); //Calls the delegate that contains any extra effects 
                }
            }
        }
    }
    //Assigns effects to the delegate
    public void SetSpellEffects(UnityAction action)
    {
        spellEffects = new SpellEffects(action);
    }
    //Sets the stats of the skill
    public void SetStats(int damage, float size)
    {
        this.damage = damage;
        this.transform.localScale = new Vector3(size, size, 1f);
    }
    //Disabling the particle system when its done and placing it in the pool
    private void OnDisable()
    {
        Game._skillManager.DestroySpell(this.gameObject);
    }
}
