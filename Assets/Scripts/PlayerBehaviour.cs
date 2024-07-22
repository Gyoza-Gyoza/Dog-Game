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

    public static PlayerBehaviour
        player;

    private bool
        disableMovement = false;

    private Dictionary<EquipmentSlot, Equipment>
        equipmentList = new Dictionary<EquipmentSlot, Equipment>();

    public int _attackSpeed
    { get { return player.attackSpeed; } }

    public bool _disableMovement
    { get { return player.disableMovement; } set { player.disableMovement = value; } }

    public Dictionary<EquipmentSlot, Equipment> _equipmentList
    { get { return equipmentList; } }

    private void Awake()
    {
        //Singleton to ensure there is only one player in the game at all times 
        if(player != null && player != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            player = this;
            Game._player = this; //Giving a reference to the static Game class
        }
    }
    public override void ChangeDestination(Vector3 target)
    {
        if(!disableMovement)
        {
            nav.SetDestination(target);
        }
    }
    public void SetStats(string entityName, int hp, int attack, int magicAttack, int movementSpeed, int armor, int magicResist, string entitySprite, int attackSpeed, string weaponType)
    {
        nav = GetComponent<NavMeshAgent>();
        //Add attack range
        //GetComponentInChildren<CircleCollider2D>().radius = attackRange;
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
