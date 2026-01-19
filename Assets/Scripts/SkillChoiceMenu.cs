using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

//This script holds a reference to the skill choice menus 
public class SkillChoiceMenu : MonoBehaviour
{
    [SerializeField]
    private SkillChoiceTab[] 
        tabs;

    private Dictionary<string, Skill>
        skillList = new Dictionary<string, Skill>(), 
        chosenSkillPool = new Dictionary<string, Skill>();

    private void Awake()
    {
        if (Game._skillChoiceMenu == null)
        {
            Game._skillChoiceMenu = this;
        }
    }
    private void Start()
    {
        foreach (KeyValuePair<string, Skill> kvp in Game._database._skillDB)
        {
            if(kvp.Key != "PROJ0001" && kvp.Key != "PROJ0002" && kvp.Key != "PROJ0003" && kvp.Key != "PROJ0004")
            {
                skillList.Add(kvp.Key, kvp.Value); //Cloning the database 
            }
        }
        Debug.Log($"Amount of skills in skill list {skillList.Count}");
        gameObject.SetActive(false);
    }
    public void OpenSkillMenu()
    {

        if (Game._skillManager.SkillSlotAvailable)
        {
            Time.timeScale = 0f;

            foreach (SkillChoiceTab sct in tabs)
            {
                int rand = Random.Range(0, skillList.Count);
                string chosenSkillId = skillList.ElementAt(rand).Key;
                Skill chosenSkill = skillList.ElementAt(rand).Value;

                sct.title.text = chosenSkill._skillName;
                sct.description.text = chosenSkill._skillDescription;
                AssetManager.LoadSprites(chosenSkill._skillIcon, (Sprite sp) =>
                {
                    sct.icon.sprite = sp;
                });
                sct.select.onClick.RemoveAllListeners();
                sct.select.onClick.AddListener(() =>
                {
                    ChooseSkill(chosenSkillId);
                });

                chosenSkillPool.Add(chosenSkillId, chosenSkill);
                skillList.Remove(chosenSkillId);
            }

            gameObject.SetActive(true);
            Debug.Log($"{skillList.Count} skills left in skill list");
        }
    }
    private void ChooseSkill(string skillId)
    {
        Time.timeScale = 1f;

        Game._skillManager.InitializePrefabSkills(skillId);

        chosenSkillPool.Remove(skillId);

        ReturnSkills();

        gameObject.SetActive(false);
    }
    private void ReturnSkills()
    {
        foreach(KeyValuePair<string, Skill> kvp in chosenSkillPool)
        {
            skillList.Add(kvp.Key, kvp.Value);
        }
        chosenSkillPool.Clear();
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