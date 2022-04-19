using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[Serializable]
public class Satellite
{
    public Satellite(Transform body, GameObject anchor, float mass, float distance, float orbitSpeed, bool orbitDirection, float orbitAxis)
    {
        this.distance = distance;
        this.anchor = anchor;
        this.mass = mass;
        
        this.orbitSpeed = orbitSpeed;
        this.orbitDirection = orbitDirection;
        this.orbitAxis = orbitAxis;
        this.body = body;
    }

    public Transform body;
    public GameObject anchor;
    public float mass;
    public float distance;
    public float orbitSpeed;
    public bool orbitDirection;
    public float orbitAxis;
}
