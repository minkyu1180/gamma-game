using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ExExTextMaker : MonoBehaviour, IDataPersistence
{
    int stageCount;

    public void LoadData(GameData data)
    {
        this.stageCount = data.stageCount;
    }

    public void SaveData(ref GameData data){}

    void Start()
    {
        if (stageCount == 0)
            this.GetComponent<TextMeshPro>().text = "";
        else if (stageCount == 1)
            this.GetComponent<TextMeshPro>().text = "더블점프: space * 2";
        else if (stageCount == 2)
            this.GetComponent<TextMeshPro>().text = "더블점프: space * 2\n윗점프: 윗키 + space";
        else
            this.GetComponent<TextMeshPro>().text = "더블점프: space * 2\n윗점프: 윗키 + space\n체공: 아래 키\n"; 
    }
}
