using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public static class Game
{
    #region References
    //This script connects non static scripts together, it returns references to scripts
    //when the right function is called
    private static PlayerBehaviour
        player;

    private static Database
        database;

    private static EnemyFactory
        enemyFactory;

    private static InputHandler
        inputHandler;

    private static GameSceneManager
        gameSceneManager;

    private static AugmentManager
        augmentManager;

    private static LootManager
        lootManager;

    private static Cursor
        cursor;

    private static PlayerManager
        playerManager;

    private static InventoryManager
        inventoryManager;

    private static SkillManager
        skillManager;

    private static WaveManager
        waveManager;

    private static TabManager
        tabManager;

    private static DialogueManager
        dialogueManager;

    private static AnalyticsUIManager
        analyticsUIManager;
    public static PlayerBehaviour _player
    { get { return player; } set { player = value; } }

    public static Database _database
    { get { return database; } set { database = value; } }

    public static EnemyFactory _enemyFactory
    { get { return enemyFactory; } set { enemyFactory = value; } }

    public static InputHandler _inputHandler
    { get { return inputHandler; } set { inputHandler = value; } }

    public static GameSceneManager _gameSceneManager
    { get { return gameSceneManager; } set { gameSceneManager = value; } }

    public static AugmentManager _augmentManager
    { get { return augmentManager; } set { augmentManager = value; } }

    public static LootManager _lootManager
    { get { return lootManager; } set { lootManager = value; } }

    public static Cursor _cursor
    { get { return cursor; } set { cursor = value; } }

    public static PlayerManager _playerManager
    { get { return playerManager; } set { playerManager = value; } }

    public static InventoryManager _inventoryManager
    { get { return inventoryManager; } set { inventoryManager = value; } }

    public static SkillManager _skillManager
    { get { return skillManager; } set { skillManager = value; } }

    public static WaveManager _waveManager
    { get { return waveManager; } set { waveManager = value; } }

    public static TabManager _tabManager
    { get { return tabManager; } set { tabManager = value; } }

    public static DialogueManager _dialogueManager
    { get { return dialogueManager; } set { dialogueManager = value; } }

    public static AnalyticsUIManager _analyticsUIManager
    { get { return analyticsUIManager; } set { analyticsUIManager = value; } }
    #endregion
    #region Objects
    private static Player
        chosenPlayer;

    private static UIManager
        uIManager;

    private static List<ShopMenu>
        shopMenus = new List<ShopMenu>();

    public static Player _chosenPlayer
    { get { return chosenPlayer; } set { chosenPlayer = value; } }

    public static UIManager _uIManager
    { get { return uIManager; } set { uIManager = value; } }

    public static List<ShopMenu> _shopMenus
    { get { return shopMenus; } }
    public static void AddToShopMenus(ShopMenu go)
    {
        shopMenus.Add(go);
    }
    #endregion
    public static float CalculateDamageReduction(int defence)
    {
        return (float)defence / (defence + 100);
    }
}
