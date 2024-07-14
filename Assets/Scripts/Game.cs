using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public static class Game
{
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

    public static PlayerBehaviour _player
    { get { return player; }
        set { player = value; } }

    public static Database _database
    { get { return database; } 
        set { database = value; } }

    public static EnemyFactory _enemyFactory
    { get { return enemyFactory; } 
        set { enemyFactory = value; } }

    public static InputHandler _inputHandler
    { get { return inputHandler; } 
        set { inputHandler = value; } }

    public static GameSceneManager _gameSceneManager
    { get { return gameSceneManager; } 
        set { gameSceneManager = value; } }

    public static AugmentManager _augmentManager
    { get { return augmentManager; }
        set { augmentManager = value; } }

    public static LootManager _lootManager
    { get { return lootManager; }
        set { lootManager = value; } }
    public static Cursor _cursor
    { get { return cursor; }
        set { cursor = value; } }
}
