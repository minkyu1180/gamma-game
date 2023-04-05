using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class PreLobbySceneManager : MonoBehaviour
{
    public bool isConditionWaiting;
    public static GameObject InterfaceElements;
    public static GameObject GameElements;
    public GameObject Camera;
    GameObject Player;
    private Vector3 cameraPositionSaved;
    private float cameraSizeSaved;
    GameObject DialogBoxTextObject;
    GameObject NPCJihee;
    GameObject NPCDahye;
    void Start()
    {
        InputDecoder.isGameInScript = true;
        InputDecoder.isConditionWaiting = true;
        DialogBoxTextObject = GameObject.Find("DialogBoxText");
        InterfaceElements = GameObject.Find("UI_Elements");
        GameElements = GameObject.Find("GAME_Elements");
        Camera = GameObject.Find("MainCamera");
        Player = GameObject.Find("Minkyu");
        NPCJihee = GameObject.Find("Jihee");
        NPCDahye = GameObject.Find("Dahye");
        cameraPositionSaved = Camera.transform.position;
        cameraSizeSaved = Camera.GetComponent<Camera>().orthographicSize;
        StartCoroutine(ScriptLoader());

    }



    IEnumerator ScriptLoader()
    {
        DialogBoxTextObject.GetComponent<DialogBoxTextTyper>().LoadScript("Text/PreLobby/Opening");
        yield return new WaitWhile(() => InputDecoder.isGameInScript);

        SceneManager.LoadScene("LobbyScene");

        



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
