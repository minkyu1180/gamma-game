using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class Stage1_1SceneManager : MonoBehaviour, IDataPersistence
{
    public static GameObject InterfaceElements;
    public static GameObject GameElements;
    public GameObject Camera;
    public GameObject Chunbok;
    GameObject Player;
    private Vector3 cameraPositionSaved;
    private float cameraSizeSaved;
    GameObject DialogBoxTextObject;
    GameObject dataPersistenceManager;

    bool didTrueClearStage1;
    bool didClearStage1;
    bool didSeeStage1_1;
    bool didClear1_2Hidden;


    public void LoadData(GameData data)
    {
        this.didTrueClearStage1 = data.didTrueClearStage1;
        this.didClearStage1 = data.didClearStage1;
        this.didSeeStage1_1 = data.didSeeStage1_1;
        this.didClear1_2Hidden = data.didClearStage1_2Hidden;
    }

    public void SaveData(ref GameData data)
    {
        data.didSeeStage1_1 = this.didSeeStage1_1;
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
        if (didTrueClearStage1) textLocation = "Text/Stage1-1/AfterAllOpening";
        else
        {
            if (didClearStage1)
            {
                if (didClear1_2Hidden) textLocation = "Text/Stage1-1/ClearAfterItemReOpening";
                else textLocation = "Text/Stage1-1/ClearReOpening";
            }
            else
            {
                if (didSeeStage1_1) textLocation = "Text/Stage1-1/ReOpening";
                else textLocation = "Text/Stage1-1/Opening";
            }
        }
        StartCoroutine(OpeningScriptLoad(textLocation));

    }



    IEnumerator OpeningScriptLoad(string textLocation)
    {
        DialogBoxTextObject.GetComponent<DialogBoxTextTyper>().LoadScript(textLocation);
        yield return new WaitWhile(() => InputDecoder.isGameInScript);
        Chunbok.transform.localScale = new Vector3(-1, 1, 1);
        yield return new WaitForSeconds(0.5f); 
        Chunbok.GetComponent<Animator>().SetTrigger("FireTrigger");
        Chunbok.GetComponent<Rigidbody2D>().AddForce(new Vector2(-400f, 0f));        
        yield return new WaitForSeconds(1.0f);  
        Chunbok.SetActive(false);

        InputDecoder.isGameInScript = true;
        InputDecoder.InterfaceElements.SetActive(true);
        DialogBoxTextObject.GetComponent<DialogBoxTextTyper>().LoadScript(textLocation + "2");
        yield return new WaitWhile(() => InputDecoder.isGameInScript);

        didSeeStage1_1 = true;
        bool saved = false;
        saved = dataPersistenceManager.GetComponent<DataPersistenceManager>().SaveGame();
        yield return new WaitWhile(() => !saved);

    }


}
