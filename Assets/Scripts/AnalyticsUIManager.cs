using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

//DONE BY WANG JIA LE
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
        goldEarned.text = $"{Game._inventoryManager._goldCount.ToString()}";
    }
    public void UpdateKillsUI()
    {
        monstersKilled.text = $"{AnalyticsManager._monstersKilled.ToString()}";
    }
    public void UpdateDamageUI()
    {
        damageDealt.text = $"{AnalyticsManager._damageDealt.ToString()}";
    }
}
