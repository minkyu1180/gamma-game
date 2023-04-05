using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class Stage1_2HiddenSceneManager : MonoBehaviour, IDataPersistence
{
    public static GameObject InterfaceElements;
    public static GameObject GameElements;
    public GameObject Camera;
    GameObject Player;
    private Vector3 cameraPositionSaved;
    private float cameraSizeSaved;
    GameObject DialogBoxTextObject;
    private bool itemGet;
    private int stageCount;

    public void LoadData(GameData data)
    {
        itemGet = data.item1Got;
    }

    public void SaveData(ref GameData data){}


    void Start()
    {
        InputDecoder.isGameInScript = true;
        InputDecoder.isConditionWaiting = true;
        DialogBoxTextObject = GameObject.Find("DialogBoxText");
        InterfaceElements = GameObject.Find("UI_Elements");
        GameElements = GameObject.Find("GAME_Elements");
        Camera = GameObject.Find("MainCamera");
        Player = GameObject.Find("Minkyu");
        cameraPositionSaved = Camera.transform.position;
        cameraSizeSaved = Camera.GetComponent<Camera>().orthographicSize;
        
        if (!itemGet)
        {
            StartCoroutine(ScriptLoader());
        }
        else
        {
            StartCoroutine(ScriptAfterItemGetLoader());
        }
    }



    IEnumerator ScriptLoader()
    {
        DialogBoxTextObject.GetComponent<DialogBoxTextTyper>().LoadScript("Text/Stage1-2Hidden/Opening");
        yield return new WaitWhile(() => InputDecoder.isGameInScript);
    }

    IEnumerator ScriptAfterItemGetLoader()
    {
        DialogBoxTextObject.GetComponent<DialogBoxTextTyper>().LoadScript("Text/Stage1-2Hidden/OpeningAfterItemGet");
        yield return new WaitWhile(() => InputDecoder.isGameInScript);
    }


}
