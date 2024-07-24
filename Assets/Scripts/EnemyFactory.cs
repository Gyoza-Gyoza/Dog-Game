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
    
    public GameObject GetEnemy(string enemyId, Transform spawnLocation)
    {
        GetPoolStack(Game._database._enemyList[enemyId]._name).TryPop(out GameObject result);
        if (result != null)
        {
            result.SetActive(true);
            result.transform.position = spawnLocation.position;
            result.GetComponent<EnemyBehaviour>().ResetHealth();
            return result;
        }
        else
        {
            return CreateObject(enemyId, spawnLocation);
        }
    }
    public void DestroyEnemy(GameObject objToDestroy)
    {
        enemyPool.TryGetValue(objToDestroy.GetComponent<EnemyBehaviour>().gameObject.name, out Stack<GameObject> result);
        objToDestroy.SetActive(false);
        Game._waveManager._killCount++;
        Debug.Log($"Enemy killed, kill count is {Game._waveManager._killCount}");
        result.Push(objToDestroy);
    }
    private Stack<GameObject> GetPoolStack(string enemyName)
    {
        if(!enemyPool.ContainsKey(enemyName))
        {
            enemyPool.Add(enemyName, new Stack<GameObject>());
        }
        //Write pool creation here and a debug to call when a pool is created
        if(enemyPool.TryGetValue(enemyName, out Stack<GameObject> stack))
        {
            return stack;
        }
        return null;
    }
    private GameObject CreateObject(string enemyId, Transform spawnLocation)
    {
        GameObject chosenEnemy = Instantiate(enemyBase, spawnLocation.position, spawnLocation.rotation);
        Enemy enemyStats = Game._database._enemyList[enemyId] as Enemy;

        chosenEnemy.GetComponent<EnemyBehaviour>().SetStats(enemyStats._name, enemyStats._hp, enemyStats._attack, enemyStats._movementSpeed, enemyStats._defence, enemyStats._entitySprite, enemyStats._goldDrop); 

        return chosenEnemy;
    }
    public void ResetFactory()
    {
        enemyPool.Clear();
    }
}

