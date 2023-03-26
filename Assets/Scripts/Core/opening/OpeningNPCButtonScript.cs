using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine;
using UnityEngine.UI;


// IPointerClickHandler
public class OpeningNPCButtonScript : MonoBehaviour
{
    GameObject ControlManager;
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
        GetComponent<Button>().interactable = false;
    }

    IEnumerator JiheeScript()
    {
        InputDecoder.InterfaceElements.SetActive(true);
        DialogBoxTextObject.GetComponent<DialogBoxTextTyper>().LoadScript("Text/Opening/JiheeClick1");
        yield return new WaitWhile(() => InputDecoder.isGameInScript);

        

        InputDecoder.isConditionWaiting = false;
    }
}