using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BodyCrushPatternScript : MonoBehaviour
{
    private GameObject minkyu;
    private Vector3Int targetCellPos;
    //float yCorrection = 0.355f;
    public float floor3YHeight = 0f;
    public float floor2YHeight = -1f;
    public float floor1YHeight = -2f;
    private bool damageDealt = false;

    public float bikeRunTime = 5f;
    public float chaseTime = 1.5f;

    public int BikeDamage = 100;

    public AudioSource audioSource;
    public AudioClip bikeSound;
    void Awake()
    {
        minkyu = GameObject.Find("Minkyu");
        audioSource = gameObject.GetComponent<AudioSource>();
        bikeSound = Resources.Load("Sound/Voice/bikeSound") as AudioClip;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("PlayerHitbox"))
        {
            if (!damageDealt)
            {
                other.gameObject.GetComponent<HealthScript>().Hit(BikeDamage);
                damageDealt = true;
            }
        }
    }


    public void BodyCrush(float bikeVelocity, int whatRowToGo)
    {
        StartCoroutine(BodyCrushAct(chaseTime, bikeRunTime, bikeVelocity, whatRowToGo));
    }

    IEnumerator BodyCrushAct(float chaseTime, float bikeRunTime, float bikeVelocity, int whatRowToGo) // -1, -2, -3 Row
    {
        transform.GetChild(0).GetComponent<SpriteRenderer>().color = new Color(1f,0f,0f, 0.4f);
        
        if (whatRowToGo == 3)          transform.GetChild(0).GetComponent<Transform>().position = new (minkyu.transform.position.x, floor3YHeight, 0f);
        else if (whatRowToGo == 2)     transform.GetChild(0).GetComponent<Transform>().position = new (minkyu.transform.position.x, floor2YHeight, 0f);
        else                           transform.GetChild(0).GetComponent<Transform>().position = new (minkyu.transform.position.x, floor1YHeight, 0f);
        // call RedBox
        audioSource.PlayOneShot(bikeSound);
        
        yield return new WaitForSeconds(chaseTime);
        transform.GetChild(0).GetComponent<SpriteRenderer>().color = Color.clear; // clear red box
    
        if (whatRowToGo == 3)       transform.position = new (minkyu.transform.position.x - 10f, floor3YHeight , 0f);
        else if (whatRowToGo == 2)  transform.position = new (minkyu.transform.position.x - 10f, floor2YHeight , 0f);
        else                        transform.position = new (minkyu.transform.position.x - 10f, floor1YHeight , 0f);
        
        // set motor cycle position
        gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(bikeVelocity, 0f);

        yield return new WaitForSeconds(bikeRunTime);
        Destroy(gameObject);
    }
}