using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class Stage3_BossSceneManager : MonoBehaviour, IDataPersistence
{
    public static GameObject InterfaceElements;
    public static GameObject GameElements;
    public GameObject Camera;
    GameObject Player;
    private Vector3 cameraPositionSaved;
    private float cameraSizeSaved;
    GameObject DialogBoxTextObject;
    GameObject dataPersistenceManager;
    GameObject Jihee;
    AudioSource audioSource;

    AudioClip punchReadySound;
    AudioClip punchGoSound;
    AudioClip punchAfterSound;

    bool didTrueClearStage3;
    bool didClearStage3;
    bool didSeeStage3_Boss;
    bool didClearStage3_2Hidden;
    int StageCount;
    int dayCount;

    public void LoadData(GameData data)
    {
        this.didTrueClearStage3 = data.didTrueClearStage3;
        this.didClearStage3 = data.didClearStage3;
        this.didSeeStage3_Boss = data.didSeeStage3_Boss;
        this.didClearStage3_2Hidden = data.didClearStage3_2Hidden;
        this.dayCount = data.dayCount;
        this.StageCount = data.stageCount;
    }

    public void SaveData(ref GameData data)
    {
        data.didSeeStage3_Boss = this.didSeeStage3_Boss;
        data.didClearStage3 = this.didClearStage3;
        data.didTrueClearStage3 = this.didTrueClearStage3;
        data.stageCount = this.StageCount;
        data.dayCount = this.dayCount;
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
        Jihee = GameObject.Find("Jihee");
        audioSource = Jihee.GetComponent<AudioSource>();
        punchReadySound = Resources.Load("Sound/Voice/PunchReadySound") as AudioClip;
        punchGoSound = Resources.Load("Sound/Voice/PunchGoSound") as AudioClip;
        punchAfterSound = Resources.Load("Sound/Voice/PunchAfterSound") as AudioClip;

        dataPersistenceManager = GameObject.Find("DataPersistenceManager");
        Jihee.SetActive(false);
        if (StageCount != 3) StageCount = 3;
        dayCount++;
        if (didTrueClearStage3)
        {
            StartCoroutine(AfterAllOpeningScriptLoad());
        }
        else
        {
            if (didClearStage3)
            {
                if (didClearStage3_2Hidden) StartCoroutine(ClearAfterItemOpeningScriptLoad());
                else StartCoroutine(ClearReOpeningScriptLoad());
            }
            else
            {
                StartCoroutine(OpeningScriptLoad());
            }
        }
    }



    IEnumerator OpeningScriptLoad()
    {
        string textLocation = "Text/Stage3-Boss/Opening1";
        DialogBoxTextObject.GetComponent<DialogBoxTextTyper>().LoadScript(textLocation);
        yield return new WaitWhile(() => InputDecoder.isGameInScript);

        Jihee.SetActive(true);
        cameraPositionSaved = Camera.transform.position;
        cameraSizeSaved = Camera.GetComponent<Camera>().orthographicSize;
        
        
        InputDecoder.isGameInScript = true;
        InputDecoder.InterfaceElements.SetActive(true);
        textLocation = "Text/Stage3-Boss/Opening2";
        DialogBoxTextObject.GetComponent<DialogBoxTextTyper>().LoadScript(textLocation);
        yield return new WaitWhile(() => InputDecoder.isGameInScript);

        
        Jihee.GetComponent<Animator>().SetBool("GetReady", true);

        InputDecoder.isGameInScript = true;
        InputDecoder.InterfaceElements.SetActive(true);
        textLocation = "Text/Stage3-Boss/Opening3";
        DialogBoxTextObject.GetComponent<DialogBoxTextTyper>().LoadScript(textLocation);
        yield return new WaitWhile(() => InputDecoder.isGameInScript);

        Jihee.GetComponent<Animator>().SetTrigger("DoPunch");
        StartCoroutine(MoveJihee());
        yield return new WaitForSeconds(2.1f);
        
        
        
        InputDecoder.isGameInScript = true;
        InputDecoder.InterfaceElements.SetActive(true);
        textLocation = "Text/Stage3-Boss/Opening4";
        DialogBoxTextObject.GetComponent<DialogBoxTextTyper>().LoadScript(textLocation);
        yield return new WaitWhile(() => InputDecoder.isGameInScript);

        didSeeStage3_Boss = true;
        didClearStage3 = true;
        bool saved = false;
        saved = dataPersistenceManager.GetComponent<DataPersistenceManager>().SaveGame();
        yield return new WaitWhile(() => !saved);

        SceneManager.LoadScene("Ending");
    }

    IEnumerator ClearReOpeningScriptLoad()
    {
        Jihee.SetActive(true);
        string textLocation = "Text/Stage3-Boss/ClearReOpening";
        DialogBoxTextObject.GetComponent<DialogBoxTextTyper>().LoadScript(textLocation);
        yield return new WaitWhile(() => InputDecoder.isGameInScript);

        didSeeStage3_Boss = true;
        didClearStage3 = true;
        bool saved = false;
        saved = dataPersistenceManager.GetComponent<DataPersistenceManager>().SaveGame();
        yield return new WaitWhile(() => !saved);

        SceneManager.LoadScene("LobbyScene");
    }

    IEnumerator ClearAfterItemOpeningScriptLoad()
    {
        Jihee.SetActive(true);
        string textLocation = "Text/Stage3-Boss/ClearAfterItemReOpening";
        DialogBoxTextObject.GetComponent<DialogBoxTextTyper>().LoadScript(textLocation);
        yield return new WaitWhile(() => InputDecoder.isGameInScript);

        didSeeStage3_Boss = true;
        didTrueClearStage3 = true;
        bool saved = false;
        saved = dataPersistenceManager.GetComponent<DataPersistenceManager>().SaveGame();
        yield return new WaitWhile(() => !saved);

        SceneManager.LoadScene("LobbyScene");
    }

    IEnumerator AfterAllOpeningScriptLoad()
    {
        Jihee.SetActive(true);
        string textLocation = "Text/Stage3-Boss/AfterAllOpening";
        DialogBoxTextObject.GetComponent<DialogBoxTextTyper>().LoadScript(textLocation);
        yield return new WaitWhile(() => InputDecoder.isGameInScript);

        didSeeStage3_Boss = true;
        bool saved = false;
        saved = dataPersistenceManager.GetComponent<DataPersistenceManager>().SaveGame();
        yield return new WaitWhile(() => !saved);

        SceneManager.LoadScene("LobbyScene");
    }


    IEnumerator MoveJihee()
    {
        Camera.GetComponent<Camera>().orthographicSize = 3.3f;
        audioSource.PlayOneShot(punchReadySound);

        yield return new WaitForSeconds(1.5f);
        audioSource.PlayOneShot(punchGoSound);
        Jihee.GetComponent<Rigidbody2D>().velocity = new Vector2(-17.0f, 0f);
        
        Coroutine Cam = StartCoroutine(CameraZoomTransfrom());


        yield return new WaitForSeconds(0.3f);
        Player.GetComponentInChildren<HealthScript>().Hit(Player.GetComponentInChildren<HealthScript>().hp - 1);
        audioSource.PlayOneShot(punchAfterSound);
        Jihee.GetComponent<Rigidbody2D>().velocity = new Vector2(-0.03f, 0f);


        yield return new WaitForSeconds(0.5f);

        StopCoroutine(Cam);
        Camera.GetComponent<Camera>().orthographicSize = cameraSizeSaved;
        Camera.transform.position = cameraPositionSaved;

        Jihee.GetComponent<Rigidbody2D>().velocity = new Vector2(1f, 0f);

        


        yield return new WaitForSeconds(1.0f);
        Jihee.GetComponent<Rigidbody2D>().velocity = new Vector2(0f, 0f);
    }

    IEnumerator CameraZoomTransfrom()
    {
        while (true)
        {
            Camera.transform.position = Camera.transform.position - new Vector3(0.05f, -0.03f, 0f);
            Camera.GetComponent<Camera>().orthographicSize = Camera.GetComponent<Camera>().orthographicSize - 0.01f;
            yield return new WaitForSeconds(0.01f);
        }
    }
}
