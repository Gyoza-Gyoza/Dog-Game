using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillBehaviour : MonoBehaviour
{
    protected delegate void SpellEffects();
    protected SpellEffects spellEffects;
    private void OnDisable()
    {
        Game._skillManager.DestroySpell(this.gameObject);
    }
}
