using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class DayTextMaker : MonoBehaviour, IDataPersistence
{
    int dayCount;

    public void LoadData(GameData data)
    {
        this.dayCount = data.dayCount;
    }

    public void SaveData(ref GameData data){}

    void Start()
    {
        this.GetComponent<TextMeshPro>().text = "Day" + Convert.ToString(dayCount + 1);
    }
}
