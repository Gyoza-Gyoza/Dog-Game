using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class InputHandler : MonoBehaviour
{
    private IInputReceiver activeReceiver; //GET RID OF THIS
    private enum Menus
    {
        None, 
        Augments, 
        Equipment
    }

    private Menus
        activeScene = Menus.None; 

    private void Awake()
    {
        if(Game._inputHandler == null)
        {
            Game._inputHandler = this;
        }
    }
    public void SetInputReceiver(IInputReceiver inputReceiver)
    {
        //set current input receiver (to control 1 thing at a time)
        activeReceiver = inputReceiver;
    }
    private void FixedUpdate()
    {
        //Movement(); 
    }
    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(1))
        {
            SetClickDestination();
        }
        GetKeyInput();
        //GetMovementInput();
    }
    private void SetClickDestination()
    {
        if(Time.timeScale > 0f)
        {
            Game._player.ChangeDestination(Game._cursor._mousePos);
            Game._cursor.SpawnArrowIndicator(Game._cursor._mousePos);
        }
    }
    private void GetKeyInput()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            Game._enemyFactory.GetEnemy(Game._database._enemyList[0]._name, transform);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            Game._enemyFactory.GetEnemy(Game._database._enemyList[1]._name, transform);
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            Game._enemyFactory.GetEnemy(Game._database._enemyList[2]._name, transform);
        }
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            Game._enemyFactory.DestroyEnemy(GameObject.FindGameObjectWithTag("Enemy")); 
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ToggleMenus(Menus.Augments);
        }
        if (Input.GetKeyDown(KeyCode.I))
        {
            ToggleMenus(Menus.Equipment);
        }
        if (Input.GetKeyDown(KeyCode.F1))
        {
            Game._lootManager.DropLoot(Game._player.transform, Game._database._itemList[1]);
        }
        if (Input.GetKeyDown(KeyCode.Z))
        {
            Game._playerManager.SpawnPlayer(gameObject.transform);
        }
        if(Input.GetKeyDown(KeyCode.S))
        {
            Game._player.ChangeDestination(Game._player.transform.position);
        }
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
    private void ToggleMenus(Menus menuToToggle)
    {
        switch (menuToToggle)
        {
            case Menus.Augments:
                if (activeScene == Menus.None)
                {
                    activeScene = Menus.Augments;
                    Game._gameSceneManager.OpenScene("AugmentsMenu", true, () =>
                    {
                        Game._augmentManager.InitializeList();
                        Game._augmentManager.SetAugment();
                    });
                }
                else if (activeScene == Menus.Augments)
                {
                    Game._gameSceneManager.CloseScene("AugmentsMenu");
                    activeScene = Menus.None;
                }
                break;

            case Menus.Equipment:
                if (activeScene == Menus.None)
                {
                    activeScene = Menus.Equipment;
                    Game._gameSceneManager.OpenScene("PlayerEquipmentMenu", true, null);
                }
                else if(activeScene == Menus.Equipment)
                {
                    Game._gameSceneManager.CloseScene("PlayerEquipmentMenu"); 
                    activeScene = Menus.None;
                }
                break;
        }
    }
}