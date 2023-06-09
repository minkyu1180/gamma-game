using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class SideStorySceneManager : MonoBehaviour
{
    public static GameObject InterfaceElements;
    public static GameObject GameElements;
    public GameObject Camera;
    GameObject Player;
    private Vector3 cameraPositionSaved;
    private float cameraSizeSaved;
    GameObject DialogBoxTextObject;
    GameObject NPCDahye;
    AudioSource bgmManager;
    AudioClip transformSound;
    void Start()
    {
        InputDecoder.isGameInScript = true;
        InputDecoder.isConditionWaiting = true;
        DialogBoxTextObject = GameObject.Find("DialogBoxText");
        InterfaceElements = GameObject.Find("UI_Elements");
        GameElements = GameObject.Find("GAME_Elements");
        Camera = GameObject.Find("MainCamera");
        bgmManager = GameObject.Find("BGMControlManager").GetComponent<AudioSource>();
        Player = GameObject.Find("Minkyu");
        NPCDahye = GameObject.Find("Dahye");
        cameraPositionSaved = Camera.transform.position;
        cameraSizeSaved = Camera.GetComponent<Camera>().orthographicSize;
        transformSound = Resources.Load("Sound/Voice/transformSound") as AudioClip;

        StartCoroutine(ScriptLoader());

    }



    IEnumerator ScriptLoader()
    {
        
        DialogBoxTextObject.GetComponent<DialogBoxTextTyper>().LoadScript("Text/SideStory/Opening1");
        yield return new WaitWhile(() => InputDecoder.isGameInScript);


        Player.GetComponent<PlayerMovement>().autoWalk(1.4f, true);
        yield return new WaitForSeconds(0.8f);
        Player.GetComponent<PlayerMovement>().autoJump();
        yield return new WaitForSeconds(1.3f);
        

        //전체를 비추다 한 2초후에 (두명만 찍음) 
        Camera.transform.position = new Vector3(3.5f, -2f, cameraPositionSaved.z);
        Camera.GetComponent<Camera>().orthographicSize = 3.3f;

        yield return new WaitForSeconds(1.0f);

        NPCDahye.GetComponent<SpriteRenderer>().flipX = false;
        

        InputDecoder.isGameInScript = true;
        InputDecoder.InterfaceElements.SetActive(true);
        DialogBoxTextObject.GetComponent<DialogBoxTextTyper>().LoadScript("Text/SideStory/Opening2");
        yield return new WaitWhile(() => InputDecoder.isGameInScript);
        

        // -- Do Something Animation -- //


        NPCDahye.GetComponent<Animator>().SetTrigger("transformTrigger");
        bgmManager.PlayOneShot(transformSound);
        yield return new WaitForSeconds(5.5f);


        //I'm the MaWang!
        InputDecoder.isGameInScript = true;
        InputDecoder.InterfaceElements.SetActive(true);
        DialogBoxTextObject.GetComponent<DialogBoxTextTyper>().LoadScript("Text/SideStory/Opening3");
        yield return new WaitWhile(() => InputDecoder.isGameInScript);

        //Camera Go To Origin
        Camera.transform.position = cameraPositionSaved;
        Camera.GetComponent<Camera>().orthographicSize = cameraSizeSaved;
        Coroutine fadeOut = StartCoroutine(FadeOutMethod());


        InputDecoder.isGameInScript = true;
        InputDecoder.InterfaceElements.SetActive(true);
        DialogBoxTextObject.GetComponent<DialogBoxTextTyper>().LoadScript("Text/SideStory/Opening4");
        yield return new WaitWhile(() => InputDecoder.isGameInScript);

        if (fadeOut != null) StopCoroutine(fadeOut);
        bgmManager.volume = 0.5f;

        InputDecoder.isGameInScript = true;
        InputDecoder.InterfaceElements.SetActive(true);
        DialogBoxTextObject.GetComponent<DialogBoxTextTyper>().LoadScript("Text/SideStory/Opening5");
        yield return new WaitWhile(() => InputDecoder.isGameInScript);


        // -- SideStory 본 사실 저장 -- // 이건 엔딩 씬 누를 때
        // -- SceneManager.LoadScene("LobbyScene"); --//
        SceneManager.LoadScene("EndingCreditScene");

    }

    IEnumerator FadeOutMethod()
    {
        while (bgmManager.volume != 0f)
        {
            float tmp = bgmManager.volume - 0.01f;
            if (tmp < 0f) bgmManager.volume = 0f;
            else bgmManager.volume = tmp;
            yield return new WaitForSeconds(0.1f);
        }
    }
}
