using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AfterClearStage1Deco : MonoBehaviour, IDataPersistence
{
    private bool didClearStage1;    


    public void LoadData(GameData data)
    {
        this.didClearStage1 = data.didClearStage1;
    }

    public void SaveData(ref GameData data){}

    void Start()
    {
        if (!didClearStage1) gameObject.SetActive(false);
    }

}
