using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using static UnityEngine.GraphicsBuffer;

public class EnemyBehaviour : MonoBehaviour
{
    private string
        entityName;

    private int
        hp,
        attack,
        attackSpeed,
        movementSpeed; 
    
    private EnemyTypes
        enemyType;

    private NavMeshAgent
        nav;

    private SpriteRenderer
        spriteRenderer;

    private GameObject
        target;

    private void Start()
    {
        nav = GetComponent<NavMeshAgent>();
        nav.updateRotation = false;
        nav.updateUpAxis = false;
        target = Game._player.gameObject;

        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    private void Update()
    {
        nav.SetDestination(target.transform.position);
    }
    public void SetStats(string enemyName, int hp, int attack, int attackSpeed, int movementSpeed, EnemyTypes enemyType, string enemySprite)
    {
        this.entityName = enemyName;
        this.hp = hp;
        this.attack = attack;
        this.attackSpeed = attackSpeed;
        this.movementSpeed = movementSpeed;
        this.enemyType = enemyType;
        AssetManager.SetSprite(enemySprite, this.gameObject);
    }

    public void ChangeDestination(GameObject newTarget)
    {
        target = newTarget;
    }
}
