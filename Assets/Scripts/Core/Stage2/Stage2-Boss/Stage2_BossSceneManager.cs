using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class Stage2_BossSceneManager : MonoBehaviour, IDataPersistence
{
    public static GameObject InterfaceElements;
    public static GameObject GameElements;
    public GameObject Camera;
    GameObject Player;
    private Vector3 cameraPositionSaved;
    private float cameraSizeSaved;
    GameObject DialogBoxTextObject;
    GameObject dataPersistenceManager;
    GameObject Boss;

    bool IsFinalScriptLoaded = false;

    bool didTrueClearStage2;
    bool didClearStage2;
    bool didSeeStage2_Boss;
    bool didClear2_2Hidden;

    private int dayCount;
    private int stageCount;

    public void LoadData(GameData data)
    {
        this.didTrueClearStage2 = data.didTrueClearStage2;
        this.didClearStage2 = data.didClearStage2;
        this.didSeeStage2_Boss = data.didSeeStage2_Boss;
        this.didClear2_2Hidden = data.didClearStage2_2Hidden;
        this.dayCount = data.dayCount;
        this.stageCount = data.stageCount;
    }

    public void SaveData(ref GameData data)
    {
        data.didSeeStage2_Boss = this.didSeeStage2_Boss;
        data.didTrueClearStage2 = this.didTrueClearStage2;
        data.didClearStage2 = this.didClearStage2;
        data.stageCount = this.stageCount;
        data.dayCount = this.dayCount + 1;
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
        Boss = GameObject.Find("Boss");
        cameraPositionSaved = Camera.transform.position;
        cameraSizeSaved = Camera.GetComponent<Camera>().orthographicSize;
        dataPersistenceManager = GameObject.Find("DataPersistenceManager");

        string textLocation;
        if (didTrueClearStage2) textLocation = "Text/Stage2-Boss/AfterAllOpening";
        else
        {
            if (didClearStage2)
            {
                if (didClear2_2Hidden) textLocation = "Text/Stage2-Boss/ClearAfterItemReOpening";
                else textLocation = "Text/Stage2-Boss/ClearReOpening";
            }
            else
            {
                if (didSeeStage2_Boss) textLocation = "Text/Stage2-Boss/ReOpening";
                else textLocation = "Text/Stage2-Boss/Opening";
            }
        }
        StartCoroutine(OpeningScriptLoad(textLocation));

    }



    IEnumerator OpeningScriptLoad(string textLocation)
    {
        DialogBoxTextObject.GetComponent<DialogBoxTextTyper>().LoadScript(textLocation);
        yield return new WaitWhile(() => InputDecoder.isGameInScript);

        didSeeStage2_Boss = true;
        bool saved = false;
        saved = dataPersistenceManager.GetComponent<DataPersistenceManager>().SaveGame();
        yield return new WaitWhile(() => !saved);
        Boss.GetComponent<SusangPatternScript>().BossPatternManager();
    }

    void Update()
    {
        if (!IsFinalScriptLoaded && Boss.GetComponent<SusangPatternScript>().hp <= 0 && !Player.GetComponentInChildren<HealthScript>().IsFainted)
        {
            string textLocation;
            if (didTrueClearStage2) textLocation = "Text/Stage2-Boss/Ending/AfterAllEnding";
            else
            {
                if (didClearStage2) 
                {
                    if (didClear2_2Hidden) textLocation = "Text/Stage2-Boss/Ending/ClearAfterItemReEnding";
                    else textLocation = "Text/Stage2-Boss/Ending/ClearReEnding";
                }
                else textLocation = "Text/Stage2-Boss/Ending/Ending";
            }

            IsFinalScriptLoaded = true;

            
            StartCoroutine(EndingScriptLoad(textLocation));
        }

        
    }


    IEnumerator EndingScriptLoad(string textLocation)
    {
        yield return new WaitForSeconds(2.0f);
        InputDecoder.isGameInScript = true;
        InputDecoder.InterfaceElements.SetActive(true);

        DialogBoxTextObject.GetComponent<DialogBoxTextTyper>().LoadScript(textLocation);
        yield return new WaitWhile(() => InputDecoder.isGameInScript);

        if (didClear2_2Hidden)
        {
            didTrueClearStage2 = true;
        }
        else
        {
            didClearStage2 = true;
        }

        if (stageCount <= 2)stageCount = 2;
        bool saved = false;
        saved = dataPersistenceManager.GetComponent<DataPersistenceManager>().SaveGame();
        yield return new WaitWhile(() => !saved);

        SceneManager.LoadScene("LobbyScene");        
    }

}
