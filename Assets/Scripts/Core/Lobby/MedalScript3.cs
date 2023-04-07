using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MedalScript3 : MonoBehaviour, IDataPersistence
{
    public Sprite medal3Sprite;
    bool itme3Got;
    public void LoadData(GameData data)
    {
        itme3Got = data.didTrueClearStage3;
    }

    public void SaveData(ref GameData data){}

    void Start()
    {
        if (itme3Got)
            this.GetComponentInChildren<SpriteRenderer>().sprite = medal3Sprite;
    }

}
