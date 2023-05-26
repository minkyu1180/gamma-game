using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class OpeningSceneManager : MonoBehaviour
{
    public static GameObject InterfaceElements;
    public static GameObject GameElements;
    public GameObject Camera;
    GameObject Player;
    private Vector3 cameraPositionSaved;
    private float cameraSizeSaved;
    GameObject DialogBoxTextObject;
    GameObject NPCJihee;
    GameObject NPCDahye;
    void Start()
    {
        InputDecoder.isGameInScript = true;
        InputDecoder.isConditionWaiting = true;
        DialogBoxTextObject = GameObject.Find("DialogBoxText");
        InterfaceElements = GameObject.Find("UI_Elements");
        GameElements = GameObject.Find("GAME_Elements");
        Camera = GameObject.Find("MainCamera");
        Player = GameObject.Find("Minkyu");
        NPCJihee = GameObject.Find("Jihee");
        NPCDahye = GameObject.Find("Dahye");
        cameraPositionSaved = Camera.transform.position;
        cameraSizeSaved = Camera.GetComponent<Camera>().orthographicSize;
        StartCoroutine(ScriptLoader());

    }



    IEnumerator ScriptLoader()
    {
        DialogBoxTextObject.GetComponent<DialogBoxTextTyper>().LoadScript("Text/Opening/Opening");
        yield return new WaitWhile(() => InputDecoder.isGameInScript);


        //Camera.transform.position = new Vector3(Target.transform.position.x, transform.position.y, -10);
        //CameraMovement.CameraGameMode = false;
        

        //camera 조정 (넓게 찍음)
        yield return new WaitForSeconds(3.0f);

        //전체를 비추다 한 2초후에 (두명만 찍음) 
        Camera.transform.position = new Vector3(-1.2f, -3.3f, cameraPositionSaved.z);
        Camera.GetComponent<Camera>().orthographicSize = 3.3f;

        yield return new WaitForSeconds(2.0f);

        InputDecoder.isGameInScript = true;
        InputDecoder.InterfaceElements.SetActive(true);
        DialogBoxTextObject.GetComponent<DialogBoxTextTyper>().LoadScript("Text/Opening/Opening2");
        yield return new WaitWhile(() => InputDecoder.isGameInScript);

        Camera.transform.position = cameraPositionSaved;
        Camera.GetComponent<Camera>().orthographicSize = cameraSizeSaved;

        NPCJihee.GetComponent<Button>().interactable = true;
        yield return new WaitWhile(() => InputDecoder.isConditionWaiting);
        
        // 관장 등장!
        NPCDahye.GetComponent<SpriteRenderer>().enabled = true;

        InputDecoder.isGameInScript = true;
        InputDecoder.InterfaceElements.SetActive(true);
        DialogBoxTextObject.GetComponent<DialogBoxTextTyper>().LoadScript("Text/Opening/Opening3");
        yield return new WaitWhile(() => InputDecoder.isGameInScript);



        // 집에 가자
        // 좋다
        // 야호

        // 밤하늘. 씬. 터벅터벅터벅.
        // 뭔가 부족함을 느낀다
        // ~ opening3

        yield return new WaitForSeconds(2.0f);


        // UI 가림. 한 3초 기다림.



        InputDecoder.isGameInScript = true;
        InputDecoder.InterfaceElements.SetActive(true);
        DialogBoxTextObject.GetComponent<DialogBoxTextTyper>().LoadScript("Text/Opening/Opening4");
        yield return new WaitWhile(() => InputDecoder.isGameInScript);

        // Opening 4 시작. 
        // 그로부터 얼마 후
        // 대 충 편 지

        // 용사라... 하고.

        // 1초 기다림

        SceneManager.LoadScene("Pre1StageScene");





    }


    void Update()
    {
        //Auto wrap (minkyu)
        //InputDecoder.InterfaceElements.SetActive(true);
        //Dialog load
        //This is the text load way.

        /*
        if (Input.GetKeyDown("h"))
        {
            if (InterfaceElements.activeInHierarchy)
            {
                InterfaceElements.SetActive(false);
            }
            else 
            {
                InterfaceElements.SetActive(true);
            }
        }
        */
        //UI hidding func
        
    }
}
