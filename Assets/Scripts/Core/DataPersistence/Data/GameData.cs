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

    public bool didSeeStage2_0;
    public bool didSeeStage2_1;
    public bool didSeeStage2_2;
    public bool didSeeStage2_2Hidden;
    public bool didSeeStage2_Boss;

    public bool didClearStage2_0;
    public bool didClearStage2_1;
    public bool didClearStage2_2;
    public bool didClearStage2_2Hidden;

    public bool didClearStage2;
    public bool didTrueClearStage2;

    public bool didSeeStage3_0;
    public bool didSeeStage3_1;
    public bool didSeeStage3_2;
    public bool didSeeStage3_2Hidden;
    public bool didSeeStage3_Boss;

    public bool didClearStage3_0;
    public bool didClearStage3_1;
    public bool didClearStage3_2;
    public bool didClearStage3_2Hidden;

    public bool didClearStage3;
    public bool didTrueClearStage3;

    public bool didSeeGlassDoorEvent;
    public bool didCheckEnterGlassDoorAfterEnding;

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



        this.didSeeStage2_0 = false;
        this.didSeeStage2_1 = false;
        this.didSeeStage2_2 = false;
        this.didSeeStage2_2Hidden = false;
        this.didSeeStage2_Boss = false;

        this.didClearStage2_0 = false;
        this.didClearStage2_1 = false;
        this. didClearStage2_2 = false;
        this.didClearStage2_2Hidden = false;

        this.didClearStage2 = false;
        this.didTrueClearStage2 = false;

        this.didSeeStage3_0 = false;
        this.didSeeStage3_1 = false;
        this.didSeeStage3_2 = false;
        this.didSeeStage3_2Hidden = false;
        this.didSeeStage3_Boss = false;

        this.didClearStage3_0 = false;
        this.didClearStage3_1 = false;
        this.didClearStage3_2 = false;
        this.didClearStage3_2Hidden = false;

        this.didClearStage3 = false;
        this.didTrueClearStage3 = false;

        this.didSeeGlassDoorEvent = false;
        this.didCheckEnterGlassDoorAfterEnding = false;
    }
}
