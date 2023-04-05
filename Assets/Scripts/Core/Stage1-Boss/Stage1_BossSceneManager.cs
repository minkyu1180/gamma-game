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
    private bool itemGet;
    private int stageCount;
    public GameObject bossPatternController;
    private BossPatternControllerScript bossPatternControllerScript;
    bool IsFasterActivated = false;
    bool IsBikeAttackEnd = false;
    bool IsBikeStopSoundHeard = false;
    public GameObject feverPanel;
    public AudioSource audioSource;
    public AudioClip bikeEndSound;
    //public AudioSource audioSource;
    //public float fasterSpeed = 1.1f;

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
        bossPatternControllerScript = bossPatternController.GetComponent<BossPatternControllerScript>();
        feverPanel = GameObject.Find("FeverPanel");

        audioSource = gameObject.GetComponent<AudioSource>();
        bikeEndSound = Resources.Load("Sound/Voice/bikeEndSound") as AudioClip;
        //audioSource = gameObject.GetComponent<AudioSource>();

        StartCoroutine(ScriptLoader());

    }



    IEnumerator ScriptLoader()
    {
        DialogBoxTextObject.GetComponent<DialogBoxTextTyper>().LoadScript("Text/Stage1-Boss/Opening");
        yield return new WaitWhile(() => InputDecoder.isGameInScript);
        
        //bossPatternControllerScript.StartBodyCrushPattern();
        //bossPatternControllerScript.StartRockDownPattern();
        bossPatternControllerScript.StartAllMixPattern();
    }

    IEnumerator ScriptAfterItemGetLoader()
    {
        DialogBoxTextObject.GetComponent<DialogBoxTextTyper>().LoadScript("Text/Stage1-Boss/OpeningAfterItemGet");
        yield return new WaitWhile(() => InputDecoder.isGameInScript);
    }

    void Update()
    {
        if (!IsFasterActivated && Player.transform.position.x >= 190f)
        {
            bossPatternControllerScript.FasterPattern();
            Debug.Log("ISFASTERACTIVATED");
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


    }

}
