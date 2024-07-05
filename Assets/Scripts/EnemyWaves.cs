using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[System.Serializable]
public class EnemyWaves 
{
    private string
        waveId;

    [SerializeField] 
    private List<EnemyBatch>
        enemyBatch = new List<EnemyBatch>(); 

    public EnemyWaves(string waveId, string enemyCode)
    {
        this.waveId = waveId;

        Database db = Game._database;
        string[] enemyBatchResult = enemyCode.Split('#');
        foreach(string s in enemyBatchResult)
        {
            List<int> result = new List<int>(); 

            string[] arr = s.Split('@');
            for (int i = 0; i < arr.Length; i++)
            {
                int number;

                bool success = int.TryParse(arr[i], out number);
                if(success)
                {
                    result.Add(number);
                }
                else
                {
                    result.Add(0);
                    Debug.Log("Unable to parse value"); 
                }
            }
            enemyBatch.Add(new EnemyBatch((EnemyTypes)result.ElementAt(0), result.ElementAt(1)));
        }
    }
}

[System.Serializable]
public class EnemyBatch
{
    [SerializeField]
    private EnemyTypes
        enemyType;

    [SerializeField]
    private int 
        enemyCount;

    public EnemyBatch(EnemyTypes enemyType, int enemyCount)
    {
        this.enemyType = enemyType;
        this.enemyCount = enemyCount;
    }
}