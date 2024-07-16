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
        SpawnPlayer(this.gameObject.transform);
    }
    public void SpawnPlayer(Transform pos)
    {
        GameObject result = Instantiate(player, pos);
        Player chosenEntity = Game._chosenPlayer;
        result.GetComponent<PlayerBehaviour>().SetStats(chosenEntity._name, chosenEntity._hp, chosenEntity._attack, chosenEntity._magicAttack, chosenEntity._movementSpeed, chosenEntity._armor, chosenEntity._magicResist, chosenEntity._entitySprite, chosenEntity._attackSpeed, chosenEntity._weaponType);
    }
}
