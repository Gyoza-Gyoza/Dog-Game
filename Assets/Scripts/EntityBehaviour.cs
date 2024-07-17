using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using static UnityEngine.GraphicsBuffer;

public class EntityBehaviour : MonoBehaviour, IDamageable
{
    protected int
        hp,
        attack,
        magicAttack,
        movementSpeed,
        armor,
        magicResist;

    protected string
        entitySprite;

    protected NavMeshAgent
        nav;

    public int _hp
    { get { return hp; } }

    public int _attack
    { get { return attack; } }

    public int _magicAttack
    { get { return magicAttack; } }

    public int _movementSpeed
    { get { return movementSpeed; } }

    public int _armor
    { get { return armor; } }

    public int _magicResist
    { get { return magicResist; } }

    private void Awake()
    {
        if (nav == null)
        {
            nav = GetComponent<NavMeshAgent>();
        }
        //nav.updateRotation = false;
        //nav.updateUpAxis = false;
    }
    public void ChangeDestination(Vector3 target)
    {
        nav.SetDestination(target);
    }

    public void TakeDamage(int damage)
    {
        hp -= damage;
        if (hp < 0)
        {
            Death();
        }
        Debug.Log($"Dealt {damage}, {hp} hp left");
    }
    private void Death()
    {
        Game._enemyFactory.DestroyEnemy(this.gameObject);
    }
}
