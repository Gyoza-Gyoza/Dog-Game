using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public static class Game
{
    //This script connects non static scripts together, it returns references to scripts
    //when the right function is called
    private static Player
        player;

    private static Database
        database;

    private static EnemyFactory
        enemyFactory;

    private static InputHandler
        inputHandler;

    private static GameSceneManager
        gameSceneManager;

    public static Player
        _player
    { 
        get { return player; } 
        set { player = value; } 
    }

    public static Database 
        _database
    { 
        get { return database; } 
        set { database = value; } 
    }
    public static EnemyFactory
        _enemyFactory
    { 
        get { return enemyFactory; } 
        set { enemyFactory = value; } 
    }

    public static InputHandler 
        _inputHandler
    { 
        get { return inputHandler; } 
        set { inputHandler = value; } 
    }

    public static GameSceneManager
        _gameSceneManager
    { 
        get { return gameSceneManager; } 
        set { gameSceneManager = value; } 
    }
}
