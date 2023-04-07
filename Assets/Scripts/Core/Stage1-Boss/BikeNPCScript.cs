using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BikeNPCScript : MonoBehaviour, IDataPersistence
{
    bool didClearStage1;
    public Sprite burningBike;

    public void LoadData(GameData data)
    {
        this.didClearStage1 = data.didClearStage1;
    }

    public void SaveData(ref GameData data){}

    void Start()
    {
        if (!didClearStage1) gameObject.GetComponent<SpriteRenderer>().sprite = burningBike;
    }

    /*
    public void Update()
    {  
        //CHEAT. EXSISTS FOR GAME TESTING. MUST BE ELIMINATED IN FINAL RELEASE
        if (Input.GetKeyDown(KeyCode.F))
        {
            GameObject minkyu = GameObject.Find("Minkyu");
            minkyu.transform.position = gameObject.transform.position;
        }
    }
    */
}
