using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using static UnityEngine.GraphicsBuffer;

public class EnemyBehaviour : EntityBehaviour
{
    private GameObject
        target;

    private int
        goldDrop;

    public int _goldDrop
    { get {  return goldDrop; } }

    private void Start()
    {
        target = Game._player.gameObject;
    }
    private void Update()
    {
        if(target != null)
        {
            ChangeDestination(target.transform.position);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            collision.GetComponent<IDamageable>().TakeDamage(attack);
        }
    }
    public override void TakeDamage(int damage)
    {
        int finalDamage = (int)(damage * Game.CalculateDamageReduction(this.defence));
        currentHp -= finalDamage;
        AnalyticsManager._damageDealt += finalDamage;
        if (currentHp < 0)
        {
            Death();
        }
    }
    protected override void Death()
    {
        Game._enemyFactory.DestroyEnemy(gameObject);
        Game._inventoryManager.GainGold(goldDrop);
    }
    public void SetStats(string enemyName, int hp, int attack, int movementSpeed, int defence, string enemySprite, int goldDrop)
    {
        name = enemyName;
        this.hp = hp;
        this.currentHp = hp;
        this.attack = attack;
        this.movementSpeed = movementSpeed;
        this.defence = defence;
        AssetManager.SetSprite(enemySprite, this.gameObject);
        this.goldDrop = goldDrop;
    }
}
