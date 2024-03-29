using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Stage1_1PortalScript : MonoBehaviour, IDataPersistence
{
    GameObject DialogBoxTextObject;
    GameObject dataPersistenceManager;

    bool didTrueClearStage1;
    bool didClearStage1;    
    bool didClearStage1_1;
    bool didClearStage1_2Hidden;

    public void LoadData(GameData data)
    {
        this.didTrueClearStage1 = data.didTrueClearStage1;
        this.didClearStage1 = data.didClearStage1;
        this.didClearStage1_1 = data.didClearStage1_1;
        this.didClearStage1_2Hidden = data.didClearStage1_2Hidden;
    }

    public void SaveData(ref GameData data)
    {
        data.didClearStage1_1 = this.didClearStage1_1;
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
                if (didTrueClearStage1) textLocation = "Text/Stage1-1/Ending/AfterALLEnding";
                else
                {
                    if (didClearStage1)
                    {
                        if (didClearStage1_2Hidden) textLocation = "Text/Stage1-1/Ending/ClearAfterItemReEnding";
                        else                        textLocation = "Text/Stage1-1/Ending/ClearReEnding";
                    }
                    else
                    {
                        if (didClearStage1_1) textLocation = "Text/Stage1-1/Ending/ReEnding";
                        else                  textLocation = "Text/Stage1-1/Ending/Ending";
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

        didClearStage1_1 = true;
        bool saved = false;
        saved = dataPersistenceManager.GetComponent<DataPersistenceManager>().SaveGame();
        yield return new WaitWhile(() => !saved);

        SceneManager.LoadScene("Stage 1-2");
    }
}
