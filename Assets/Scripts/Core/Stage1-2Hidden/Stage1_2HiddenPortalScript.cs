using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Stage1_2HiddenPortalScript : MonoBehaviour, IDataPersistence
{
    GameObject DialogBoxTextObject;
    private bool itemGet;
    public void LoadData(GameData data)
    {
        itemGet = data.item1Got;
    }

    public void SaveData(ref GameData data){}

    void Start()
    {
        DialogBoxTextObject = GameObject.Find("DialogBoxText");
    }
    void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player") && Input.GetAxisRaw("Vertical") == 1)
        {
            if (other.gameObject.GetComponent<Rigidbody2D>().velocity.magnitude <= 0.1f && !InputDecoder.isGameInScript)
            {
                InputDecoder.isGameInScript = true;
                if (!itemGet)
                {
                    StartCoroutine(GoNextStage());
                }
                else
                {
                    StartCoroutine(GoNextStageAfterGetItem());
                }
            }
        }
    }

    IEnumerator GoNextStage()
    {
        InputDecoder.InterfaceElements.SetActive(true);
        DialogBoxTextObject.GetComponent<DialogBoxTextTyper>().LoadScript("Text/Stage1-2Hidden/Clear1-2Hidden");
        yield return new WaitWhile(() => InputDecoder.isGameInScript);

        SceneManager.LoadScene("Stage 1-Boss");
    }

    IEnumerator GoNextStageAfterGetItem()
    {
        InputDecoder.InterfaceElements.SetActive(true);
        DialogBoxTextObject.GetComponent<DialogBoxTextTyper>().LoadScript("Text/Stage1-2Hidden/Clear1-2HiddenAfterItemGet");
        yield return new WaitWhile(() => InputDecoder.isGameInScript);

        SceneManager.LoadScene("Stage 1-Boss");
    }
}
