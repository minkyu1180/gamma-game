using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MedalScript2 : MonoBehaviour, IDataPersistence
{
    public Sprite medal2Sprite;
    bool itme2Got;
    public void LoadData(GameData data)
    {
        itme2Got = data.didTrueClearStage2;
    }

    public void SaveData(ref GameData data){}

    void Start()
    {
        if (itme2Got)
            this.GetComponentInChildren<SpriteRenderer>().sprite = medal2Sprite;
    }

}
