using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

//This script holds a reference to the skill choice menus 
public class SkillChoiceMenu : MonoBehaviour
{
    [SerializeField]
    private SkillChoiceTab[] 
        tabs;

    private void Awake()
    {
        if (Game._skillChoiceMenu == null)
        {
            Game._skillChoiceMenu = this;
        }
    }
    private void Start()
    {
        gameObject.SetActive(false);
    }
    public void ChooseSkills()
    {
        Dictionary<string, Skill> skillPool = Game._database._skillDB; 
    }
}

[System.Serializable]
public class SkillChoiceTab
{
    public Image
        icon; 

    public TextMeshProUGUI
        title,
        description;

    public Button
        select;
}