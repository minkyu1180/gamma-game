using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpeningSceneManager : MonoBehaviour
{
    void Start()
    {
        GameObject DialogBoxTextObject = GameObject.Find("DialogBoxText");
        DialogBoxTextObject.GetComponent<DialogBoxTextTyper>().LoadScript("Scripts/Opening");
        


    }

    
    void Update()
    {
        if (Input.GetKeyDown("h"))
        {
            if (InputDecoder.InterfaceElements.activeInHierarchy)
            {
                InputDecoder.InterfaceElements.SetActive(false);
            }
            else 
            {
                InputDecoder.InterfaceElements.SetActive(true);
            }
        }
        //UI hidding func
        
    }
}
