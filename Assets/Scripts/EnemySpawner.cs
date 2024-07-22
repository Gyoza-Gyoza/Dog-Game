using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    private float
        timer;

    private void Awake()
    {
        Game._waveManager._spawnLocations.Add(gameObject.transform);
    }
}
