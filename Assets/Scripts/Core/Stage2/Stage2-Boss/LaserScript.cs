using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserScript : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject minkyu;
    public GameObject boss;

    public Vector2 vec1 = new Vector2(1,0);
    public Vector2 vec2 = new Vector2(0,1);

    public float len = 50f;

    public float chaseDuration = 3.0f;
    public float heatingDuration = 0.5f;

    public bool chaseEnd = false;
    public Color cautionColor = Color.red;

    private Vector2 minkyuPos;
    private Vector2 bossPos;
    private Vector2 start;
    private Vector2 end;

    void Start()
    {
        minkyu = GameObject.Find("Minkyu");
        boss = GameObject.Find("Boss");
        StartCoroutine(ActiveAllChildAfterWhile(chaseDuration, heatingDuration));
    }




    IEnumerator ActiveAllChildAfterWhile(float chaseDuration, float heatingDuration)
    {
        yield return new WaitForSeconds(chaseDuration);
        chaseEnd = true;

        transform.GetChild(4).position = minkyu.transform.position;
        transform.GetChild(5).position = boss.transform.position;
        transform.GetChild(4).GetComponent<SunShrinkScript>().wait = false;
        transform.GetChild(5).GetComponent<SunShrinkScript>().wait = false;
        

        List <Vector2> setpoint = new List<Vector2>();
        start = minkyuPos + vec1 * len;
        end = minkyuPos - vec1 * len;
        setpoint.Add(start);
        setpoint.Add(end);
        transform.GetChild(0).GetComponent<EdgeCollider2D>().SetPoints(setpoint);

        setpoint.Clear();
        start = minkyuPos + vec2 * len;
        end = minkyuPos - vec2 * len;
        setpoint.Add(start);
        setpoint.Add(end);
        transform.GetChild(1).GetComponent<EdgeCollider2D>().SetPoints(setpoint);
        
        setpoint.Clear();
        start = bossPos + vec1 * len;
        end = bossPos - vec1 * len;
        setpoint.Add(start);
        setpoint.Add(end);
        transform.GetChild(2).GetComponent<EdgeCollider2D>().SetPoints(setpoint);

        setpoint.Clear();
        start = bossPos + vec2 * len;
        end = bossPos - vec2 * len;
        setpoint.Add(start);
        setpoint.Add(end);
        transform.GetChild(3).GetComponent<EdgeCollider2D>().SetPoints(setpoint);
        

        yield return new WaitForSeconds(heatingDuration);
        
        transform.GetChild(0).GetComponent<LineHitScript>().active = true;
        transform.GetChild(0).GetComponent<LineRenderer>().startColor = cautionColor;
        transform.GetChild(0).GetComponent<LineRenderer>().endColor = cautionColor;
        
        transform.GetChild(1).GetComponent<LineHitScript>().active = true;
        transform.GetChild(1).GetComponent<LineRenderer>().startColor = cautionColor;
        transform.GetChild(1).GetComponent<LineRenderer>().endColor = cautionColor;
        
        transform.GetChild(2).GetComponent<LineHitScript>().active = true;
        transform.GetChild(2).GetComponent<LineRenderer>().startColor = cautionColor;
        transform.GetChild(2).GetComponent<LineRenderer>().endColor = cautionColor;

        transform.GetChild(3).GetComponent<LineHitScript>().active = true;
        transform.GetChild(3).GetComponent<LineRenderer>().startColor = cautionColor;
        transform.GetChild(3).GetComponent<LineRenderer>().endColor = cautionColor;
        
    }
    
    
    void Update()
    {
        if (!chaseEnd)
        {
            minkyuPos = minkyu.transform.position;
            bossPos = boss.transform.position;
            transform.GetChild(0).GetComponent<LineRenderer>().SetPosition(0, minkyuPos + vec1 * len);
            transform.GetChild(0).GetComponent<LineRenderer>().SetPosition(1, minkyuPos - vec1 * len);

            transform.GetChild(1).GetComponent<LineRenderer>().SetPosition(0,minkyuPos + vec2 * len);
            transform.GetChild(1).GetComponent<LineRenderer>().SetPosition(1,minkyuPos - vec2 * len);

            transform.GetChild(2).GetComponent<LineRenderer>().SetPosition(0,bossPos + vec1 * len);
            transform.GetChild(2).GetComponent<LineRenderer>().SetPosition(1,bossPos - vec1 * len);

            transform.GetChild(3).GetComponent<LineRenderer>().SetPosition(0,bossPos + vec2 * len);
            transform.GetChild(3).GetComponent<LineRenderer>().SetPosition(1,bossPos - vec2 * len);
        } 

        if (transform.childCount == 0) Destroy(gameObject);
    }
}
