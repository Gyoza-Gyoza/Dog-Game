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
        Time.timeScale = 0f;
        LoadSceneMode mode = isAdditive ? LoadSceneMode.Additive : LoadSceneMode.Single;
        SceneManager.LoadSceneAsync(sceneName, mode).completed += (asyncop) =>
        {
            actionOnLoad?.Invoke();
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
