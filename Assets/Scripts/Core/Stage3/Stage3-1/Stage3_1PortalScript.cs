using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Stage3_1PortalScript : MonoBehaviour, IDataPersistence
{
    GameObject DialogBoxTextObject;
    GameObject dataPersistenceManager;

    bool didTrueClearStage3;
    bool didClearStage3;    
    bool didClearStage3_1;
    bool didClearStage3_2Hidden;

    public void LoadData(GameData data)
    {
        this.didTrueClearStage3 = data.didTrueClearStage3;
        this.didClearStage3 = data.didClearStage3;
        this.didClearStage3_1 = data.didClearStage3_1;
        this.didClearStage3_2Hidden = data.didClearStage3_2Hidden;
    }

    public void SaveData(ref GameData data)
    {
        data.didClearStage3_1 = this.didClearStage3_1;
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
                if (didTrueClearStage3) textLocation = "Text/Stage3-1/Ending/AfterALLEnding";
                else
                {
                    if (didClearStage3)
                    {
                        if (didClearStage3_2Hidden) textLocation = "Text/Stage3-1/Ending/ClearAfterItemReEnding";
                        else                        textLocation = "Text/Stage3-1/Ending/ClearReEnding";
                    }
                    else
                    {
                        if (didClearStage3_1) textLocation = "Text/Stage3-1/Ending/ReEnding";
                        else                  textLocation = "Text/Stage3-1/Ending/Ending";
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
        
        didClearStage3_1 = true;
        bool saved = false;
        saved = dataPersistenceManager.GetComponent<DataPersistenceManager>().SaveGame();
        yield return new WaitWhile(() => !saved);

        SceneManager.LoadScene("Stage 3-2");
    }
}
