using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameData
{
    public int stageCount;
    public int[] dayCount = new int[4];
    public bool item1Got;
    public bool item2Got;
    public bool item3Got;
    public bool doubleJumpEnabled;
    public bool upJumpEnabled;
    public bool blinkEnabled;

    public GameData()
    {
        this.stageCount = 0;
        this.dayCount[0] = 0;
        this.dayCount[1] = 0;
        this.dayCount[2] = 0;
        this.dayCount[3] = 0;
        this.item1Got = false;
        this.item2Got = false;
        this.item3Got = false;
        this.doubleJumpEnabled = false;
        this.upJumpEnabled = false;
        this.blinkEnabled = false;
    }
}
