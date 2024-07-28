using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    //[SerializeField] //Remove after testing
    //private string
    //    dialogueIDToPlay;

    [SerializeField]
    private TextMeshProUGUI
        dialogueText;

    [SerializeField]
    private GameObject
        response1,
        response2;

    [SerializeField]
    private Image
        portrait;

    private Dictionary<string, Sprite>
        characterPortraits = new Dictionary<string, Sprite>();

    private List<Dialogue> 
        dialogueList = new List<Dialogue>();

    private bool
        canRespond = true,
        isTyping = false,
        skipText = false, 
        canSkipText = false, 
        endDialogue = false, 
        canEndDialogue = false;

    private WaitForSecondsRealtime
        typingSpeed = new WaitForSecondsRealtime(0.05f), 
        skipTextTimer = new WaitForSecondsRealtime(0.2f);

    private Dialogue
        nextDialogue = null;

    private void Awake()
    {
        if (Game._dialogueManager == null)
        {
            Game._dialogueManager = this;
        }
        gameObject.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (canEndDialogue && !isTyping)
            {
                EndDialogue();
                return;
            }
            if (!canRespond && !isTyping)
            {
                NextDialogue(nextDialogue);
            }
            if(isTyping && canSkipText) //Checks if can skip text during typewriter effect 
            {
                skipText = true;
            }
        }
    }

    public void StartDialogue(string dialogueIDToPlay) //Called by NPCs to start dialogue 
    {
        Game._inputHandler.PauseGame();
        endDialogue = false;
        canEndDialogue = false;

        //Adds the dialogues needed for the required dialogue 
        foreach (KeyValuePair<string, Dialogue> kvp in Game._database._dialogueDB)
        {
            if (Game._database._dialogueDB[dialogueIDToPlay]._dialogueRef == kvp.Value._dialogueRef)
            {
                dialogueList.Add(kvp.Value);

                if(!characterPortraits.ContainsKey(kvp.Value._speaker))
                {
                    characterPortraits.Add(kvp.Value._speaker, null); //Make sure that the if statement works before loading the sprite is done

                    AssetManager.LoadSprites($"Assets/Images/UI/Dialogue UI/Portraits/{kvp.Value._speaker}.png", (Sprite sp) => //Loads sprites into a list for later use 
                    {
                        characterPortraits[kvp.Value._speaker] = sp;
                        Debug.Log($"Added {kvp.Value._speaker} to portraits");
                        gameObject.SetActive(true);
                        NextDialogue(Game._database._dialogueDB[dialogueIDToPlay]);
                    });
                }
                else
                {
                    gameObject.SetActive(true);
                    NextDialogue(Game._database._dialogueDB[dialogueIDToPlay]);
                }
            }
        }

    }
    private void NextDialogue(Dialogue dialogueToPlay) //Assigns responses to text boxes as well as starts the typewriter effect 
    {
        canRespond = dialogueToPlay._canRespond;

        portrait.sprite = characterPortraits[dialogueToPlay._speaker];

        response1.GetComponentInChildren<TextMeshProUGUI>().text = dialogueToPlay._response1._response;
        response1.GetComponent<Button>().onClick.RemoveAllListeners();
        response1.GetComponent<Button>().onClick.AddListener(() =>
        {
            AddButtonFunctions(dialogueToPlay._response1);
        });

        response2.GetComponentInChildren<TextMeshProUGUI>().text = dialogueToPlay._response2._response;
        response2.GetComponent<Button>().onClick.RemoveAllListeners();
        response2.GetComponent<Button>().onClick.AddListener(() =>
        {
            AddButtonFunctions(dialogueToPlay._response2);
        });

        response1.SetActive(false);
        response2.SetActive(false);

        if(!canRespond) //Saves the next response for the no response option
        {
            if (dialogueToPlay._noResponseLink != "QUIT")
            {
                nextDialogue = Game._database._dialogueDB[dialogueToPlay._noResponseLink];
            }
            else
            {
                endDialogue = true;
            }
        }

        StartCoroutine(TypingEffect(dialogueText, dialogueToPlay._dialogueText));
    }
    private void AddButtonFunctions(Response response)
    {
        switch (response._link)
        {
            case "SHOP":
                EndDialogue();

                Game._uIManager.ToggleShop();
                Game._inputHandler.PauseGame();
                Game._inventoryManager.UpdateInventory();
                Game._inventoryManager.UpdateShopMenu();
                break;

            case "QUIT":
                EndDialogue();
                break;

            default:
                NextDialogue(Game._database._dialogueDB[response._link]);
                break;
        }
    }
    private IEnumerator TypingEffect(TextMeshProUGUI text, string textToType) 
    {
        StartCoroutine(SkipTextTimer()); //Sets a timer so text cannot be immediately skipped

        if (!isTyping)
        {
            isTyping = true;
            text.text = ""; //Initializes text at the start

            foreach (char c in textToType) //Types out text with a delay between each character
            {
                if(!skipText)
                {
                    text.text += c;
                    yield return typingSpeed;
                }
                else
                {
                    skipText = false;
                    text.text = textToType;
                    break;
                }
            }

            if(canRespond) //Activates the response boxes if can respond
            {
                response1.SetActive(true);
                response2.SetActive(true);
            }

            if(endDialogue) //Allows the player to end the dialogue if it ends with a non response dialogue and its finished typing
            {
                canEndDialogue = true;
            }

            isTyping = false;
        }
    }
    private IEnumerator SkipTextTimer()
    {
        canSkipText = false;
        yield return skipTextTimer;
        canSkipText = true;
    }
    private void EndDialogue() //Ends the dialogue and resumes the game
    {
        Game._inputHandler.ResumeGame();
        gameObject.SetActive(false);
        dialogueList.Clear();
    }
}
