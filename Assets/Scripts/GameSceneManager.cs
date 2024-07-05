using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameSceneManager : MonoBehaviour
{
    private void Awake()
    {
        Game._gameSceneManager = this;
    }
    public void OpenScene(string sceneName)
    {
        SceneManager.LoadSceneAsync(sceneName);
    }
}
