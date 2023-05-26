using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AfterClearStage3Deco : MonoBehaviour, IDataPersistence
{
    private bool didClearStage3;    


    public void LoadData(GameData data)
    {
        this.didClearStage3 = data.didClearStage3;
    }

    public void SaveData(ref GameData data){}

    void Start()
    {
        if (!didClearStage3) gameObject.SetActive(false);
    }

}
