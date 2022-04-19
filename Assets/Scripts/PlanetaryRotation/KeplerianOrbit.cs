using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class KeplerianOrbit : MonoBehaviour
{

    //Orbital Keplerian Parameters
    [SerializeField] float semiMajorAxis = 20f; //a - size
    [SerializeField] [Range(0f, 0.99f)] float eccentricity; //e - shape
    [SerializeField] [Range(0f, Math.TAU)] float inclination = 0f; //i - tilt
    [SerializeField] [Range(0f, Math.TAU)] float longitudeOfAcendingNode; //n - swivel
    [SerializeField] [Range(0f, Math.TAU)] float argumentOfPeriapsis; //w - position
    [SerializeField] float meanLongitude; //L - offset
    [SerializeField] ObjBodyMass objBody;
    [SerializeField] float meanAnomaly;    

    //Settings
    [SerializeField] float accuracyTolerance = 1e-6f;
    [SerializeField] int maxIterations = 5;          

    //Numbers which only change if orbit or mass changes
    [HideInInspector] [SerializeField] float mu;
    [HideInInspector] [SerializeField] float n, cosLOAN, sinLOAN, sinI, cosI, trueAnomalyConstant;    

    private void OnValidate() => orbitalPoints.Clear();
    void Awake() => CalculateSemiConstants();

    //f(x) = 0
    public float F(float E, float e, float M)  
    {
        return (M - E + e * Mathf.Sin(E));
    }

    //Derivative of the function
    public float DF(float E, float e)      
    {
        return (-1f) + e * Mathf.Cos(E);
    }

    struct Constants
    {
        public const float G = 6.67f;
    }    

    //Numbers that only need to be calculated once if the orbit doesn't change.
    public void CalculateSemiConstants()    
    {
        mu = Constants.G * objBody.mass;
        n = Mathf.Sqrt(mu / Mathf.Pow(semiMajorAxis, 3));
        trueAnomalyConstant = Mathf.Sqrt((1 + eccentricity) / (1 - eccentricity));
        cosLOAN = Mathf.Cos(longitudeOfAcendingNode);
        sinLOAN = Mathf.Sin(longitudeOfAcendingNode);
        cosI = Mathf.Cos(inclination);
        sinI = Mathf.Sin(inclination);
    }

    void Update()
    {
        CalculateSemiConstants();

        meanAnomaly = (float)(n * (Time.time - meanLongitude));

        float E1 = meanAnomaly;
        float difference = 1f;
        for (int i = 0; difference > accuracyTolerance && i < maxIterations; i++)
        {
            float E0 = E1;
            E1 = E0 - F(E0, eccentricity, meanAnomaly) / DF(E0, eccentricity);
            difference = Mathf.Abs(E1 - E0);
        }
        float EccentricAnomaly = E1;

        float trueAnomaly = 2 * Mathf.Atan(trueAnomalyConstant * Mathf.Tan(EccentricAnomaly / 2));
        float distance = semiMajorAxis * (1 - eccentricity * Mathf.Cos(EccentricAnomaly));

        float cosAOPPlusTA = Mathf.Cos(argumentOfPeriapsis + trueAnomaly);
        float sinAOPPlusTA = Mathf.Sin(argumentOfPeriapsis + trueAnomaly);

        float x = distance * ((cosLOAN * cosAOPPlusTA) - (sinLOAN * sinAOPPlusTA * cosI));
        //Switching z and y to be aligned with xz not xy
        float z = distance * ((sinLOAN * cosAOPPlusTA) + (cosLOAN * sinAOPPlusTA * cosI));      
        float y = distance * (sinI * sinAOPPlusTA);

        transform.position = new Vector3(x, y, z) + objBody.transform.position;
    }
    struct Math
    {
        public const float TAU = 6.28318530718f;
    }

    int orbitResolution = 50;
    List<Vector3> orbitalPoints = new List<Vector3>();

    private void OnDrawGizmos()
    {
        if (orbitalPoints.Count == 0)
        {
            if (objBody == null)
            {
                Debug.LogWarning($"Add a reference body to {gameObject.name}");
                return;
            }

            CalculateSemiConstants();
            Vector3 pos = objBody.transform.position;
            float orbitFraction = 1f / orbitResolution;

            for (int i = 0; i < orbitResolution + 1; i++)
            {
                float EccentricAnomaly = i * orbitFraction * Math.TAU;

                float trueAnomaly = 2 * Mathf.Atan(trueAnomalyConstant * Mathf.Tan(EccentricAnomaly / 2));
                float distance = semiMajorAxis * (1 - eccentricity * Mathf.Cos(EccentricAnomaly));

                float cosAOPPlusTA = Mathf.Cos(argumentOfPeriapsis + trueAnomaly);
                float sinAOPPlusTA = Mathf.Sin(argumentOfPeriapsis + trueAnomaly);

                float x = distance * ((cosLOAN * cosAOPPlusTA) - (sinLOAN * sinAOPPlusTA * cosI));
                float z = distance * ((sinLOAN * cosAOPPlusTA) + (cosLOAN * sinAOPPlusTA * cosI));
                float y = distance * (sinI * sinAOPPlusTA);

                float meanAnomaly = EccentricAnomaly - eccentricity * Mathf.Sin(EccentricAnomaly);

                orbitalPoints.Add(pos + new Vector3(x, y, z));
            }
        }
        //Draw path of orbit
       //Handles.DrawAAPolyLine(orbitalPoints.ToArray());
    }

}