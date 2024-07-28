using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class InputHandler : MonoBehaviour
{
    private enum Menus
    {
        None, 
        Augments, 
        Equipment
    }

    private Menus
        activeScene = Menus.None;

    private bool
        disableMovement = false;

    public bool _disableMovement
    {  get { return disableMovement; } set { disableMovement = value; } }

    private void Awake()
    {
        if(Game._inputHandler == null)
        {
            Game._inputHandler = this;
        }
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
        if(Time.timeScale > 0f && !disableMovement)
        {
            Game._player.ChangeDestination(Game._cursor._mousePos);
            Game._cursor.SpawnArrowIndicator(Game._cursor._mousePos);
        }
    }
    private void GetKeyInput()
    {
        //if (Input.GetKeyDown(KeyCode.Alpha1))
        //{
        //    Game._enemyFactory.GetEnemy("ENEM00001", transform);
        //}
        //if (Input.GetKeyDown(KeyCode.Alpha2))
        //{
        //    Game._enemyFactory.GetEnemy("ENEM00002", transform);
        //}
        //if (Input.GetKeyDown(KeyCode.Alpha3))
        //{
        //    Game._enemyFactory.GetEnemy("ENEM00003", transform);
        //}
        if (Input.GetKeyDown(KeyCode.I))
        {
            Game._uIManager.ToggleInventory();
            if(Time.timeScale == 0f)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
                Game._inventoryManager.UpdateInventory();
            }
        }
        if(Input.GetKeyDown(KeyCode.O))
        {
            Game._uIManager.ToggleShop();
            if (Time.timeScale == 0f)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
                Game._inventoryManager.UpdateInventory();
                Game._inventoryManager.UpdateShopMenu();
            }
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
        if(Input.GetKeyDown(KeyCode.P))
        {
            Game._inventoryManager.GainGold(100);
        }
        if (Input.GetKeyDown(KeyCode.F))
        {
            Game._inventoryManager.GainGold(100);
        }
    }
    public void PauseGame()
    {
        Time.timeScale = 0f;
        disableMovement = true;
    }
    public void ResumeGame()
    {
        Time.timeScale = 1f; 
        disableMovement = false;
    }
    //private void Movement()
    //{
    //    float horDir = 0f;
    //    float vertDir = 0f;

    //    if (Input.GetAxis("Horizontal") > 0)
    //    {
    //        horDir = 1f;
    //    }
    //    else if (Input.GetAxis("Horizontal") < 0)
    //    {
    //        horDir = -1f;
    //    }

    //    if (Input.GetAxis("Vertical") > 0)
    //    {
    //        vertDir = 1f;
    //    }
    //    else if (Input.GetAxis("Vertical") < 0)
    //    {
    //        vertDir = -1f;
    //    }

    //    activeReceiver.DoMoveDir(new Vector2(horDir, vertDir));
    //}
    //private void GetMovementInput()
    //{
    //    if (Input.GetButtonDown("Horizontal"))
    //    {
    //        if (Input.GetAxisRaw("Horizontal") > 0)
    //        {
    //            activeReceiver.DoRightAction();
    //        }
    //        else if (Input.GetAxisRaw("Horizontal") < 0)
    //        {
    //            activeReceiver.DoLeftAction();
    //        }
    //    }
    //    else if (Input.GetButtonDown("Submit"))
    //    {
    //        activeReceiver.DoSubmitAction();
    //    }
    //    else if (Input.GetButtonDown("Cancel"))
    //    {
    //        activeReceiver.DoCancelAction();
    //    }
    //}
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
    private void UseSkills()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {

        }
    }
}