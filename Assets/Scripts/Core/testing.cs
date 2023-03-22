using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testing : MonoBehaviour, IDataPersistence
{
    private int dayCount = 0;
    public void buttonClick()
    {
        dayCount++;
        Debug.Log("DayCount: " + dayCount);
    }   

    public void LoadData(GameData data)
    {
        this.dayCount = data.dayCount[0];
    }

    public void SaveData(ref GameData data)
    {
        data.dayCount[0] = this.dayCount;
    }
}
