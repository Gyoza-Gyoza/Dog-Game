using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartMenuManager : MonoBehaviour
{
    [SerializeField]
    private Button
        startGame,
        quitGame;

    [SerializeField]
    private GameObject
        characterMenu;

    private void Start()
    {
        //Adds functions to character menu and turns it off 
        Button[] selectButtons = characterMenu.GetComponentsInChildren<Button>(); 
        foreach (Button button in selectButtons)
        {
            switch (button.gameObject.name[0]) //Checks the names of the gameobject to assign button functions 
            {
                case 'K':
                    button.onClick.AddListener(() =>
                    {
                        Game._chosenPlayer = Game._database._playerDB["CLASS_KNIGHT"] as Player;
                        Game._playerManager.SpawnPlayer(Game._playerManager.transform);
                        Game._gameSceneManager.CloseScene("MainMenu");
                    });
                    break;

                case 'M':
                    button.onClick.AddListener(() =>
                    {
                        Game._chosenPlayer = Game._database._playerDB["CLASS_MAGE"] as Player;
                        Game._playerManager.SpawnPlayer(Game._playerManager.transform);
                        Game._gameSceneManager.CloseScene("MainMenu");
                    });
                    break;

                case 'A':
                    button.onClick.AddListener(() =>
                    {
                        Game._chosenPlayer = Game._database._playerDB["CLASS_ARCHER"] as Player;
                        Game._playerManager.SpawnPlayer(Game._playerManager.transform);
                        Game._gameSceneManager.CloseScene("MainMenu");
                    });
                    break;

                case 'T':
                    button.onClick.AddListener(() =>
                    {
                        Game._chosenPlayer = Game._database._playerDB["CLASS_THIEF"] as Player;
                        Game._playerManager.SpawnPlayer(Game._playerManager.transform);
                        Game._gameSceneManager.CloseScene("MainMenu");
                    });
                    break;
            }
        }

        characterMenu.SetActive(false);

        //Adds functions to the buttons 
        startGame.onClick.AddListener(() =>
        {
            characterMenu.SetActive(true); //Moves to character selection screen
        });

        quitGame.onClick.AddListener(() =>
        {
            Application.Quit(); //Quits the game
        });
    }
}
