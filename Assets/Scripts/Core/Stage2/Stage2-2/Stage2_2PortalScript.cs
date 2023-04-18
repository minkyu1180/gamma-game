using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Stage2_2PortalScript : MonoBehaviour, IDataPersistence
{
    GameObject DialogBoxTextObject;
    GameObject dataPersistenceManager;
    SpiritManagerScript spiritManagerScript;

    bool didTrueClearStage2;
    bool didClearStage2;    
    bool didClearStage2_2;
    bool didClearStage2_2Hidden;

    public void LoadData(GameData data)
    {
        this.didTrueClearStage2 = data.didTrueClearStage2;
        this.didClearStage2 = data.didClearStage2;
        this.didClearStage2_2 = data.didClearStage2_2;
        this.didClearStage2_2Hidden = data.didClearStage2_2Hidden;
    }

    public void SaveData(ref GameData data)
    {
        data.didClearStage2_2 = this.didClearStage2_2;
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
        spiritManagerScript = GameObject.Find("SpiritUI").GetComponent<SpiritManagerScript>();
    }
    void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player") && Input.GetAxisRaw("Vertical") == 1)
        {
            if (other.gameObject.GetComponent<Rigidbody2D>().velocity.magnitude <= 0.1f && !InputDecoder.isGameInScript)
            {
                
                InputDecoder.isGameInScript = true;
                string textLocation;
                if (didTrueClearStage2) textLocation = "Text/Stage2-2/Ending/AfterALLEnding";
                else
                {
                    if (didClearStage2)
                    {
                        if (didClearStage2_2Hidden) textLocation = "Text/Stage2-2/Ending/ClearAfterItemReEnding";
                        else                        textLocation = "Text/Stage2-2/Ending/ClearReEnding";
                    }
                    else
                    {
                        if (didClearStage2_2) textLocation = "Text/Stage2-2/Ending/ReEnding";
                        else                  textLocation = "Text/Stage2-2/Ending/Ending";
                    }
                }
                if (spiritManagerScript.checkHasAllSpirits())   StartCoroutine(GoNextStage(textLocation));
                else StartCoroutine(NotEnoughSpirits("Text/Stage2-2/Ending/Nope"));
            }
        }
    }

    IEnumerator GoNextStage(string textLocation)
    {

        InputDecoder.InterfaceElements.SetActive(true);
        DialogBoxTextObject.GetComponent<DialogBoxTextTyper>().LoadScript(textLocation);
        yield return new WaitWhile(() => InputDecoder.isGameInScript);
        
        didClearStage2_2 = true;
        bool saved = false;
        saved = dataPersistenceManager.GetComponent<DataPersistenceManager>().SaveGame();
        yield return new WaitWhile(() => !saved);

        SceneManager.LoadScene("Stage 2-Boss");
    }

    IEnumerator NotEnoughSpirits(string textLocation)
    {
        InputDecoder.InterfaceElements.SetActive(true);
        DialogBoxTextObject.GetComponent<DialogBoxTextTyper>().LoadScript(textLocation);
        yield return new WaitWhile(() => InputDecoder.isGameInScript);
    }
}
