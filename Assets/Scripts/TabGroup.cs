using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class TabGroup : MonoBehaviour
{
    public List<GUITabButton> tabButtons;
    public GUITabButton selectedTab;

    public List<GameObject> allTabPanels;

    public void Subscribe(GUITabButton button)
    {
        if (tabButtons == null)
        {
            tabButtons = new List<GUITabButton>();
        }

        tabButtons.Add(button);
    }

    public void OnTabEnter(GUITabButton buton)
    {
        ResetTabs();
    }

    public void OnTabExit(GUITabButton button)
    {
        ResetTabs();
    }

    public void OnTabSelected(GUITabButton button)
    {
        selectedTab = button;
        ResetTabs();
    }

    public void ResetTabs()
    {
        int index = selectedTab.transform.GetSiblingIndex();
        foreach (GUITabButton button in tabButtons)
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
