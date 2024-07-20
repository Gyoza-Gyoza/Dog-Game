using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using static UnityEngine.GraphicsBuffer;

public class EnemyBehaviour : EntityBehaviour
{
    private GameObject
        target;

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
    public void SetStats(string enemyName, int hp, int attack, int magicAttack, int movementSpeed, int armor, int magicResist, string enemySprite)
    {
        this.name = enemyName;
        this.hp = hp;
        this.currentHp = hp;
        this.attack = attack;
        this.magicAttack = magicAttack;
        this.movementSpeed = movementSpeed;
        this.armor = armor;
        this.magicResist = magicResist;
        AssetManager.SetSprite(enemySprite, this.gameObject);
    }
}
