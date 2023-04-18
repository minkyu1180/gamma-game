using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class Stage2_0SceneManager : MonoBehaviour, IDataPersistence
{
    public static GameObject InterfaceElements;
    public static GameObject GameElements;
    public GameObject Camera;
    GameObject Player;
    private Vector3 cameraPositionSaved;
    private float cameraSizeSaved;
    GameObject DialogBoxTextObject;
    GameObject dataPersistenceManager;
    GameObject Susang;

    bool didTrueClearStage2;
    bool didClearStage2;
    bool didSeeStage2_0;
    bool didClear2_2Hidden;

    public void LoadData(GameData data)
    {
        this.didTrueClearStage2 = data.didTrueClearStage2;
        this.didClearStage2 = data.didClearStage2;
        this.didSeeStage2_0 = data.didSeeStage2_0;
        this.didClear2_2Hidden = data.didClearStage2_2Hidden;
    }

    public void SaveData(ref GameData data)
    {
        data.didSeeStage2_0 = this.didSeeStage2_0;
    }

    void Start()
    {
        InputDecoder.isGameInScript = true;
        InputDecoder.isConditionWaiting = true;
        DialogBoxTextObject = GameObject.Find("DialogBoxText");
        InterfaceElements = GameObject.Find("UI_Elements");
        GameElements = GameObject.Find("GAME_Elements");
        Camera = GameObject.Find("MainCamera");
        Player = GameObject.Find("Minkyu");
        Susang = GameObject.Find("Susang");
        cameraPositionSaved = Camera.transform.position;
        cameraSizeSaved = Camera.GetComponent<Camera>().orthographicSize;
        dataPersistenceManager = GameObject.Find("DataPersistenceManager");

        string textLocation;
        if (didTrueClearStage2) textLocation = "Text/Stage2-0/AfterAllOpening";
        else
        {
            if (didClearStage2)
            {
                if (didClear2_2Hidden) textLocation = "Text/Stage2-0/ClearAfterItemReOpening";
                else textLocation = "Text/Stage2-0/ClearReOpening";
            }
            else
            {
                if (didSeeStage2_0) textLocation = "Text/Stage2-0/ReOpening";
                else textLocation = "Text/Stage2-0/Opening";
            }
        }
        StartCoroutine(OpeningScriptLoad(textLocation));

    }



    IEnumerator OpeningScriptLoad(string textLocation)
    {
        DialogBoxTextObject.GetComponent<DialogBoxTextTyper>().LoadScript(textLocation);
        yield return new WaitWhile(() => InputDecoder.isGameInScript);

        didSeeStage2_0 = true;
        bool saved = false;
        saved = dataPersistenceManager.GetComponent<DataPersistenceManager>().SaveGame();
        yield return new WaitWhile(() => !saved);
        Susang.SetActive(false);
    }


}
