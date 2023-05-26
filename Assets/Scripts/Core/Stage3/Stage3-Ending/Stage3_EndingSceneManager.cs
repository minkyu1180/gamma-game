using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class Stage3_EndingSceneManager : MonoBehaviour
{
    public static GameObject InterfaceElements;
    public static GameObject GameElements;
    public GameObject Camera;
    private Vector3 cameraPositionSaved;
    private float cameraSizeSaved;
    GameObject DialogBoxTextObject;
    GameObject dataPersistenceManager;

    
    void Start()
    {
        InputDecoder.isGameInScript = true;
        InputDecoder.isConditionWaiting = true;
        DialogBoxTextObject = GameObject.Find("DialogBoxText");
        InterfaceElements = GameObject.Find("UI_Elements");
        GameElements = GameObject.Find("GAME_Elements");
        Camera = GameObject.Find("MainCamera");
        
        StartCoroutine(OpeningScriptLoad());

    }



    IEnumerator OpeningScriptLoad()
    {
        string textLocation = "Text/Stage3-Ending/Opening";
        DialogBoxTextObject.GetComponent<DialogBoxTextTyper>().LoadScript(textLocation);
        yield return new WaitWhile(() => InputDecoder.isGameInScript);
        SceneManager.LoadScene("LobbyScene");
    }
}
