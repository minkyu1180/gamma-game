using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;


public class StageTextMaker : MonoBehaviour, IDataPersistence
{
    int stageCount;

    public void LoadData(GameData data)
    {
        this.stageCount = data.stageCount;
    }

    public void SaveData(ref GameData data){}

    void Start()
    {
        if (stageCount != 3) this.GetComponent<TextMeshPro>().text = "Stage" + Convert.ToString(stageCount + 1);
        else this.GetComponent<TextMeshPro>().text = "AllClear";
    }
}
