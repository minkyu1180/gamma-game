using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossDirectGoYesNoCanvasScript : MonoBehaviour
{
    StageUIScript stageUIScript;
    public int whereToGo = 0;
    bool callOnce = false;
    // Start is called before the first frame update
    void Start()
    {
        stageUIScript = GameObject.Find("MapChooseUI").GetComponent<StageUIScript>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!callOnce)
        {
            if (Input.GetKeyDown(KeyCode.Y))
            {
                yesClick();
            }
            if (Input.GetKeyDown(KeyCode.N))
            {
                noClick();
            }
        }
    }

    public void yesClick()
    {
        callOnce = true;
        if (whereToGo == 1) stageUIScript.Stage1BossGo();
        else if (whereToGo == 2) stageUIScript.Stage2BossGo();
        Destroy(gameObject);
    }

    public void noClick()
    {
        callOnce = true;
        if (whereToGo == 1)stageUIScript.Stage1Go();
        else if (whereToGo == 2) stageUIScript.Stage2Go();
        Destroy(gameObject);
    }
}
