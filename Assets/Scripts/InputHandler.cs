using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class InputHandler : MonoBehaviour
{
    private IInputReceiver activeReceiver;
    private bool
        menuOpen = false;
    private enum Menus
    {
        Augments, 
        Equipment
    }
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
        Game._player.LeagueMovement(Game._cursor._mousePos);
        Game._cursor.SpawnArrowIndicator(Game._cursor._mousePos);
    }
    private void GetKeyInput()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            Game._enemyFactory.GetEnemy(Game._database._enemyList[0]._name);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            Game._enemyFactory.GetEnemy(Game._database._enemyList[1]._name);
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            Game._enemyFactory.GetEnemy(Game._database._enemyList[2]._name);
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
    private void ToggleMenus(Menus menuToOpen)
    {
        switch (menuToOpen)
        {
            case Menus.Augments:
                if(!menuOpen)
                {
                    Time.timeScale = 0f;
                    Game._gameSceneManager.OpenScene("AugmentsMenu", true, () =>
                    {
                        Game._augmentManager.InitializeList();
                        Game._augmentManager.SetAugment();
                    });
                    menuOpen = true;
                }
                else
                {
                    Time.timeScale = 1f;
                    Game._gameSceneManager.CloseScene("AugmentsMenu");
                    menuOpen = false;
                }
                break;

            case Menus.Equipment:
                if (!menuOpen)
                {
                    Time.timeScale = 0f;
                    Game._gameSceneManager.OpenScene("PlayerEquipmentMenu", true, null);
                    menuOpen = true;
                }
                else
                {
                    Time.timeScale = 1f;
                    Game._gameSceneManager.CloseScene("PlayerEquipmentMenu");
                    menuOpen = false;
                }
                break;
        }
    }
}