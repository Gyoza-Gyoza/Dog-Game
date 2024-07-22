using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    private List<Transform>
        spawnLocations = new List<Transform>(); 

    public List<Transform> _spawnLocations
    { get { return spawnLocations; } }

    private void Awake()
    {
        if (Game._waveManager == null)
        {
            Game._waveManager = this;
        }
    }

}
