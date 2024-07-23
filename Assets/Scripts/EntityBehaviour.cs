using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using static UnityEngine.GraphicsBuffer;

public class EntityBehaviour : MonoBehaviour, IDamageable
{
    protected int
        hp,
        currentHp,
        attack,
        movementSpeed,
        defence;

    protected string
        entitySprite;

    protected NavMeshAgent
        nav;

    public int _hp
    { get { return hp; } }

    public int _currentHp
    { get { return currentHp; } }

    public int _attack
    { get { return attack; } }

    public int _movementSpeed
    { get { return movementSpeed; } }

    public int _defence
    { get { return defence; } }

    public NavMeshAgent _nav
    { get { return nav; } }

    private void Awake()
    {
        if (nav == null)
        {
            nav = GetComponent<NavMeshAgent>();
        }
    }
    public virtual void ChangeDestination(Vector3 target)
    {
        nav.SetDestination(target);
        if(transform.position.x > target.x)
        {
            GetComponent<SpriteRenderer>().flipX = true;
        }
        else
        {
            GetComponent<SpriteRenderer>().flipX = false;
        }
    }

    public void TakeDamage(int damage)
    {
        currentHp -= damage;
        if (currentHp < 0)
        {
            Death();
        }
    }
    protected virtual void Death()
    {

    }
    public void ResetHealth()
    {
        currentHp = hp;
    }
}
