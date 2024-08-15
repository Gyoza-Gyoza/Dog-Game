using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class InputHandler : MonoBehaviour
{
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
        if (Input.GetKeyDown(KeyCode.I))
        {
            if(Game._tabManager.gameObject.activeInHierarchy)
            {
                return;
            }
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
        if(Input.GetKeyDown(KeyCode.S))
        {
            Game._player.ChangeDestination(Game._player.transform.position);
        }
        //if(Input.GetKeyDown(KeyCode.P))
        //{
        //    Game._inventoryManager.GainGold(100);
        //    Game._inventoryManager.UpdateInventory();
        //}
        if (Input.GetKeyDown(KeyCode.F))
        {
            Game._player.Interact();
        }
        //if (Input.GetKeyDown(KeyCode.Z))
        //{
        //    Game._enemyFactory.DestroyAllEnemies();
        //}
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
    
    private void UseSkills()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {

        }
    }
}