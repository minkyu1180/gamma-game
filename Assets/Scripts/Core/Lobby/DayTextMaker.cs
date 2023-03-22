using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class DayTextMaker : MonoBehaviour, IDataPersistence
{
    int[] dayCount = new int[4];
    int stageCount;

    public void LoadData(GameData data)
    {
        this.stageCount = data.stageCount;
        this.dayCount[0] = data.dayCount[0];
        this.dayCount[1] = data.dayCount[1];
        this.dayCount[2] = data.dayCount[2];
        this.dayCount[3] = data.dayCount[3];
    }

    public void SaveData(ref GameData data){}

    void Start()
    {
        this.GetComponent<TextMeshPro>().text = "Day" + Convert.ToString(dayCount[stageCount] + 1);
    }
}
