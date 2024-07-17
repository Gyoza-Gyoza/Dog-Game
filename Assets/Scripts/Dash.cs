using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dash : Skill
{
    private float
        dashSpeed,
        dashDuration; 

    public Dash(string skillName, string skillDescription, float dashSpeed, float dashDuration)
        : base (skillName, skillDescription)
    {
        this.dashSpeed = dashSpeed;
        this.dashDuration = dashDuration;
    }
}
