using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill
{
    private string
        skillName,
        skillDescription, 
        skillIcon,
        skillPrefab;

    private int
        skillCooldown; 

    public string _skillName
    { get { return skillName; } }

    public string _skillDescription
    { get { return skillDescription; } }

    public string _skillIcon
    { get { return skillIcon; } }

    public string _skillPrefab
    { get { return skillPrefab; } }

    public int _skillCooldown
    { get { return skillCooldown; } }

    public Skill(string skillName, string skillDescription, string skillIcon, string skillPrefab, int skillCooldown)
    {
        this.skillName = skillName;
        this.skillDescription = skillDescription.Replace('#', ',');
        this.skillIcon = skillIcon;
        this.skillPrefab = skillPrefab;
        this.skillCooldown = skillCooldown;
    }
}
