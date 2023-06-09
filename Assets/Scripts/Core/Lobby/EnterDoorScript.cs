using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class EnterDoorScript : MonoBehaviour, IDataPersistence
{
    GameObject DialogBoxTextObject;
    GameObject dataPersistenceManager;

    bool didSeeGlassDoorEvent;
    bool didClearStage3;
    bool didTrueClearStage1;
    bool didTrueClearStage2;
    bool didTrueClearStage3;
    bool didCheckEnterGlassDoorAfterEnding;

    bool enterSideStory = false;


    public void LoadData(GameData data)
    {
        this.didSeeGlassDoorEvent = data.didSeeGlassDoorEvent;
        this.didClearStage3 = data.didClearStage3;
        this.didTrueClearStage1 = data.didTrueClearStage1;
        this.didTrueClearStage2 = data.didTrueClearStage2;
        this.didTrueClearStage3 = data.didTrueClearStage3;
        this.didCheckEnterGlassDoorAfterEnding = data.didCheckEnterGlassDoorAfterEnding;
    }

    public void SaveData(ref GameData data)
    {
        data.didCheckEnterGlassDoorAfterEnding = this.didCheckEnterGlassDoorAfterEnding;
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
                string textLocation;
                if (didSeeGlassDoorEvent) textLocation = "Text/Lobby/GlassDoor/AlreadyTalked";
                else
                {
                    if (!didClearStage3) textLocation = "Text/Lobby/GlassDoor/NoNeedToEnter";
                    else
                    {
                        if (didTrueClearStage1 && didTrueClearStage2 && didTrueClearStage3)
                        {
                            if (didCheckEnterGlassDoorAfterEnding)
                            {
                                enterSideStory = true;
                                textLocation = "Text/Lobby/GlassDoor/OkayLetEnter";
                            }
                            else
                            {
                                enterSideStory = true;
                                textLocation = "Text/Lobby/GlassDoor/GetSomeTalkOkayLetEnter";
                            }
                        }
                        else
                        {
                            if (didCheckEnterGlassDoorAfterEnding)
                            {
                                textLocation = "Text/Lobby/GlassDoor/NoAdmit";
                            }
                            else
                            {
                                textLocation = "Text/Lobby/GlassDoor/GetSomeTalkNoAdmit";
                            }
                        }
                    }
                }
                StartCoroutine(ScriptLoad(textLocation));
            }
        }
    }

    IEnumerator ScriptLoad(string textLocation)
    {

        InputDecoder.InterfaceElements.SetActive(true);
        DialogBoxTextObject.GetComponent<DialogBoxTextTyper>().LoadScript(textLocation);
        yield return new WaitWhile(() => InputDecoder.isGameInScript);
        
        bool saved = false;
        if (didClearStage3) didCheckEnterGlassDoorAfterEnding = true;
        saved = dataPersistenceManager.GetComponent<DataPersistenceManager>().SaveGame();
        yield return new WaitWhile(() => !saved);

        if (enterSideStory) SceneManager.LoadScene("SideStoryScene");
        //Go SideStory Scene
    }
}
