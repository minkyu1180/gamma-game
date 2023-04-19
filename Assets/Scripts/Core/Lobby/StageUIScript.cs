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

    public GameObject GoBossDirectQuest;
    int stageCount;
    bool didClearStage1_2;
    bool didClearStage2_2;
    public GameObject minkyu;

    public bool GoDirectBoss = false;

    public void LoadData(GameData data)
    {
        this.stageCount = data.stageCount;
        this.didClearStage1_2 = data.didClearStage1_2;
        this.didClearStage2_2 = data.didClearStage2_2;
    }
    public void SaveData(ref GameData data){}
    
    void Start()
    {
        minkyu = GameObject.Find("Minkyu");
        DialogBoxTextObject = GameObject.Find("DialogBoxText");
        if (stageCount == 0)
        {
            Stage2Button.GetComponent<SpriteRenderer>().color = new Color(0.3f,0.3f,0.3f,1f);
            Stage2Button.GetComponent<Button>().interactable = false;
            Stage3Button.GetComponent<SpriteRenderer>().color = new Color(0.3f,0.3f,0.3f,1f);
            Stage3Button.GetComponent<Button>().interactable = false;
        }
        else if (stageCount == 1)
        {
            Stage3Button.GetComponent<SpriteRenderer>().color = new Color(0.3f,0.3f,0.3f,1f);
            Stage3Button.GetComponent<Button>().interactable = false;
        }
    }
    public void Stage1Click()
    {
        minkyu.GetComponent<PlayerMovement>().freeze();
        buttonDisable();
        if (!didClearStage1_2) Stage1Go();
        else
        {
            var RealGoBossDirectQuest = Instantiate(GoBossDirectQuest, new Vector3(0f,0f,0f), Quaternion.identity);
            RealGoBossDirectQuest.GetComponent<BossDirectGoYesNoCanvasScript>().whereToGo = 1;
        }
    }

    public void Stage2Click()
    {
        minkyu.GetComponent<PlayerMovement>().freeze();
        buttonDisable();
        if (!didClearStage2_2) Stage2Go();
        else 
        {
            var RealGoBossDirectQuest = Instantiate(GoBossDirectQuest, new Vector3(0f,0f,0f), Quaternion.identity);
            RealGoBossDirectQuest.GetComponent<BossDirectGoYesNoCanvasScript>().whereToGo = 2;
        }
    }

    public void Stage1Go()
    {
        InputDecoder.isGameInScript = true;
        StartCoroutine(Button1Script("Stage 1-0"));
    }

    public void Stage1BossGo()
    {
        InputDecoder.isGameInScript = true;
        PlayerPrefs.SetInt("HP", 500);
        StartCoroutine(Button1Script("Stage 1-Boss"));
    }   

    public void Stage2Go()
    {
        InputDecoder.isGameInScript = true;
        StartCoroutine(Button2Script("Stage 2-0"));
    }

    public void Stage2BossGo()
    {
        InputDecoder.isGameInScript = true;
        PlayerPrefs.SetInt("HP", 500);
        StartCoroutine(Button2Script("Stage 2-Boss"));
    }   



    public void Stage3Click()
    {
        //minkyufreeze
        //buttonDisable();
    }

    public void buttonDisable()
    {
        Stage1Button.GetComponent<Button>().interactable = false;
        Stage2Button.GetComponent<Button>().interactable = false;
        Stage3Button.GetComponent<Button>().interactable = false;
    }


    IEnumerator Button1Script(string destination)
    {
        InputDecoder.InterfaceElements.SetActive(true);
        DialogBoxTextObject.GetComponent<DialogBoxTextTyper>().LoadScript("Text/Lobby/Stage1Go" + Convert.ToString(UnityEngine.Random.Range(1,4)));
        yield return new WaitWhile(() => InputDecoder.isGameInScript);

        SceneManager.LoadScene(destination);
    }

    IEnumerator Button2Script(string destination)
    {
        InputDecoder.InterfaceElements.SetActive(true);
        DialogBoxTextObject.GetComponent<DialogBoxTextTyper>().LoadScript("Text/Lobby/Stage2Go" + Convert.ToString(UnityEngine.Random.Range(1,4)));
        yield return new WaitWhile(() => InputDecoder.isGameInScript);

        SceneManager.LoadScene(destination);
    }
}