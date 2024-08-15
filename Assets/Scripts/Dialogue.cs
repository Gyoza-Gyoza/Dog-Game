using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//DONE BY WANG JIA LE
public class Dialogue
{
    private string
        dialogueRef,
        speaker, 
        dialogueText,
        noResponseLink;

    private bool
        canRespond;

    private Response
        response1,
        response2;

    public string _dialogueRef
    { get { return dialogueRef; } }

    public string _speaker
    { get { return speaker; } }

    public string _dialogueText
    { get { return dialogueText; } }

    public bool _canRespond
    { get { return canRespond; } }

    public string _noResponseLink
    { get { return noResponseLink; } }

    public Response _response1
    { get { return response1; } }

    public Response _response2
    { get { return response2; } }

    public Dialogue(string dialogueRef, string speaker, string dialogueText, string canRespond, string noResponseLink, string response1, string response1Link, string response2, string response2Link)
    {
        this.dialogueRef = dialogueRef;
        this.speaker = speaker;
        this.dialogueText = dialogueText.Replace('#', ',');

        if(canRespond == "TRUE")
        {
            this.canRespond = true;
        }
        else
        {
            this.canRespond = false;
        }

        this.noResponseLink = noResponseLink;
        this.response1 = new Response(response1, response1Link);
        this.response2 = new Response(response2, response2Link);
    }
}
