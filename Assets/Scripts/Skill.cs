using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill
{
    private string
        skillName,
        skillDescription, 
        skillIcon, 
        skillSprite;

    private int
        skillCooldown; 

    public string _skillName
    { get { return skillName; } }

    public string _skillDescription
    { get { return skillDescription; } }

    public string _skillIcon
    { get { return skillIcon; } }

    public string _skillSprite
    { get { return skillSprite; } }

    public int _skillCooldown
    { get { return skillCooldown; } }

    public Skill(string skillName, string skillDescription, string skillIcon, string skillSprite, int skillCooldown)
    {
        this.skillName = skillName;
        this.skillDescription = skillDescription;
        this.skillIcon = skillIcon;
        this.skillSprite = skillSprite;
        this.skillCooldown = skillCooldown;
    }
}
