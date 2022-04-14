using UnityEngine;
using System.Collections;

//This class is intended to be used to bring up a gui with maps of the planet, moon or ship so the player can pick various options
public class PlanetaryDevice : BaseDevice
{
    public GameObject map;

    public override void Operate()
    {
        Debug.Log($"{this.gameObject.name} Operated");
        map.SetActive(true);
    }
}
