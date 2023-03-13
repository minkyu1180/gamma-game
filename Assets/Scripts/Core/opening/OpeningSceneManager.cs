using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpeningSceneManager : MonoBehaviour
{
    public static GameObject InterfaceElements;
    public static GameObject GameElements;
    void Start()
    {
        GameObject DialogBoxTextObject = GameObject.Find("DialogBoxText");
        InterfaceElements = GameObject.Find("UI_Elements");
        GameElements = GameObject.Find("GAME_Elements");
        DialogBoxTextObject.GetComponent<DialogBoxTextTyper>().LoadScript("Text/Opening");


    }

    
    void Update()
    {
        //Auto wrap (minkyu)
        //InputDecoder.InterfaceElements.SetActive(true);
        //Dialog load
        //This is the text load way.

        /*
        if (Input.GetKeyDown("h"))
        {
            if (InterfaceElements.activeInHierarchy)
            {
                InterfaceElements.SetActive(false);
            }
            else 
            {
                InterfaceElements.SetActive(true);
            }
        }
        */
        //UI hidding func
        
    }
}
