using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class GameSceneManager : MonoBehaviour
{
    [SerializeField]
    private GameObject
        dependencies;

    private void Awake()
    {
        if (Game._gameSceneManager == null)
        {
            Game._gameSceneManager = this;
        }
    }
    private void Start()
    {
        if (GameObject.FindGameObjectWithTag("Dependencies") == null)
        {
            Instantiate(dependencies);
        }
        DontDestroyOnLoad(gameObject);
    }
    public void OpenScene(string sceneName, bool isAdditive, UnityAction actionOnLoad)
    {
        Time.timeScale = isAdditive ? 0f : 1f; //Pauses the game if the player is opening an additive scene
        LoadSceneMode mode = isAdditive ? LoadSceneMode.Additive : LoadSceneMode.Single;
        SceneManager.LoadSceneAsync(sceneName, mode).completed += (asyncop) =>
        {
            if(isAdditive)
            {
                actionOnLoad?.Invoke(); //Invokes the unityaction given in the argument
                Debug.Log("Opened additive scene");
            }
            else
            {
                Game._cursor._arrowPool.Clear(); //Clears the cursor pool to prevent missing references 
                Game._waveManager.ResetWaves(); //Resets the wave manager
                Game._enemyFactory.ResetFactory(); //Resets enemy factory
                Game._skillManager.ResetSkillPools(); //Resets skill pools
                actionOnLoad?.Invoke(); //Invokes the unityaction given in the argument
                Game._player._nav.Warp(Vector3.zero); //Sets the player position 
                Debug.Log("Changed scene");
            }
        };
    }
    public void CloseScene(string sceneName)
    {
        Time.timeScale = 1f;
        Scene sceneToClose = SceneManager.GetSceneByName(sceneName);
        if(sceneToClose.IsValid())
        {
            SceneManager.UnloadSceneAsync(sceneName);
        }
    }
}
