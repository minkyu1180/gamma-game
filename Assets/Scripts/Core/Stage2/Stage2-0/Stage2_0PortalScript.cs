using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Stage2_0PortalScript : MonoBehaviour, IDataPersistence
{
    GameObject DialogBoxTextObject;
    GameObject dataPersistenceManager;

    bool didTrueClearStage2;
    bool didClearStage2;    
    bool didClearStage2_0;
    bool didClearStage2_2Hidden;

    public void LoadData(GameData data)
    {
        this.didTrueClearStage2 = data.didTrueClearStage2;
        this.didClearStage2 = data.didClearStage2;
        this.didClearStage2_0 = data.didClearStage2_0;
        this.didClearStage2_2Hidden = data.didClearStage2_2Hidden;
    }

    public void SaveData(ref GameData data)
    {
        data.didClearStage2_0 = this.didClearStage2_0;
    }

    /*
    public void Update()
    {  
        //CHEAT. EXSISTS FOR GAME TESTING. MUST BE ELIMINATED IN FINAL RELEASE
        if (Input.GetKeyDown(KeyCode.F))
        {
            GameObject minkyu = GameObject.Find("Minkyu");
            minkyu.transform.position = gameObject.transform.position;
        }
    }
    */

    void Start()
    {
        DialogBoxTextObject = GameObject.Find("DialogBoxText");
        dataPersistenceManager = GameObject.Find("DataPersistenceManager");
    }
    void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player") && Input.GetAxisRaw("Vertical") == 1)
        {
            if (other.gameObject.GetComponent<Rigidbody2D>().velocity.magnitude <= 0.1f && !InputDecoder.isGameInScript)
            {
                
                InputDecoder.isGameInScript = true;
                string textLocation;
                if (didTrueClearStage2) textLocation = "Text/Stage2-0/Ending/AfterALLEnding";
                else
                {
                    if (didClearStage2)
                    {
                        if (didClearStage2_2Hidden) textLocation = "Text/Stage2-0/Ending/ClearAfterItemReEnding";
                        else                        textLocation = "Text/Stage2-0/Ending/ClearReEnding";
                    }
                    else
                    {
                        if (didClearStage2_0) textLocation = "Text/Stage2-0/Ending/ReEnding";
                        else                  textLocation = "Text/Stage2-0/Ending/Ending";
                    }
                }
                StartCoroutine(GoNextStage(textLocation));
            }
        }
    }

    IEnumerator GoNextStage(string textLocation)
    {

        InputDecoder.InterfaceElements.SetActive(true);
        DialogBoxTextObject.GetComponent<DialogBoxTextTyper>().LoadScript(textLocation);
        yield return new WaitWhile(() => InputDecoder.isGameInScript);
        
        didClearStage2_0 = true;
        bool saved = false;
        saved = dataPersistenceManager.GetComponent<DataPersistenceManager>().SaveGame();
        yield return new WaitWhile(() => !saved);

        SceneManager.LoadScene("Stage 2-1");
    }
}
