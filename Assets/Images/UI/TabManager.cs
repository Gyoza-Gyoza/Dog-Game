using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TabManager : MonoBehaviour
{
    [SerializeField]
    private GameObject[] tabs;

    private int activeTab = 0;

    private int initializedTabs = 0;

    private void Awake()
    {
        if(Game._tabManager == null)
        {
            Game._tabManager = this;
        }
    }
    public void TurnOnTabs(int tab)
    {
        if(tab != activeTab)
        {
            tabs[tab].SetActive(true); //Opens the selected tab 

            tabs[activeTab].SetActive(false); //Closes the old tab 

            activeTab = tab; //Keeps track of the currently open tab
        }
    }

    public void CountInitializedTabs()
    {
        initializedTabs++; //Counter to count how many tabs are initialized 

        if (initializedTabs == tabs.Length) //When all tabs are initialized, close the menu
        {
            Game._uIManager.CloseAllMenus();
        }
    }
}
