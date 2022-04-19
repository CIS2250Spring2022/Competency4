using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

/*
 * Dynamic Star Scene
 * *******************
 * if you want to make a scene, add objects according to the parameters inside the /satellites.json file in the assets directory.
 * Each line of the file is a separate satellite. Replicate the provided satellites or change the values. Newlines are delimiters.
 */



public class SceneLoader : MonoBehaviour
{
    public Transform Star;
    public Transform satellitePrefab;
    public List<Satellite> satellites;

    private GameObject lightGameObject;
    private string m_Path;

    void Start()
    {

        lightGameObject = new GameObject("Light");
        Light lightComp = lightGameObject.AddComponent<Light>();
        lightComp.color = Color.yellow;
        lightGameObject.transform.position = new Vector3(0, 0, 0);
        m_Path = Application.dataPath + "/satellites.json";

        satellites = ReadFile(m_Path);

        //WriteFile(m_Path);

        Instantiate(Star, new Vector3(0, 0, 0), Quaternion.identity);
        
        if (satellites.Count > 0)
        {
            for (int i = 0; i < satellites.Count; i++)
            {
                Transform satellite = Instantiate(satellites[i].body, new Vector3(0, 0, satellites[i].distance), Quaternion.identity);
                satellite.parent = satellites[i].anchor.transform;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (satellites.Count > 0)
        {
            for (int i = 0; i < satellites.Count; i++)
            {
                float direction = satellites[i].orbitSpeed * (satellites[i].orbitDirection ? 1f : -1f);
                satellites[i].anchor.transform.Rotate(0f, direction, 0f);
            }
        }
    }

    List<Satellite> ReadFile(string path)
    {
        var rawdump = File.ReadLines(path);
        List<Satellite> output = new List<Satellite>();
        foreach (string line in rawdump)
        {
            Satellite temp = JsonUtility.FromJson<Satellite>(line);
            temp.body = satellitePrefab;
            temp.anchor = new GameObject();
            temp.anchor.transform.position = new Vector3(0, 0, 0);
            output.Add(temp);
        }


        return output;
    }

    void WriteFile(string path)
        // this is just here as an example of how to make the json file, initially. If you want to add satellites, put them in the file.
    {
        string json = "";
        for (int i = 0; i < satellites.Count; i++)
        {
            json = JsonUtility.ToJson(satellites[i]);
            json += Environment.NewLine;
            File.AppendAllText(path, json);
        }
        
    }
}
