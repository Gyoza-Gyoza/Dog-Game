using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerBehaviour : EntityBehaviour
{
    protected string
        weaponType;

    protected int
        attackSpeed; 

    private Vector2
        moveDir;

    private static PlayerBehaviour
        instance;

    private void Awake()
    {
        //Singleton to ensure there is only one player in the game at all times 
        if(instance != null && instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            instance = this;
            Game._player = this; //Giving a reference to the static Game class
        }
    }

    //public void LeagueMovement(Vector3 pos)
    //{
    //    nav.SetDestination(pos);
    //}
    public void DoMoveDir(Vector2 aDir)
    {
        //set movement direction
        moveDir = aDir;

        //Getting a reference to the rigidbody
        Rigidbody2D rb = GetComponent<Rigidbody2D>();

        //Normalizing the vector to make the diagonal movement consistent with the horizontal and vertical movement
        moveDir.Normalize();

        Vector2 nextPos = rb.position + moveDir * movementSpeed * Time.fixedDeltaTime;
        rb.MovePosition(nextPos);

        //Rotating the player towards the direction they're moving towards 
        if (aDir != Vector2.zero)
        {
            this.transform.up = moveDir;
        }
    }
    public void SetStats(string entityName, int hp, int attack, int magicAttack, int movementSpeed, int armor, int magicResist, string entitySprite, int attackSpeed, string weaponType)
    {
        nav = GetComponent<NavMeshAgent>();
        this.name = entityName;
        this.hp = hp;
        this.attack = attack;
        this.magicAttack = magicAttack;
        nav.speed = movementSpeed;
        this.armor = armor;
        this.magicResist = magicResist;
        this.entitySprite = entitySprite;
        this.attackSpeed = attackSpeed;
        this.weaponType = weaponType;
    }
}
