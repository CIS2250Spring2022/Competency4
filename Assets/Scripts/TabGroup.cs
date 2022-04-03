using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class TabGroup : MonoBehaviour
{
    public List<TabButton> tabButtons;
    public TabButton selectedTab;

    public List<GameObject> allTabPanels;

    public void Subscribe(TabButton button)
    {
        if (tabButtons == null)
        {
            tabButtons = new List<TabButton>();
        }

        tabButtons.Add(button);
    }

    public void OnTabEnter(TabButton buton)
    {
        ResetTabs();
    }

    public void OnTabExit(TabButton button)
    {
        ResetTabs();
    }

    public void OnTabSelected(TabButton button)
    {
        selectedTab = button;
        ResetTabs();
    }

    public void ResetTabs()
    {
        int index = selectedTab.transform.GetSiblingIndex();
        foreach (TabButton button in tabButtons)
        {
            //TODO check if we want Sprite backgrounds in the buttons or not.
            if (selectedTab == null)
            {
                selectedTab = tabButtons[0];
            }
            for (int i = 0; i < allTabPanels.Count; i++)
            {
                if (i == index)
                {
                    allTabPanels[i].SetActive(true);
                }
                else
                {
                    allTabPanels[i].SetActive(false);
                }
            }
        }
        ensureTabIsSelected();
    }

    public void ensureTabIsSelected()
    {
        selectedTab.GetComponentInParent<Button>().Select();
    }

    void Start()
    {
        selectedTab = tabButtons[0];
        ResetTabs();
    }

    void Update()
    {
        ensureTabIsSelected();
    }
}
