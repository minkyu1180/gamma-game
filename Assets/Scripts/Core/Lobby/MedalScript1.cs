using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MedalScript1 : MonoBehaviour, IDataPersistence
{
    public Sprite medal1Sprite;
    bool itme1Got;
    public void LoadData(GameData data)
    {
        itme1Got = data.didTrueClearStage1;
    }

    public void SaveData(ref GameData data){}

    void Start()
    {
        if (itme1Got)
            this.GetComponentInChildren<SpriteRenderer>().sprite = medal1Sprite;
    }

}
