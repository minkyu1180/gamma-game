using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine;

// IPointerClickHandler
public class LobbyNPCButtonScript : MonoBehaviour
{
    GameObject DialogBoxTextObject;
    public bool isGameInScript;

    void Start()
    {
        DialogBoxTextObject = GameObject.Find("DialogBoxText");
    }
    public void JiheeClick()
    {
        InputDecoder.isGameInScript = true;
        StartCoroutine(JiheeScript());
    }

    IEnumerator JiheeScript()
    {
        InputDecoder.InterfaceElements.SetActive(true);
        DialogBoxTextObject.GetComponent<DialogBoxTextTyper>().LoadScript("Text/Opening/JiheeClick1");
        yield return new WaitWhile(() => InputDecoder.isGameInScript);

        

        InputDecoder.isConditionWaiting = false;
    }
}