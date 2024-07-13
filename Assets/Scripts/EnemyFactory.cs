using System;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.EventSystems.EventTrigger;

public class EnemyFactory : MonoBehaviour
{
    [SerializeField]
    private GameObject
        enemyBase; 

    private Dictionary<EnemyTypes, Stack<GameObject>>
        enemyPool = new Dictionary<EnemyTypes, Stack<GameObject>>();

    private void Awake()
    {
        if(Game._enemyFactory == null)
        {
            Game._enemyFactory = this;
        }
    }
    private void Start()
    {
        for (int i = 0; i < Enum.GetNames(typeof(EnemyTypes)).Length; i++)
        {
            enemyPool.Add((EnemyTypes)i, new Stack<GameObject>());
        }
    }
    public GameObject GetEnemy(EnemyTypes type)
    {
        GetPoolStack(type).TryPop(out GameObject result);
        if (result != null)
        {
            result.SetActive(true);
            return result;
        }
        else
        {
            return CreateObject(type);
        }
    }
    public void DestroyEnemy(GameObject objToDestroy)
    {
        enemyPool.TryGetValue(objToDestroy.GetComponent<Enemy>()._enemyType, out Stack<GameObject> result);
        objToDestroy.SetActive(false);
        result.Push(objToDestroy);
    }
    private Stack<GameObject> GetPoolStack(EnemyTypes type)
    {
        enemyPool.TryGetValue(type, out Stack<GameObject> stack);
        return stack;
    }
    private GameObject CreateObject(EnemyTypes type)
    {
        GameObject chosenEnemy = Instantiate(enemyBase);
        foreach (Enemy enemy in Game._database._enemyList)
        {
            if (enemy._enemyType == type)
            {
                chosenEnemy.GetComponent<EnemyBehaviour>().SetStats(enemy._name, enemy._hp, enemy._attack, enemy._attackSpeed, enemy._movementSpeed, enemy._enemyType, enemy._enemySprite); 
                chosenEnemy.name = enemy._name;
                Debug.Log($"Spawning one {enemy._name}, renaming to {chosenEnemy.name}");
                break;
            }
        }
        return chosenEnemy;
    }

}

