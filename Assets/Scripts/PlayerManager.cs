using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    [SerializeField]
    private GameObject
        player;

    private void Awake()
    {
        if(Game._playerManager == null)
        {
            Game._playerManager = this;
        }
    }
    private void Start()
    {
        SpawnPlayer(gameObject.transform);
    }
    public void SpawnPlayer(Transform pos)
    {
        GameObject result = Instantiate(player, pos);
        Player chosenEntity = Game._chosenPlayer;
        result.GetComponent<PlayerBehaviour>().SetStats(
            chosenEntity._name, 
            chosenEntity._hp, 
            chosenEntity._attack, 
            chosenEntity._movementSpeed, 
            chosenEntity._defence, 
            chosenEntity._entitySprite, 
            chosenEntity._attackSpeed, 
            chosenEntity._attackRange, 
            chosenEntity._critChance, 
            chosenEntity._projectileType, 
            chosenEntity._classHurtSprite,
            chosenEntity._dashSpeed, 
            chosenEntity._dashDuration);
    }
}
