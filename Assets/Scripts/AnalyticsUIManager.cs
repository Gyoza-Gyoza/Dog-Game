using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class AnalyticsUIManager : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI
        goldEarned,
        monstersKilled,
        damageDealt;

    private void Awake()
    {
        if (Game._analyticsUIManager == null)
        {
            Game._analyticsUIManager = this;
        }
    }
    public void UpdateGoldUI()
    {
        goldEarned.text = $"Gold: {AnalyticsManager._goldEarned.ToString()}";
    }
    public void UpdateKillsUI()
    {
        monstersKilled.text = $"Kills: {AnalyticsManager._monstersKilled.ToString()}";
    }
    public void UpdateDamageUI()
    {
        damageDealt.text = $"Damage: {AnalyticsManager._damageDealt.ToString()}";
    }
}
