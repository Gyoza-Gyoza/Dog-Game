using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InputHandler : MonoBehaviour
{
    private IInputReceiver activeReceiver;
    private void Awake()
    {
        Game._inputHandler = this;
    }
    public void SetInputReceiver(IInputReceiver inputReceiver)
    {
        //set current input receiver (to control 1 thing at a time)
        activeReceiver = inputReceiver;
    }
    private void FixedUpdate()
    {
        Movement(); 
    }
    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Alpha1))
        {
            //Game._gameSceneManager.OpenScene("Stage2");
            Game._enemyFactory.GetEnemy(EnemyTypes.Basic);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            Game._enemyFactory.GetEnemy(EnemyTypes.Elite);
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            Game._enemyFactory.GetEnemy(EnemyTypes.Boss);
        }
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            Game._enemyFactory.DestroyEnemy(GameObject.FindGameObjectWithTag("Enemy"));
        }

        GetMovementInput();
    }
    private void Movement()
    {
        float horDir = 0f;
        float vertDir = 0f;

        if (Input.GetAxis("Horizontal") > 0)
        {
            horDir = 1f;
        }
        else if (Input.GetAxis("Horizontal") < 0)
        {
            horDir = -1f;
        }

        if (Input.GetAxis("Vertical") > 0)
        {
            vertDir = 1f;
        }
        else if (Input.GetAxis("Vertical") < 0)
        {
            vertDir = -1f;
        }

        activeReceiver.DoMoveDir(new Vector2(horDir, vertDir));
    }
    private void GetMovementInput()
    {
        if (Input.GetButtonDown("Horizontal"))
        {
            if (Input.GetAxisRaw("Horizontal") > 0)
            {
                activeReceiver.DoRightAction();
            }
            else if (Input.GetAxisRaw("Horizontal") < 0)
            {
                activeReceiver.DoLeftAction();
            }
        }
        else if (Input.GetButtonDown("Submit"))
        {
            activeReceiver.DoSubmitAction();
        }
        else if (Input.GetButtonDown("Cancel"))
        {
            activeReceiver.DoCancelAction();
        }
    }
}
