using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespondToClick : MonoBehaviour
{
    void Update()
    {
        if (Input.GetMouseButtonDown(0)) 
        Debug.Log("Mouse button 0 pressed");
        {  
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);  
            RaycastHit hit;  
            if (Physics.Raycast(ray, out hit)) 
            {  
                Debug.Log("Hit detected");
                //Select stage    
                if (hit.transform.gameObject.name == this.transform.name) 
                {  
                //SceneManager.LoadScene("SceneTwo");  
                Debug.Log("Clicked!");
                }  
            }  
        }  
    }
}
