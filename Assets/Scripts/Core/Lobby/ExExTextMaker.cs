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
            this.GetComponent<TextMeshPro>().text = "더블점프: space * 2\n활강: 추락 시 윗 키";
        else
            this.GetComponent<TextMeshPro>().text = "더블점프: 더블점프: space * 2\n활강: 추락 시 윗 키\n윗점프: 윗키 + space\n"; 
    }
}
