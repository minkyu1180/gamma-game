using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Stage1_1HiddenPortalScript : MonoBehaviour
{
    GameObject DialogBoxTextObject;
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
                StartCoroutine(GoNextStage());
            }
        }
    }

    IEnumerator GoNextStage()
    {
        InputDecoder.InterfaceElements.SetActive(true);
        DialogBoxTextObject.GetComponent<DialogBoxTextTyper>().LoadScript("Text/Stage1-1/HiddenClear1-1");
        yield return new WaitWhile(() => InputDecoder.isGameInScript);

        SceneManager.LoadScene("Stage 1-2Hidden");
    }
}
