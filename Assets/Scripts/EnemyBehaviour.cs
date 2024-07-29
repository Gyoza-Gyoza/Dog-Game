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

    private delegate void SpecialEffect(); 
    private SpecialEffect specialEffect = null;
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
        specialEffect?.Invoke();
    }
    public void SetStats(string enemyId, string enemyName, int hp, int attack, int movementSpeed, int defence, string enemySprite, int goldDrop)
    {
        name = enemyName;
        this.hp = hp;
        this.currentHp = hp;
        this.attack = attack;
        this.movementSpeed = movementSpeed;
        this.defence = defence;
        AssetManager.SetSprite(enemySprite, this.gameObject);
        this.goldDrop = goldDrop;

        if (enemyId.Substring(0,4) == "BOSS")
        {
            specialEffect += () =>
            {
                Debug.Log("Effect added");
                EndWave();
            };
        }
    }
    private void EndWave()
    {
        Debug.Log("Door Opened");
        GameObject.FindGameObjectWithTag("Portal").GetComponent<BoxCollider2D>().enabled = true;
        Game._waveManager._startWave = false;

        switch (name)
        {
            case "RHINO BEETLE":
                Game._currentK9Dialogue = "D00009";
                break;

            case "STORM GIANT":
                Game._currentK9Dialogue = "D00014";
                break;

            case "ADEPT NECROMANCER":
                Game._currentK9Dialogue = "D00018";
                break;

            case "WARP SKULL":
                Game._currentK9Dialogue = "D00022";
                break;
        }
    }
}
