using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveAtPositionScript : MonoBehaviour, IDataPersistence
{
    GameObject startPosition;
    GameObject DialogBoxTextObject;

    bool didClearStage2;    
    
    public void LoadData(GameData data)
    {
        this.didClearStage2 = data.didClearStage2;
    }

    public void SaveData(ref GameData data)
    {
    }


    bool onceActivated = false;
    void Start()
    {
        DialogBoxTextObject = GameObject.Find("DialogBoxText");
        startPosition = GameObject.Find("Start");
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player" && !onceActivated)
        {
            if (!didClearStage2) StartCoroutine(ExpalinNotToGo("Text/Stage2-1/Explain"));
            startPosition.transform.position = transform.position;
            onceActivated = true;
        }        
    }

    IEnumerator ExpalinNotToGo(string textLocation)
    {
        InputDecoder.isGameInScript = true;
        InputDecoder.InterfaceElements.SetActive(true);
        DialogBoxTextObject.GetComponent<DialogBoxTextTyper>().LoadScript(textLocation);
        yield return new WaitWhile(() => InputDecoder.isGameInScript);
    }
}
