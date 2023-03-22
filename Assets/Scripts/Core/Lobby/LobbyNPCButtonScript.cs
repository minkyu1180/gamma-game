using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine;

// IPointerClickHandler
public class LobbyNPCButtonScript : MonoBehaviour
{
    GameObject ControlManager;
    GameObject DialogBoxTextObject;
    public bool isGameInScript;

    void Start()
    {
        ControlManager = GameObject.Find("BGMControlManager");
        DialogBoxTextObject = GameObject.Find("DialogBoxText");
    }
    public void JiheeClick()
    {
        ControlManager.GetComponent<OpeningSceneManager>().isGameInScript = true;
        StartCoroutine(JiheeScript());
    }

    IEnumerator JiheeScript()
    {
        InputDecoder.InterfaceElements.SetActive(true);
        DialogBoxTextObject.GetComponent<DialogBoxTextTyper>().LoadScript("Text/Opening/JiheeClick1");
        yield return new WaitWhile(() => ControlManager.GetComponent<OpeningSceneManager>().isGameInScript);

        

        ControlManager.GetComponent<OpeningSceneManager>().isConditionWaiting = false;
    }
}