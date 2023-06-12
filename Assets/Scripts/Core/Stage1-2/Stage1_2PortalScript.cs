using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Stage1_2PortalScript : MonoBehaviour, IDataPersistence
{
    GameObject DialogBoxTextObject;
    GameObject dataPersistenceManager;

    bool didClearStage1_2;

    public void LoadData(GameData data)
    {
        this.didClearStage1_2 = data.didClearStage1_2;
    }

    public void SaveData(ref GameData data)
    {
        data.didClearStage1_2 = this.didClearStage1_2;
    }

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
                StartCoroutine(GoNextStage());
            }
        }
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

    IEnumerator GoNextStage()
    {
        InputDecoder.InterfaceElements.SetActive(true);
        DialogBoxTextObject.GetComponent<DialogBoxTextTyper>().LoadScript("Text/Stage1-2/Ending/Ending");
        yield return new WaitWhile(() => InputDecoder.isGameInScript);

        didClearStage1_2 = true;
        bool saved = false;
        saved = dataPersistenceManager.GetComponent<DataPersistenceManager>().SaveGame();
        yield return new WaitWhile(() => !saved);

        SceneManager.LoadScene("Stage 1-Boss");
    }
}
