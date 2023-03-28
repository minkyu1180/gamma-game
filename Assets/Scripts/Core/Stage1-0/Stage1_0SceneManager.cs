using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class Stage1_0SceneManager : MonoBehaviour, IDataPersistence
{
    public static GameObject InterfaceElements;
    public static GameObject GameElements;
    public GameObject Camera;
    GameObject Player;
    private Vector3 cameraPositionSaved;
    private float cameraSizeSaved;
    GameObject DialogBoxTextObject;
    private int day;
    private int stageCount;

    public void LoadData(GameData data)
    {
        day = data.dayCount[0];
        stageCount = data.stageCount;
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
        if (day == 0)
        {
            StartCoroutine(ScriptFirstTryLoader());
        }
        else if (stageCount < 1)
        {
            StartCoroutine(ScriptRetryLoader());
        }
        else
        {
            StartCoroutine(ScriptExpertLoader());
        }

    }



    IEnumerator ScriptFirstTryLoader()
    {
        DialogBoxTextObject.GetComponent<DialogBoxTextTyper>().LoadScript("Text/Stage1-0/FirstTryOpening1-0");
        yield return new WaitWhile(() => InputDecoder.isGameInScript);
    }

    IEnumerator ScriptRetryLoader()
    {
        DialogBoxTextObject.GetComponent<DialogBoxTextTyper>().LoadScript("Text/Stage1-0/RetryOpening1-0");
        yield return new WaitWhile(() => InputDecoder.isGameInScript);
    }

    IEnumerator ScriptExpertLoader()
    {
        DialogBoxTextObject.GetComponent<DialogBoxTextTyper>().LoadScript("Text/Stage1-0/ExpertOpening1-0");
        yield return new WaitWhile(() => InputDecoder.isGameInScript);
    }


}
