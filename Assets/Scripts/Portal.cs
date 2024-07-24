using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour
{
    [SerializeField]
    private string
        dungeonName;

    [SerializeField]
    private bool
        exitingDungeon;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            Game._gameSceneManager.OpenScene(dungeonName, false, () => //Opens the scene 
            {
                Debug.Log(dungeonName[dungeonName.Length - 1]);
                Game._waveManager.InitializeStage(int.Parse(dungeonName[dungeonName.Length - 1].ToString())); //Gets the wave number to initialize 
            });
        }
    }
}
