using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class GameSceneManager : MonoBehaviour
{
    private void Awake()
    {
        if (Game._gameSceneManager == null)
        {
            Game._gameSceneManager = this;
        }
    }
    public void OpenScene(string sceneName, bool isAdditive, UnityAction actionOnLoad)
    {
        LoadSceneMode mode = isAdditive ? LoadSceneMode.Additive : LoadSceneMode.Single;
        SceneManager.LoadSceneAsync(sceneName, mode).completed += (asyncop) =>
        {
            actionOnLoad?.Invoke();
        };
    }
    public void CloseScene(string sceneName)
    {
        Scene sceneToClose = SceneManager.GetSceneByName(sceneName);
        if(sceneToClose.IsValid())
        {
            SceneManager.UnloadSceneAsync(sceneName);
        }
    }
}
