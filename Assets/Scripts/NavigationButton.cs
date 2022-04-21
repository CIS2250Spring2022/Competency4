using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NavigationButton : MonoBehaviour
{
    // Start is called before the first frame update
    public string levelName;

    private Text _text;

    public void OnClick()
    {
        Managers.Mission.GoToScene(levelName);
    }
}
