using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCBehaviour : MonoBehaviour 
{
    [SerializeField]
    private string
        dialogueRef; 

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            Game._player._npcInRange = this;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            Game._player._npcInRange = null;
        }
    }
    public virtual void Interact()
    {
        Game._dialogueManager.StartDialogue(dialogueRef);
    }
}