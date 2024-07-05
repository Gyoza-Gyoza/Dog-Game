using Microsoft.Unity.VisualStudio.Editor;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public enum EnemyTypes
{
    Basic, 
    Elite, 
    Boss
}
[System.Serializable]
public class Enemy : Entity
{
    [SerializeField]
    private EnemyTypes
        enemyType;

    private NavMeshAgent
        nav;

    private SpriteRenderer
        spriteRenderer;

    public EnemyTypes
        _enemyType
    {
        get { return enemyType; }
    }

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

    public Enemy(string name, int hp, int attack, int attackSpeed, int movementSpeed, EnemyTypes enemyType)
    {
        this.entityName = name;
        this.hp = hp;
        this.attack = attack;
        this.attackSpeed = attackSpeed;
        this.movementSpeed = movementSpeed;
        this.enemyType = enemyType;
    }

    public void SetEnemyStats(string enemyName, int hp, int attack, int attackSpeed,int movementSpeed, EnemyTypes enemyType)
    {
        this.entityName = enemyName;
        this.hp = hp;
        this.attack = attack;
        this.attackSpeed = attackSpeed;
        this.movementSpeed = movementSpeed;
        this.enemyType = enemyType;
    }

    public void ChangeDestination(GameObject newTarget)
    {
        target = newTarget;
    }
}
