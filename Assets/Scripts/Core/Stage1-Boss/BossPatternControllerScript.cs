using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossPatternControllerScript : MonoBehaviour
{
    //public GameObject minkyu;
    private Grid grid;
    int noAttackFloor;
    bool IsBodyCrushPatternOFF = false;
    bool IsRockDownPatternOFF = false;
    bool IsAllMixPatternOFF = false;
    bool IsFasterActivated = false;
    public GameObject UpBikeChunbok;
    public GameObject DownBikeChunbok;
    private float bikeVelocityMin;
    private float bikeVelocityMax;
    private float dustTermMin;
    private float dustTermMax;
    private float BodyCrushTerm;
    private float RockDownTerm;
    

    // Start is called before the first frame update
    void Start()
    {
        grid = transform.parent.GetComponentInParent<Grid>();
        bikeVelocityMin = 5f;
        bikeVelocityMax = 10f;
        dustTermMin = 0.1f;
        dustTermMax = 0.2f;
        BodyCrushTerm = 5.0f;
        RockDownTerm = 4.0f;
    }

    public void FasterPattern()
    {
        IsFasterActivated = true;
    }



    public void StartAllMixPattern()
    {
        IsAllMixPatternOFF = false;
        StartCoroutine(AllMixPatternIterate());
    }
    public void StopAllMixPattern()
    {
        IsAllMixPatternOFF = true;
    }

    IEnumerator AllMixPatternIterate()
    {
        while (!IsAllMixPatternOFF)
        {
            if (IsFasterActivated)
            {
                bikeVelocityMin = 10f;
                bikeVelocityMax = 15f;
                dustTermMin = 0.07f;
                dustTermMax = 0.1f;
                BodyCrushTerm = 3.0f;
            }
            int patternChosen = Random.Range(0,3);
            if (patternChosen <= 1) // BodyCrush
            {
                BodyCrushWave();
                yield return new WaitForSeconds(BodyCrushTerm);
            }
            else
            {
                RockDownWave();
                yield return new WaitForSeconds(RockDownTerm);
            }
        }
    }
    
    public void StartBodyCrushPattern()
    {
        IsBodyCrushPatternOFF = false;
        StartCoroutine(BodyCrushPatternIterate());
    }
    public void StopBodyCrushPattern()
    {
        IsBodyCrushPatternOFF = true;
    }

    IEnumerator BodyCrushPatternIterate()
    {
        while (!IsBodyCrushPatternOFF)
        {
            BodyCrushWave();
            yield return new WaitForSeconds(BodyCrushTerm);
        }
    }

    public void StartRockDownPattern()
    {
        IsRockDownPatternOFF = false;
        StartCoroutine(RockDownPatternIterate());
    }

    public void StopRockDownPattern()
    {
        IsRockDownPatternOFF = true;
    }


    IEnumerator RockDownPatternIterate()
    {
        while (!IsRockDownPatternOFF)
        {
            RockDownWave();
            yield return new WaitForSeconds(RockDownTerm);
        }
    }

    void RockDownWave()
    {
        UpBikeChunbok.GetComponent<RockDownPatternScript>().RockDown(dustTermMin, dustTermMax);
    }

    void BodyCrushWave()
    {
        //grid.WorldToCell(minkyu.transform.position);
        //select floor that player exists.. but I don't think just random is fun.
        noAttackFloor = Random.Range(1,4);
        for (int i = 1; i < 4; i++)
        {
            
            if (i == noAttackFloor) continue;
            float velocity = Random.Range(bikeVelocityMin, bikeVelocityMax);
            var DownBikeChunbokInstance = Instantiate(DownBikeChunbok, new Vector3(0f,0f,0f), Quaternion.identity);
            DownBikeChunbokInstance.transform.parent = gameObject.transform;
            DownBikeChunbokInstance.GetComponent<BodyCrushPatternScript>().BodyCrush(velocity, i);

            
        }
    }


    int GetNeareast(int target, int a, int b, int c)
    {    
        int diffA = Mathf.Abs(target - a);
        int diffB = Mathf.Abs(target - b);
        int diffC = Mathf.Abs(target - c);
        if (Mathf.Min(diffA, diffB, diffC) == diffA) return a;
        else if (Mathf.Min(diffA, diffB, diffC) == diffB) return b;
        else return c;   
    }


}
