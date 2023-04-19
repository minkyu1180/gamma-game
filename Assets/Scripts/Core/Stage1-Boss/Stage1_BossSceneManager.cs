using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class Stage1_BossSceneManager : MonoBehaviour, IDataPersistence
{
    public static GameObject InterfaceElements;
    public static GameObject GameElements;
    public GameObject Camera;
    GameObject Player;
    private Vector3 cameraPositionSaved;
    private float cameraSizeSaved;
    GameObject DialogBoxTextObject;
    
    public GameObject bossPatternController;
    private BossPatternControllerScript bossPatternControllerScript;
    
    bool IsFasterActivated = false;
    bool IsBikeAttackEnd = false;
    bool IsBikeStopSoundHeard = false;
    bool IsFinalScriptLoaded = false;

    public GameObject feverPanel;
    public AudioSource audioSource;
    public AudioClip bikeEndSound;
    public GameObject dataPersistenceManager;

    public bool didTrueClearStage1;
    public bool didClearStage1;
    public bool didClear1_2Hidden;
    public bool didSeeStage1_Boss;

    private int dayCount;
    private int stageCount;

    public void LoadData(GameData data)
    {
        this.didTrueClearStage1 = data.didTrueClearStage1;
        this.didClearStage1 = data.didClearStage1;
        this.didClear1_2Hidden = data.didClearStage1_2Hidden;
        this.didSeeStage1_Boss = data.didSeeStage1_Boss;
        this.dayCount = data.dayCount;
        this.stageCount = data.stageCount;
    }

    public void SaveData(ref GameData data)
    {
        data.didSeeStage1_Boss = this.didSeeStage1_Boss;
        data.didTrueClearStage1 = this.didTrueClearStage1;
        data.didClearStage1 = this.didClearStage1;
        data.stageCount = this.stageCount;
        data.dayCount = this.dayCount + 1; //either case the day pass
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
        bossPatternControllerScript = bossPatternController.GetComponent<BossPatternControllerScript>();
        feverPanel = GameObject.Find("FeverPanel");

        audioSource = gameObject.GetComponent<AudioSource>();
        bikeEndSound = Resources.Load("Sound/Voice/bikeEndSound") as AudioClip;
        audioSource = gameObject.GetComponent<AudioSource>();
        dataPersistenceManager = GameObject.Find("DataPersistenceManager");

        string textLocation;
        
        if (didTrueClearStage1) textLocation = "Text/Stage1-Boss/AfterAllOpening";
        else
        {
            if (didClearStage1)
            {
                if (didClear1_2Hidden) textLocation = "Text/Stage1-Boss/ClearAfterItemReOpening";
                else textLocation = "Text/Stage1-Boss/ClearReOpening";
            }
            else
            {
                if (didSeeStage1_Boss) textLocation = "Text/Stage1-Boss/ReOpening";
                else textLocation = "Text/Stage1-Boss/Opening";
                
            }
        }
        StartCoroutine(OpeningScriptLoad(textLocation));
    }



    IEnumerator OpeningScriptLoad(string textLocation)
    {
        DialogBoxTextObject.GetComponent<DialogBoxTextTyper>().LoadScript(textLocation);
        yield return new WaitWhile(() => InputDecoder.isGameInScript);

        didSeeStage1_Boss = true;
        bool saved = false;
        saved = dataPersistenceManager.GetComponent<DataPersistenceManager>().SaveGame();
        yield return new WaitWhile(() => !saved);

        bossPatternControllerScript.StartAllMixPattern();
    }

    void Update()
    {
        if (!IsFasterActivated && Player.transform.position.x >= 190f)
        {
            bossPatternControllerScript.FasterPattern();
            feverPanel.GetComponent<morePowerPanelScript>().feverNotification();
            //audioSource.pitch = fasterSpeed;
            //audioSource.outputAudioMixerGroup.audioMixer.SetFloat("Pitch", 1f / fasterSpeed);
            IsFasterActivated = true;
        }

        if (!IsBikeAttackEnd && Player.transform.position.x >= 291f)
        {
            bossPatternControllerScript.StopAllMixPattern();
            IsBikeAttackEnd = true;
        }

        if (!IsBikeStopSoundHeard && Player.transform.position.x >= 325f)
        {
            audioSource.Stop();
            audioSource.PlayOneShot(bikeEndSound);
            
            IsBikeStopSoundHeard = true;
        }

        if (!IsFinalScriptLoaded && Player.transform.position.x >= 350f)
        {
            string textLocation;
            if (didTrueClearStage1) textLocation = "Text/Stage1-Boss/Ending/AfterAllEnding";
            else
            {
                if (didClearStage1) 
                {
                    if (didClear1_2Hidden) textLocation = "Text/Stage1-Boss/Ending/ClearAfterItemReEnding";
                    else textLocation = "Text/Stage1-Boss/Ending/ClearReEnding";
                }
                else textLocation = "Text/Stage1-Boss/Ending/Ending";
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

        if (didClear1_2Hidden)
        {
            didTrueClearStage1 = true;
        }
        else
        {
            didClearStage1 = true;
        }

        if (stageCount <= 1)stageCount = 1;
        bool saved = false;
        saved = dataPersistenceManager.GetComponent<DataPersistenceManager>().SaveGame();
        yield return new WaitWhile(() => !saved);

        SceneManager.LoadScene("LobbyScene");        
    }


}
