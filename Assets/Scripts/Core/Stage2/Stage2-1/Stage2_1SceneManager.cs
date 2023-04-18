using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class Stage2_1SceneManager : MonoBehaviour, IDataPersistence
{
    public static GameObject InterfaceElements;
    public static GameObject GameElements;
    public GameObject Camera;
    GameObject Player;
    GameObject Susang;
    private Vector3 cameraPositionSaved;
    private float cameraSizeSaved;
    GameObject DialogBoxTextObject;
    GameObject dataPersistenceManager;

    bool didTrueClearStage2;
    bool didClearStage2;
    bool didSeeStage2_1;
    bool didClear2_2Hidden;

    public void LoadData(GameData data)
    {
        this.didTrueClearStage2 = data.didTrueClearStage2;
        this.didClearStage2 = data.didClearStage2;
        this.didSeeStage2_1 = data.didSeeStage2_1;
        this.didClear2_2Hidden = data.didClearStage2_2Hidden;
    }

    public void SaveData(ref GameData data)
    {
        data.didSeeStage2_1 = this.didSeeStage2_1;
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
        if (didTrueClearStage2) textLocation = "Text/Stage2-1/AfterAllOpening";
        else
        {
            if (didClearStage2)
            {
                if (didClear2_2Hidden) textLocation = "Text/Stage2-1/ClearAfterItemReOpening";
                else textLocation = "Text/Stage2-1/ClearReOpening";
            }
            else
            {
                if (didSeeStage2_1) textLocation = "Text/Stage2-1/ReOpening";
                else textLocation = "Text/Stage2-1/Opening";
            }
        }
        StartCoroutine(OpeningScriptLoad(textLocation));

    }



    IEnumerator OpeningScriptLoad(string textLocation)
    {
        DialogBoxTextObject.GetComponent<DialogBoxTextTyper>().LoadScript(textLocation);
        yield return new WaitWhile(() => InputDecoder.isGameInScript);

        didSeeStage2_1 = true;
        bool saved = false;
        saved = dataPersistenceManager.GetComponent<DataPersistenceManager>().SaveGame();
        yield return new WaitWhile(() => !saved);
        Susang.SetActive(false);
    }


}
