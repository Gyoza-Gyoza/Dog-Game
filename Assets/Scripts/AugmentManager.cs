using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

//This code was made for a cancelled mechanic called augments, it would display a few choices to the player and upon choosing one, it would give 
//the player new skills;
public class Augment
{
    private string
        augmentName,
        augmentDescription;

    private int
        augmentEffect; 

    public string
        _augmentName
    {  get { return augmentName; } }

    public string 
        _augmentDescription
    { get { return augmentDescription; } }

    public int
        _augmentEffect
    { get { return augmentEffect; } }

    public Augment(string augmentName, string augmentDescription, int augmentEffect)
    {
        this.augmentName = augmentName;
        this.augmentDescription = augmentDescription;
        this.augmentEffect = augmentEffect;
    }
}

public class AugmentManager : MonoBehaviour
{
    private List<Augment> 
        augmentList = new List<Augment>();

    [SerializeField]
    private GameObject[]
        cardArray; 

    private void Awake()
    {
        if(Game._augmentManager == null)
        {
            Game._augmentManager = this;
        }
    }
    public void InitializeList()
    {
        //augmentList = Game._database._augmentList.ToList();
    }
    public void SetAugment()
    {
        foreach (GameObject go in cardArray)
        {
            TextMeshProUGUI[] texts = go.GetComponentsInChildren<TextMeshProUGUI>();

            int rand = Random.Range(0, augmentList.Count);

            Augment aug = augmentList.ElementAt(rand);
            augmentList.RemoveAt(rand);

            foreach (TextMeshProUGUI text in texts)
            {
                switch (text.name)
                {
                    case "Title":
                        text.text = aug._augmentName;
                        break;

                    case "BodyText":
                        text.text = aug._augmentDescription;
                        break;
                }
            }
            go.GetComponent<Button>().onClick.AddListener(() =>
            {
                AugmentEffects(aug._augmentEffect);
            });
        }
    }
    private void AugmentEffects(int effect)
    {
        switch (effect)
        {
            case 0:
                Debug.Log("Effect 0");
                break;
            case 1:
                Debug.Log("Effect 1");
                break;
            case 2:
                Debug.Log("Effect 2");
                break;
            case 3:
                Debug.Log("Effect 3");
                break;
        }
    }
}
