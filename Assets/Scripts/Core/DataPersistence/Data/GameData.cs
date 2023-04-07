using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameData
{
    public int stageCount;
    public int dayCount;
    
 
    public bool didSeeStage1_0;
    public bool didSeeStage1_1;
    public bool didSeeStage1_2;
    public bool didSeeStage1_2Hidden;
    public bool didSeeStage1_Boss;

    public bool didClearStage1_0;
    public bool didClearStage1_1;
    public bool didClearStage1_2;
    public bool didClearStage1_2Hidden; //same

    public bool didClearStage1;
    public bool didTrueClearStage1;

    public bool didClearStage2;
    public bool didTrueClearStage2;

    public bool didClearStage3;
    public bool didTrueClearStage3;

    public GameData()
    {
        this.stageCount = 0;
        this.dayCount = 0;
        
        
        this.didSeeStage1_0 = false;
        this.didSeeStage1_1 = false;
        this.didSeeStage1_2 = false;
        this.didSeeStage1_2Hidden = false;

        this.didClearStage1_0 = false;
        this.didClearStage1_1 = false;
        this.didClearStage1_2 = false;
        this.didClearStage1_2Hidden = false;

        this.didClearStage1 = false;
        this.didTrueClearStage1 = false;

        this.didClearStage2 = false;
        this.didTrueClearStage2 = false;

        this.didClearStage3 = false;
        this.didTrueClearStage3 = false;
    }
}
