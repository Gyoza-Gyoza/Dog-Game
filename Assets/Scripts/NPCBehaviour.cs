using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCBehaviour : MonoBehaviour 
{
    [SerializeField]
    private NPC
        npcType;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            Game._player._canInteract = true;
        }
    }
}

public enum NPC
{
    FrogShop,
    SirK9
}