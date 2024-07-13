using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerBehaviour : MonoBehaviour, IInputReceiver
{
    private string
        entityName;

    private int
        hp,
        attack,
        attackSpeed,
        movementSpeed;
    private Vector2 moveDir;

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
        Entity chosenEntity = Game._database._playerClassList[0];
        SetStats(chosenEntity._name, chosenEntity._hp, chosenEntity._attack, chosenEntity._attackSpeed, chosenEntity._movementSpeed);
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
    public virtual void SetStats(string name, int hp, int attack, int attackSpeed, int movementSpeed)
    {
        this.entityName = name;
        this.hp = hp;
        this.attack = attack;
        this.attackSpeed = attackSpeed;
        this.movementSpeed = movementSpeed;
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
