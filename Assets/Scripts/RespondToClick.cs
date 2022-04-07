using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespondToClick : MonoBehaviour
{
    void Update()
    {
        if (Input.GetMouseButtonDown(0)) 
        {  
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);  
            RaycastHit hit;  
            if (Physics.Raycast(ray, out hit)) 
            {  
                //Select stage    
                if (hit.transform.position == this.transform.position) 
                {  
                //SceneManager.LoadScene("SceneTwo");  
                Debug.Log("Clicked!");
                }  
            }  
        }  
    }
}
