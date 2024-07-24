using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AI;

public class PlayerBehaviour : EntityBehaviour
{
    protected string
        projectileType,
        classHurtSprite;

    protected int
        attackSpeed, 
        critChance,
        dashSpeed,
        dashDuration;

    protected WaitForSeconds
        iFrameDuration = new WaitForSeconds(1f);

    private Vector2
        moveDir;

    public static PlayerBehaviour
        player;

    private bool
        disableMovement = false,
        damageImmune = false;

    public string _projectileType
    { get { return projectileType; } }
    public int _attackSpeed
    { get { return player.attackSpeed; } }

    public bool _disableMovement
    { get { return player.disableMovement; } set { player.disableMovement = value; } }

    //public Dictionary<EquipmentSlot, Equipment> _equipmentList
    //{ get { return equipmentList; } }

    private void Awake()
    {
        //Singleton to ensure there is only one player in the game at all times 
        if(player != null && player != this)
        {
            Destroy(this.gameObject);
            return;
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
            if (transform.position.x > target.x)
            {
                GetComponent<SpriteRenderer>().flipX = true;
            }
            else
            {
                GetComponent<SpriteRenderer>().flipX = false;
            }
        }
    }
    public override void TakeDamage(int damage)
    {
        if(!damageImmune)
        {
            currentHp -= (int)(damage * Game.CalculateDamageReduction(defence));
            //Debug.Log($"Took {damage * Game.CalculateDamageReduction(defence)}");

            StartCoroutine(IFrame());

            if (currentHp < 0)
            {
                Death();
            }
        }
    }
    private IEnumerator IFrame()
    {
        damageImmune = true;

        yield return iFrameDuration;

        damageImmune = false;
    }
    public void Respawn()
    {
        currentHp = hp;
        Game._gameSceneManager.OpenScene("Town", false, null);
    }
    protected override void Death()
    {
        Respawn();
    }
    public void SetStats(string entityName, int hp, int attack, int movementSpeed, int defence, string entitySprite, int attackSpeed, int attackRange, int critChance, string projectileType, string classHurtSprite, int dashSpeed, float dashDuration)
    {
        nav = GetComponent<NavMeshAgent>();
        name = entityName;
        this.hp = hp;
        this.attack = attack;
        nav.speed = movementSpeed;
        this.defence = defence;
        AssetManager.SetSprite(entitySprite, gameObject);
        this.attackSpeed = attackSpeed;
        GetComponentInChildren<CircleCollider2D>().radius = attackRange;
        this.critChance = critChance;
        this.projectileType = projectileType;
        this.classHurtSprite = classHurtSprite;
        Game._skillManager._dashSpeed = dashSpeed;
        Game._skillManager._dashDuration = dashDuration;
        ResetHealth();
    }
}
