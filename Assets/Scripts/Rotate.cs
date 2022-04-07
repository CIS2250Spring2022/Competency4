using UnityEngine;

public class Rotate : MonoBehaviour
{
    public float rotationRate = 5;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0,rotationRate*Time.deltaTime,0);
    }
}