using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using static UnityEngine.GraphicsBuffer;

public class EnemyBehaviour : MonoBehaviour
{
    private int
        hp,
        attack,
        magicAttack,
        movementSpeed,
        armor,
        magicResist;

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
    public void SetStats(string enemyName, int hp, int attack, int magicAttack, int movementSpeed, int armor, int magicResist, string enemySprite)
    {
        this.name = enemyName;
        this.hp = hp;
        this.attack = attack;
        this.magicAttack = magicAttack;
        this.movementSpeed = movementSpeed;
        this.armor = armor;
        this.magicResist = magicResist;
        AssetManager.SetSprite(enemySprite, this.gameObject);
    }

    public void ChangeDestination(GameObject newTarget)
    {
        target = newTarget;
    }
}
