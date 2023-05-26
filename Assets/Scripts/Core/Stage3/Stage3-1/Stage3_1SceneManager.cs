using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class Stage3_1SceneManager : MonoBehaviour, IDataPersistence
{
    public static GameObject InterfaceElements;
    public static GameObject GameElements;
    public GameObject Camera;
    GameObject Player;
    private Vector3 cameraPositionSaved;
    private float cameraSizeSaved;
    GameObject DialogBoxTextObject;
    GameObject dataPersistenceManager;

    bool didTrueClearStage3;
    bool didClearStage3;
    bool didSeeStage3_1;
    bool didClear3_2Hidden;

    public void LoadData(GameData data)
    {
        this.didTrueClearStage3 = data.didTrueClearStage3;
        this.didClearStage3 = data.didClearStage3;
        this.didSeeStage3_1 = data.didSeeStage3_1;
        this.didClear3_2Hidden = data.didClearStage3_2Hidden;
    }

    public void SaveData(ref GameData data)
    {
        data.didSeeStage3_1 = this.didSeeStage3_1;
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
        cameraPositionSaved = Camera.transform.position;
        cameraSizeSaved = Camera.GetComponent<Camera>().orthographicSize;
        dataPersistenceManager = GameObject.Find("DataPersistenceManager");

        string textLocation;
        if (didTrueClearStage3) textLocation = "Text/Stage3-1/AfterAllOpening";
        else
        {
            if (didClearStage3)
            {
                if (didClear3_2Hidden) textLocation = "Text/Stage3-1/ClearAfterItemReOpening";
                else textLocation = "Text/Stage3-1/ClearReOpening";
            }
            else
            {
                if (didSeeStage3_1) textLocation = "Text/Stage3-1/ReOpening";
                else textLocation = "Text/Stage3-1/Opening";
            }
        }
        StartCoroutine(OpeningScriptLoad(textLocation));

    }



    IEnumerator OpeningScriptLoad(string textLocation)
    {
        DialogBoxTextObject.GetComponent<DialogBoxTextTyper>().LoadScript(textLocation);
        Camera.GetComponent<AutoScroll3_1Camera>().CameraGameMode = false;
        yield return new WaitWhile(() => InputDecoder.isGameInScript);


        Camera.GetComponent<AutoScroll3_1Camera>().CameraGameMode = true;

        didSeeStage3_1 = true;
        bool saved = false;
        saved = dataPersistenceManager.GetComponent<DataPersistenceManager>().SaveGame();
        yield return new WaitWhile(() => !saved);
    }


}
