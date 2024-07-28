using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Response 
{
    private string
        response,
        link; 

    public string _response
    { get { return response; } }

    public string _link
    { get { return link; } }

    public Response(string response, string link)
    {
        this.response = response.Replace('#', ',');
        this.link = link;
    }
}
