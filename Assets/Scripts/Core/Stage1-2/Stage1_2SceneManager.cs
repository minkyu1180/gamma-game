using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class Stage1_2SceneManager : MonoBehaviour, IDataPersistence
{
    public static GameObject InterfaceElements;
    public static GameObject GameElements;
    public GameObject Camera;
    public GameObject Chunbok;
    GameObject Player;
    private Vector3 cameraPositionSaved;
    private float cameraSizeSaved;
    GameObject DialogBoxTextObject;
    private int day;
    private int stageCount;

    public void LoadData(GameData data)
    {
        day = data.dayCount[0];
        stageCount = data.stageCount;
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
        
        if (day == 0)
        {
            StartCoroutine(ScriptDay0Loader());
        }
        else if (stageCount < 1)
        {
            StartCoroutine(ScriptDayOver0Loader());
        }
        else
        {
            StartCoroutine(ScriptClearRetryLoader());
        }

    }



    IEnumerator ScriptDay0Loader()
    {
        DialogBoxTextObject.GetComponent<DialogBoxTextTyper>().LoadScript("Text/Stage1-2/Opening");
        yield return new WaitWhile(() => InputDecoder.isGameInScript);
        Chunbok.transform.localScale = new Vector3(-1, 1, 1);
        yield return new WaitForSeconds(0.5f); 
        Chunbok.GetComponent<Animator>().SetTrigger("FireTrigger");
        Chunbok.GetComponent<Rigidbody2D>().AddForce(new Vector2(-400f, 0f));        
        yield return new WaitForSeconds(1.0f);  
        Chunbok.SetActive(false);

        InputDecoder.isGameInScript = true;
        InputDecoder.InterfaceElements.SetActive(true);
        DialogBoxTextObject.GetComponent<DialogBoxTextTyper>().LoadScript("Text/Stage1-2/Opening2");
        yield return new WaitWhile(() => InputDecoder.isGameInScript);

    }

    IEnumerator ScriptDayOver0Loader()
    {
        DialogBoxTextObject.GetComponent<DialogBoxTextTyper>().LoadScript("Text/Stage1-2/ReOpening");
        yield return new WaitWhile(() => InputDecoder.isGameInScript);
        Chunbok.transform.localScale = new Vector3(-1, 1, 1);
        yield return new WaitForSeconds(0.5f); 
        Chunbok.GetComponent<Animator>().SetTrigger("FireTrigger");
        Chunbok.GetComponent<Rigidbody2D>().AddForce(new Vector2(-400f, 0f));        
        yield return new WaitForSeconds(1.0f);  
        Chunbok.SetActive(false);

        InputDecoder.isGameInScript = true;
        InputDecoder.InterfaceElements.SetActive(true);
        DialogBoxTextObject.GetComponent<DialogBoxTextTyper>().LoadScript("Text/Stage1-2/ReOpening2");
        yield return new WaitWhile(() => InputDecoder.isGameInScript);
    }

    IEnumerator ScriptClearRetryLoader()
    {
        DialogBoxTextObject.GetComponent<DialogBoxTextTyper>().LoadScript("Text/Stage1-2/ClearReOpening");
        yield return new WaitWhile(() => InputDecoder.isGameInScript);
        Chunbok.transform.localScale = new Vector3(-1, 1, 1);
        yield return new WaitForSeconds(0.5f); 
        Chunbok.GetComponent<Animator>().SetTrigger("FireTrigger");
        Chunbok.GetComponent<Rigidbody2D>().AddForce(new Vector2(-400f, 0f));        
        yield return new WaitForSeconds(1.0f);  
        Chunbok.SetActive(false);

        InputDecoder.isGameInScript = true;
        InputDecoder.InterfaceElements.SetActive(true);
        DialogBoxTextObject.GetComponent<DialogBoxTextTyper>().LoadScript("Text/Stage1-2/ClearReOpening2");
        yield return new WaitWhile(() => InputDecoder.isGameInScript);
    }


}
