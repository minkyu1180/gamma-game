using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SunMovementScript : MonoBehaviour
{
    private GameObject Sun;
    private GameObject SunStartPoint;
    private GameObject SunEndPoint;

    public Vector3 directionVector;
    
    public float goSpeed;

    public Sprite SunDefaultSprite;
    public Sprite SunAggrovateSprite;

    public bool endPointAttached = false;
    public bool startPointAttached = false;

    private bool OnCycle = false;

    void Start()
    {
        Sun = transform.GetChild(2).gameObject;
        SunEndPoint = transform.GetChild(1).gameObject;
        SunStartPoint = transform.GetChild(0).gameObject;
        

        Sun.transform.position = SunStartPoint.transform.position;
    }

    void Update()
    {
        if (startPointAttached && endPointAttached) OnCycle = false;

        if (!OnCycle){
            startPointAttached = false;
            endPointAttached = false;
            StartCoroutine(Rush());
            OnCycle = true;
        }
    }

    IEnumerator Rush()
    {
        Sun.GetComponent<Rigidbody2D>().velocity = Vector3.zero;
        Sun.GetComponent<Rigidbody2D>().AddForce(directionVector);
        yield return new WaitWhile(() => !endPointAttached);

        Sun.GetComponent<Rigidbody2D>().velocity = Vector3.zero;
        Sun.GetComponent<Rigidbody2D>().AddForce(-directionVector);
        
       
        yield return new WaitWhile(() => !startPointAttached);

    }
}
