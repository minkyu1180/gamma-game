using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine;
using UnityEngine.UI;
using System;
using UnityEngine.SceneManagement;



// IPointerClickHandler
public class StageUIScript : MonoBehaviour, IDataPersistence
{
    GameObject DialogBoxTextObject;
    public GameObject Stage1Button;
    public GameObject Stage2Button;
    public GameObject Stage3Button;
    int stageCount;    
    
    public void LoadData(GameData data)
    {
        this.stageCount = data.stageCount;
    }
    public void SaveData(ref GameData data){}
    
    void Start()
    {
        DialogBoxTextObject = GameObject.Find("DialogBoxText");
        if (stageCount == 0)
        {
            Stage2Button.GetComponent<SpriteRenderer>().color = new Color(0.3f,0.3f,0.3f,1f);
            Stage3Button.GetComponent<SpriteRenderer>().color = new Color(0.3f,0.3f,0.3f,1f);
        }
        else if (stageCount == 1)
        {
            Stage3Button.GetComponent<SpriteRenderer>().color = new Color(0.3f,0.3f,0.3f,1f);
        }
    }
    public void Stage1Click()
    {
        InputDecoder.isGameInScript = true;
        StartCoroutine(Button1Script());
    }

    public void Stage2Click()
    {
        InputDecoder.isGameInScript = true;
        //StartCoroutine(JiheeScript());
        GetComponent<Button>().interactable = false;
    }

    public void Stage3Click()
    {

    }

    IEnumerator Button1Script()
    {
        Stage1Button.GetComponent<Button>().interactable = false;
        InputDecoder.InterfaceElements.SetActive(true);
        DialogBoxTextObject.GetComponent<DialogBoxTextTyper>().LoadScript("Text/Lobby/Stage1-1~10Go " + Convert.ToString(UnityEngine.Random.Range(1,8)));
        yield return new WaitWhile(() => InputDecoder.isGameInScript);

        SceneManager.LoadScene("Stage 1-0");

        
    }
}