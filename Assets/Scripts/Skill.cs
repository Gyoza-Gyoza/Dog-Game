using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill
{
    private string
        skillId,
        skillName,
        skillDescription; 

    public Skill(string skillName, string skillDescription)
    {
        this.skillName = skillName;
        this.skillDescription = skillDescription;
    }
}
