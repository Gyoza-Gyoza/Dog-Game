using System;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.EventSystems.EventTrigger;

public class EnemyFactory : MonoBehaviour
{
    [SerializeField]
    private GameObject
        enemyBase; 

    private Dictionary<string, Stack<GameObject>>
        enemyPool = new Dictionary<string, Stack<GameObject>>();

    private void Awake()
    {
        if(Game._enemyFactory == null)
        {
            Game._enemyFactory = this;
        }
    }
    
    public GameObject GetEnemy(string enemyToSpawn)
    {
        GetPoolStack(enemyToSpawn).TryPop(out GameObject result);
        if (result != null)
        {
            result.SetActive(true);
            return result;
        }
        else
        {
            return CreateObject(enemyToSpawn);
        }
    }
    public void DestroyEnemy(GameObject objToDestroy)
    {
        enemyPool.TryGetValue(objToDestroy.GetComponent<EnemyBehaviour>().gameObject.name, out Stack<GameObject> result);
        objToDestroy.SetActive(false);
        result.Push(objToDestroy);
    }
    private Stack<GameObject> GetPoolStack(string enemyName)
    {
        if(!enemyPool.ContainsKey(enemyName))
        {
            enemyPool.Add(enemyName, new Stack<GameObject>());
            foreach(KeyValuePair<string, Stack<GameObject>> keyValuePairs in enemyPool)
            {
                bool result = false;
                if(keyValuePairs.Value !=null)
                {
                    result = true; 
                }
            }
        }
        //Write pool creation here and a debug to call when a pool is created
        if(enemyPool.TryGetValue(enemyName, out Stack<GameObject> stack))
        {
            return stack;
        }
        return null;
    }
    private GameObject CreateObject(string enemyName)
    {
        GameObject chosenEnemy = Instantiate(enemyBase);
        foreach (Enemy enemy in Game._database._enemyList)
        {
            if (enemy._name == enemyName)
            {
                chosenEnemy.GetComponent<EnemyBehaviour>().SetStats(enemy._name, enemy._hp, enemy._attack, enemy._magicAttack, enemy._movementSpeed, enemy._armor, enemy._magicResist, enemy._entitySprite); 
                break;
            }
        }
        return chosenEnemy;
    }
}

