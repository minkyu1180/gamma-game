using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PinkNPCSpiritScript : MonoBehaviour, IDataPersistence
{
    public GameObject spiritUI;
    GameObject DialogBoxTextObject;
    bool once = false;

    bool didClearStage2_2;

    public void LoadData(GameData data)
    {
        this.didClearStage2_2 = data.didClearStage2_2;
    }

    public void SaveData(ref GameData data){}

    void Start()
    {
        spiritUI = GameObject.Find("SpiritUI");
        DialogBoxTextObject = GameObject.Find("DialogBoxText");
        if (didClearStage2_2)
        {
            spiritUI.GetComponent<SpiritManagerScript>().getPink();
            Destroy(gameObject);
        }

    }

    void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("PlayerHitbox") && !once)
        {
            once = true;
            StartCoroutine(TalkNBanish("Text/Stage2-2/Spirit/Pink"));
        }        
    }
    

    IEnumerator TalkNBanish(string textLocation)
    {
        InputDecoder.isGameInScript = true;
        InputDecoder.InterfaceElements.SetActive(true);
        DialogBoxTextObject.GetComponent<DialogBoxTextTyper>().LoadScript(textLocation);
        yield return new WaitWhile(() => InputDecoder.isGameInScript);
        
        spiritUI.GetComponent<SpiritManagerScript>().getPink();
        Destroy(gameObject);
    }
}
