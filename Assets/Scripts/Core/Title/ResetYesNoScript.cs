using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetYesNoScript : MonoBehaviour
{
    Transform resetUI;
    public GameObject StartButton;
    public void Start()
    {
        StartButton = GameObject.Find("Start");
    }

    public void OnClickYes()
    {
        StartButton.GetComponent<AudioSource>().Play();
        StartCoroutine(StartButton.GetComponent<TitleButtonScript>().DelayOpeningSceneLoad());
    }

    public void OnClickNo()
    {
        resetUI = gameObject.transform.parent.transform.parent;
        StartButton.GetComponent<TitleButtonScript>().IsresetUIPopped = false;
        Destroy(resetUI.gameObject);
    }
}
