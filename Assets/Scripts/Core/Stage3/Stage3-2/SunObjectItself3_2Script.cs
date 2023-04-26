using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SunObjectItself3_2Script : MonoBehaviour
{

    public GameObject Camera;
    public float goSpeed;
    public float accelerateRate;
    public Vector3 directionVector;

    public bool endPointAttached = false;
    public bool startPointAttached = false;

    private bool IsPlayerHit = false;
    private bool OnCycle = false;
    private Vector3 ongoingVector;

    public GameObject StartPoint;

    void Start()
    {
        ongoingVector = accelerateRate * directionVector;
        StartPoint = GameObject.Find("Start");
    }

    void Update()
    {
        if (startPointAttached && endPointAttached) OnCycle = false;
        if ( gameObject.GetComponent<Rigidbody2D>().velocity.x > 0) gameObject.GetComponent<SpriteRenderer>().flipX = true;
        else gameObject.GetComponent<SpriteRenderer>().flipX = false;
        if (!OnCycle){
            startPointAttached = false;
            endPointAttached = false;
            OnCycle = true;
        }    
    }

    

    void FixedUpdate()
    {
        if (gameObject.GetComponent<Rigidbody2D>().velocity.magnitude <= goSpeed)
        {
            gameObject.GetComponent<Rigidbody2D>().AddForce(ongoingVector);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (!endPointAttached)
        {
            if (other.gameObject.tag == "SunEnd")
            {
                if (transform.IsChildOf(other.transform.parent))
                {
                    endPointAttached = true;
                    ongoingVector = -accelerateRate * directionVector;
                    gameObject.GetComponent<Rigidbody2D>().AddForce(ongoingVector);
                }
            }
        }
        if (!startPointAttached)
        {
            if (other.gameObject.tag == "SunStart")
            {
                if (transform.IsChildOf(other.transform.parent))
                {
                    startPointAttached = true;
                    ongoingVector = accelerateRate * directionVector;
                    gameObject.GetComponent<Rigidbody2D>().AddForce(ongoingVector);
                }
            }
        }

        if (other.gameObject.CompareTag("PlayerHitbox") && !IsPlayerHit)
        {
            IsPlayerHit = true;
            StartCoroutine(WaitWithPainAndRevive(other));
        }
    }

    IEnumerator WaitWithPainAndRevive(Collider2D other)
    {
        Time.timeScale = 0.05f;
        other.gameObject.GetComponent<HealthScript>().Hit(10);
        yield return new WaitForSecondsRealtime(0.35f);
        Time.timeScale = 1.0f;
        other.gameObject.transform.parent.GetComponent<Rigidbody2D>().velocity = new Vector2(0f,0f);
        other.gameObject.transform.parent.GetComponent<Rigidbody2D>().position = StartPoint.GetComponent<Rigidbody2D>().position;
        
        //yield return new WaitForSecondsRealtime(0.05f);
        IsPlayerHit = false;        
        Camera.GetComponent<AutoScroll3_2Camera>().CameraInit = true;
    }
}
