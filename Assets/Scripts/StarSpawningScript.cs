using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarSpawningScript : MonoBehaviour
{
    public GameObject starPrefab;

    private string[] starColors = new string[] { "#9bb0ff", "#aabfff", "#cad7ff", "#f8f7ff", "#fff4ea", "#ffd2a1", "#ffcc6f" };

    private Color randomStarColor()
    {
        string randomColor = starColors[Random.Range(0, starColors.Length)];
        Color toReturn = new Color();
        bool validHex = ColorUtility.TryParseHtmlString(randomColor, out toReturn);
        if (validHex)
        {
            return toReturn;
        }
        else
        {
            return Color.white;
        }
    }


    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < 60; i++)
        {
            Vector3 randPos = Random.insideUnitSphere * 10;
            GameObject newStar = Instantiate(starPrefab, randPos, Quaternion.identity);
            Color newColor = randomStarColor();
            newStar.GetComponent<MeshRenderer>().material.SetColor("_Color", newColor);
            newStar.GetComponent<MeshRenderer>().material.EnableKeyword("_EMISSION");
            newStar.GetComponent<MeshRenderer>().material.SetColor("_EmissionColor", newColor);
            newStar.GetComponent<MeshRenderer>().receiveShadows = false;
            newStar.GetComponent<MeshRenderer>().material.shader = Shader.Find("Unlit/Color");
            DynamicGI.UpdateEnvironment();
        }
    }
}
