using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisableContinueButtonScript : MonoBehaviour
{
    public GameObject dataPersistenceManager;
    void Start()
    {
        dataPersistenceManager = GameObject.Find("DataPersistenceManager");
        if (!dataPersistenceManager.GetComponent<DataPersistenceManager>().CheckGameDataExist())
        {
            gameObject.GetComponent<Button>().interactable = false;
        }
    }
}
