using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerBehaviour : MonoBehaviour, IInputReceiver
{
    [SerializeField]
    protected string
        entityId,
        entityName,
        weaponType;

    [SerializeField]
    protected int
        hp,
        attack,
        magicAttack,
        movementSpeed,
        armor,
        magicResist,
        attackSpeed; 

    [SerializeField]
    protected string
        entitySprite;

    private Vector2
        moveDir;

    private NavMeshAgent
        nav; 

    private void Awake()
    {
        if (Game._player == null)
        {
            Game._player = this;
        }
    }

    void Start()
    {
        Player chosenEntity = Game._database._playerClassList[0] as Player;
        SetStats(chosenEntity._entityId, chosenEntity._name, chosenEntity._hp, chosenEntity._attack, chosenEntity._magicAttack, chosenEntity._movementSpeed, chosenEntity._armor, chosenEntity._magicResist, chosenEntity._entitySprite, chosenEntity._attackSpeed, chosenEntity._weaponType);
        Game._inputHandler.SetInputReceiver(this);
        nav = GetComponent<NavMeshAgent>();
    }
    public void LeagueMovement(Vector3 pos)
    {
        nav.SetDestination(pos);
    }
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
    public virtual void SetStats(string entityId, string entityName, int hp, int attack, int magicAttack, int movementSpeed, int armor, int magicResist, string entitySprite, int attackSpeed, string weaponType)
    {
        this.entityId = entityId;
        this.entityName = entityName;
        this.hp = hp;
        this.attack = attack;
        this.magicAttack = magicAttack;
        this.movementSpeed = movementSpeed;
        this.armor = armor;
        this.magicResist = magicResist;
        this.entitySprite = entitySprite;
        this.attackSpeed = attackSpeed;
        this.weaponType = weaponType;
    }
    public void DoLeftAction()
    {

    }
    public void DoRightAction()
    {

    }
    public void DoSubmitAction()
    {

    }
    public void DoCancelAction()
    {

    }
}
