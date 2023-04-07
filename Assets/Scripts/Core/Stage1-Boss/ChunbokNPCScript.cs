using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChunbokNPCScript : MonoBehaviour,IDataPersistence
{
    bool didClearStage1;
    public Sprite damagedChunbok;

    public void LoadData(GameData data)
    {
        this.didClearStage1 = data.didClearStage1;
    }

    public void SaveData(ref GameData data){}

    void Start()
    {
        if (!didClearStage1)
        {
            gameObject.GetComponent<SpriteRenderer>().sprite = damagedChunbok;
            gameObject.transform.position = new Vector3(363.47f, -2.46f, 0f);
        } 
    }
}
